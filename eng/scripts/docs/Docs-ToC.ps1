function Get-NamepspacesFromDll($dllPath) {
    $file = [System.IO.File]::OpenRead($dllPath)
    $namespaces = @()
    try {
        # Use to parse the namespaces out from the dll file.
        $pe = [System.Reflection.PortableExecutable.PEReader]::new($file)
        try {
            $meta = [System.Reflection.Metadata.PEReaderExtensions]::GetMetadataReader($pe)
            foreach ($typeHandle in $meta.TypeDefinitions) {
                $type = $meta.GetTypeDefinition($typeHandle)
                $attr = $type.Attributes
                if ($attr -band 'Public' -and !$type.IsNested) {
                    $namespaces += $meta.GetString($type.Name)
                }
            }
        } finally {
            $pe.Dispose()
        }
    } finally {
        $file.Dispose()
    }
    return $namespaces | Sort-Object -Unique
}

function Fetch-NamespacesFromNupkg ($nupkgFilePath) {
    $tempLocation = (Join-Path ([System.IO.Path]::GetTempPath()) "extractNupkg")
    if (Test-Path $tempLocation) {
        Remove-Item $tempLocation/* -Recurse -Force 
    }
    else {
        New-Item -ItemType Directory -Path $tempLocation -Force | Out-Null
    }

    Write-Host "Unzipping nupkg..."
    Write-Host $nupkgFilePath
    Add-Type -AssemblyName System.IO.Compression.FileSystem
    [System.IO.Compression.ZipFile]::ExtractToDirectory($nupkgFilePath, $tempLocation)
    # .NET core includes multiple target framework. We currently have the same namespaces for different framework. 
    # Will use whatever the first dll file.
    Write-Host "Searching ddl file..."
    $dllFile = Get-ChildItem "$tempLocation/netstandard2.0" -Filter '*.dll' -Recurse
    if (!$dllFile -or ($dllFile.Count -gt 1)) {
        Write-Warning "Can't find any dll file from $nupkgFilePath with target netstandard2.0."
        $dllFiles = Get-ChildItem "$tempLocation/" -Filter '*.dll' -Recurse
        if (!$dllFiles) {
            Write-Error "Can't find any dll file from $nupkgFilePath."
            return $null
        }
        $dllFile = $dllFiles[0]
    }
    Write-Host "Dll file found: $($dllFile.FullName)"
    $namespaces = Get-NamepspacesFromDll $dllFile.FullName
    if (!$namespaces) {
        Write-Error "Can't find namespaces from dotnet $nupkgFilePath."
        return
    }
    return $namespaces.Namespace | Sort-Object -Unique
}

function Get-dotnet-OnboardedDocsMsPackages($DocRepoLocation) {
    $packageOnboardingFiles = @(
        "$DocRepoLocation/bundlepackages/azure-dotnet.csv",
        "$DocRepoLocation/bundlepackages/azure-dotnet-preview.csv",
        "$DocRepoLocation/bundlepackages/azure-dotnet-legacy.csv")

    $onboardedPackages = @{}
    foreach ($file in $packageOnboardingFiles) {
        $onboardingSpec = Get-Content $file
        foreach ($spec in $onboardingSpec) {
            $packageInfo = $spec -split ","
            if (!$packageInfo -or ($packageInfo.Count -lt 2)) {
                Write-Error "Please check the package info in csv file $file. Please have at least one package and follow the format {name index, package name, version(optional)}"
                return $null
            }
            $packageName = $packageInfo[1].Trim() -replace "\[.*\](.*)", '$1'
            $onboardedPackages[$packageName] = $null
        }
    }
    return $onboardedPackages
}

function GetPackageReadmeName($packageMetadata) {
    # Fallback to get package-level readme name if metadata file info does not exist
    $packageLevelReadmeName = $packageMetadata.Package.ToLower().Replace('azure.', '')
  
    # If there is a metadata json for the package use the DocsMsReadmeName from
    # the metadata function
    if ($packageMetadata.PSObject.Members.Name -contains "FileMetadata") {
      $readmeMetadata = &$GetDocsMsMetadataForPackageFn -PackageInfo $packageMetadata.FileMetadata
      $packageLevelReadmeName = $readmeMetadata.DocsMsReadMeName
    }
    return $packageLevelReadmeName
  }
  
function Get-dotnet-DocsMsTocData($packageMetadata, $docRepoLocation) {
    $packageLevelReadmeName = GetPackageReadmeName -packageMetadata $packageMetadata
    $packageTocHeader = $packageMetadata.Package
    if ($packageMetadata.DisplayName) {
      $packageTocHeader = $packageMetadata.DisplayName
    }
    $children = @()
    # Children here combine namespaces in both preview and GA.
    if($packageMetadata.VersionPreview) {
        $children += Get-Toc-Children -package $packageMetadata.Package -version $packageMetadata.VersionPreview `
            -docRepoLocation $docRepoLocation -folderName "preview"
    }
    if($packageMetadata.VersionGA) {
        $children += Get-Toc-Children -package $packageMetadata.Package -version $packageMetadata.VersionGA `
            -docRepoLocation $docRepoLocation -folderName "latest"
    }
    $children = @($children | Sort-Object -Unique)
    # Write-Host $children
    if (!$children) {
        if ($packageMetadata.VersionPreview) {
            Write-Host "Did not find the package namespaces for $($packageMetadata.Package):$($packageMetadata.VersionPreview)"
        }
        if ($packageMetadata.VersionGA) {
            Write-Host "Did not find the package namespaces for $($packageMetadata.Package):$($packageMetadata.VersionGA)"
        }
    }
    $output = [PSCustomObject]@{
      PackageLevelReadmeHref = "~/api/overview/azure/{moniker}/$packageLevelReadmeName-readme.md"
      PackageTocHeader       = $packageTocHeader
      TocChildren            = $children
    }
  
    return $output
}

function DownloadNugetPackage($package, $version, $destination) {
    # $PackageSourceOverride is a global variable provided in
    # Update-DocsMsPackages.ps1. Its value can set a "customSource" property.
    # If it is empty then the property is not overridden.
    $customPackageSource = Get-Variable -Name 'PackageSourceOverride' -ValueOnly -ErrorAction 'Ignore'
    if (!$customPackageSource) {
        Find-Package -Name $package -ProviderName NuGet -RequiredVersion $version | Install-Package -Destination $destination -Scope CurrentUser -Force
    }
    Find-Package -Name $package -Source CustomPackageSource -RequiredVersion $version | Install-Package -Destination $destination -Scope CurrentUser -Force
}

# This is a helper function which fetches the dotnet package namespaces from nupkg.
# Here are the major workflow:
# 1. Read the ${package}.json under /metadata folder. If Namespaces exists and version match, return the array. 
# 2. If the json file exist but version mismatch, fetch the namespaces from nuget and udpate the json namespaces property.
# 3. If file not found, then fetch namespaces from nuget and create new package.json file with very basic info, like package name, version, namespaces. 
function Get-Toc-Children($package, $version, $docRepoLocation, $folderName) {
    # Looking for the txt
    $packageJsonPath = "$docRepoLocation/metadata/$folderName/$package.json" 
    $namespaces = @()
    $packageJsonObject = [PSCustomObject]@{
        Name = $package
        Version = $version
        Namespaces = @()
    }
    if (!(Test-Path $packageJsonPath)) {
        New-Item $packageJsonPath -Type File -Force
        $tempLocation = (Join-Path ([System.IO.Path]::GetTempPath()) "dotnetnupkg1")
        DownloadNugetPackage -package $package -version $version -destination $tempLocation
        $namespaces = Fetch-NamespacesFromNupkg -nupkgFilePath "$tempLocation/$package.$version.nupkg"
        $packageJsonObject.Namespaces = $namespaces
        Set-Content $packageJsonPath -Value ($packageJsonObject | ConvertTo-Json)
    }
    $packageJson = Get-Content $packageJsonPath | ConvertTo-Json
    $existingNamespaces = $packageJson.Namespaces
    $existingVersion = $packageJson.version
    if (($version -eq $existingVersion) -and $existingNamespaces) {
        return $existingNamespaces
    }
    return $namespaces
}
