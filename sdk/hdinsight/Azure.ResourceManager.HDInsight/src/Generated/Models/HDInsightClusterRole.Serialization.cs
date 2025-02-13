// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

using System.Collections.Generic;
using System.Text.Json;
using Azure.Core;

namespace Azure.ResourceManager.HDInsight.Models
{
    public partial class HDInsightClusterRole : IUtf8JsonSerializable
    {
        void IUtf8JsonSerializable.Write(Utf8JsonWriter writer)
        {
            writer.WriteStartObject();
            if (Optional.IsDefined(Name))
            {
                writer.WritePropertyName("name");
                writer.WriteStringValue(Name);
            }
            if (Optional.IsDefined(MinInstanceCount))
            {
                writer.WritePropertyName("minInstanceCount");
                writer.WriteNumberValue(MinInstanceCount.Value);
            }
            if (Optional.IsDefined(TargetInstanceCount))
            {
                writer.WritePropertyName("targetInstanceCount");
                writer.WriteNumberValue(TargetInstanceCount.Value);
            }
            if (Optional.IsDefined(VmGroupName))
            {
                writer.WritePropertyName("VMGroupName");
                writer.WriteStringValue(VmGroupName);
            }
            if (Optional.IsDefined(AutoScaleConfiguration))
            {
                writer.WritePropertyName("autoscale");
                writer.WriteObjectValue(AutoScaleConfiguration);
            }
            if (Optional.IsDefined(HardwareProfile))
            {
                writer.WritePropertyName("hardwareProfile");
                writer.WriteObjectValue(HardwareProfile);
            }
            if (Optional.IsDefined(OSProfile))
            {
                writer.WritePropertyName("osProfile");
                writer.WriteObjectValue(OSProfile);
            }
            if (Optional.IsDefined(VirtualNetworkProfile))
            {
                writer.WritePropertyName("virtualNetworkProfile");
                writer.WriteObjectValue(VirtualNetworkProfile);
            }
            if (Optional.IsCollectionDefined(DataDisksGroups))
            {
                writer.WritePropertyName("dataDisksGroups");
                writer.WriteStartArray();
                foreach (var item in DataDisksGroups)
                {
                    writer.WriteObjectValue(item);
                }
                writer.WriteEndArray();
            }
            if (Optional.IsCollectionDefined(ScriptActions))
            {
                writer.WritePropertyName("scriptActions");
                writer.WriteStartArray();
                foreach (var item in ScriptActions)
                {
                    writer.WriteObjectValue(item);
                }
                writer.WriteEndArray();
            }
            if (Optional.IsDefined(EncryptDataDisks))
            {
                writer.WritePropertyName("encryptDataDisks");
                writer.WriteBooleanValue(EncryptDataDisks.Value);
            }
            writer.WriteEndObject();
        }

        internal static HDInsightClusterRole DeserializeHDInsightClusterRole(JsonElement element)
        {
            Optional<string> name = default;
            Optional<int> minInstanceCount = default;
            Optional<int> targetInstanceCount = default;
            Optional<string> vmGroupName = default;
            Optional<HDInsightAutoScaleConfiguration> autoScale = default;
            Optional<HardwareProfile> hardwareProfile = default;
            Optional<OSProfile> osProfile = default;
            Optional<HDInsightVirtualNetworkProfile> virtualNetworkProfile = default;
            Optional<IList<HDInsightClusterDataDiskGroup>> dataDisksGroups = default;
            Optional<IList<ScriptAction>> scriptActions = default;
            Optional<bool> encryptDataDisks = default;
            foreach (var property in element.EnumerateObject())
            {
                if (property.NameEquals("name"))
                {
                    name = property.Value.GetString();
                    continue;
                }
                if (property.NameEquals("minInstanceCount"))
                {
                    if (property.Value.ValueKind == JsonValueKind.Null)
                    {
                        property.ThrowNonNullablePropertyIsNull();
                        continue;
                    }
                    minInstanceCount = property.Value.GetInt32();
                    continue;
                }
                if (property.NameEquals("targetInstanceCount"))
                {
                    if (property.Value.ValueKind == JsonValueKind.Null)
                    {
                        property.ThrowNonNullablePropertyIsNull();
                        continue;
                    }
                    targetInstanceCount = property.Value.GetInt32();
                    continue;
                }
                if (property.NameEquals("VMGroupName"))
                {
                    vmGroupName = property.Value.GetString();
                    continue;
                }
                if (property.NameEquals("autoscale"))
                {
                    if (property.Value.ValueKind == JsonValueKind.Null)
                    {
                        property.ThrowNonNullablePropertyIsNull();
                        continue;
                    }
                    autoScale = HDInsightAutoScaleConfiguration.DeserializeHDInsightAutoScaleConfiguration(property.Value);
                    continue;
                }
                if (property.NameEquals("hardwareProfile"))
                {
                    if (property.Value.ValueKind == JsonValueKind.Null)
                    {
                        property.ThrowNonNullablePropertyIsNull();
                        continue;
                    }
                    hardwareProfile = HardwareProfile.DeserializeHardwareProfile(property.Value);
                    continue;
                }
                if (property.NameEquals("osProfile"))
                {
                    if (property.Value.ValueKind == JsonValueKind.Null)
                    {
                        property.ThrowNonNullablePropertyIsNull();
                        continue;
                    }
                    osProfile = OSProfile.DeserializeOSProfile(property.Value);
                    continue;
                }
                if (property.NameEquals("virtualNetworkProfile"))
                {
                    if (property.Value.ValueKind == JsonValueKind.Null)
                    {
                        property.ThrowNonNullablePropertyIsNull();
                        continue;
                    }
                    virtualNetworkProfile = HDInsightVirtualNetworkProfile.DeserializeHDInsightVirtualNetworkProfile(property.Value);
                    continue;
                }
                if (property.NameEquals("dataDisksGroups"))
                {
                    if (property.Value.ValueKind == JsonValueKind.Null)
                    {
                        property.ThrowNonNullablePropertyIsNull();
                        continue;
                    }
                    List<HDInsightClusterDataDiskGroup> array = new List<HDInsightClusterDataDiskGroup>();
                    foreach (var item in property.Value.EnumerateArray())
                    {
                        array.Add(HDInsightClusterDataDiskGroup.DeserializeHDInsightClusterDataDiskGroup(item));
                    }
                    dataDisksGroups = array;
                    continue;
                }
                if (property.NameEquals("scriptActions"))
                {
                    if (property.Value.ValueKind == JsonValueKind.Null)
                    {
                        property.ThrowNonNullablePropertyIsNull();
                        continue;
                    }
                    List<ScriptAction> array = new List<ScriptAction>();
                    foreach (var item in property.Value.EnumerateArray())
                    {
                        array.Add(ScriptAction.DeserializeScriptAction(item));
                    }
                    scriptActions = array;
                    continue;
                }
                if (property.NameEquals("encryptDataDisks"))
                {
                    if (property.Value.ValueKind == JsonValueKind.Null)
                    {
                        property.ThrowNonNullablePropertyIsNull();
                        continue;
                    }
                    encryptDataDisks = property.Value.GetBoolean();
                    continue;
                }
            }
            return new HDInsightClusterRole(name.Value, Optional.ToNullable(minInstanceCount), Optional.ToNullable(targetInstanceCount), vmGroupName.Value, autoScale.Value, hardwareProfile.Value, osProfile.Value, virtualNetworkProfile.Value, Optional.ToList(dataDisksGroups), Optional.ToList(scriptActions), Optional.ToNullable(encryptDataDisks));
        }
    }
}
