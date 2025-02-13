// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Azure;
using Azure.Core;
using Azure.Core.Pipeline;
using Azure.ResourceManager;
using Azure.ResourceManager.HealthcareApis.Models;

namespace Azure.ResourceManager.HealthcareApis
{
    /// <summary> A class to add extension methods to SubscriptionResource. </summary>
    internal partial class SubscriptionResourceExtensionClient : ArmResource
    {
        private ClientDiagnostics _healthcareApisServiceServicesClientDiagnostics;
        private ServicesRestOperations _healthcareApisServiceServicesRestClient;
        private ClientDiagnostics _healthcareApisWorkspaceWorkspacesClientDiagnostics;
        private WorkspacesRestOperations _healthcareApisWorkspaceWorkspacesRestClient;

        /// <summary> Initializes a new instance of the <see cref="SubscriptionResourceExtensionClient"/> class for mocking. </summary>
        protected SubscriptionResourceExtensionClient()
        {
        }

        /// <summary> Initializes a new instance of the <see cref="SubscriptionResourceExtensionClient"/> class. </summary>
        /// <param name="client"> The client parameters to use in these operations. </param>
        /// <param name="id"> The identifier of the resource that is the target of operations. </param>
        internal SubscriptionResourceExtensionClient(ArmClient client, ResourceIdentifier id) : base(client, id)
        {
        }

        private ClientDiagnostics HealthcareApisServiceServicesClientDiagnostics => _healthcareApisServiceServicesClientDiagnostics ??= new ClientDiagnostics("Azure.ResourceManager.HealthcareApis", HealthcareApisServiceResource.ResourceType.Namespace, Diagnostics);
        private ServicesRestOperations HealthcareApisServiceServicesRestClient => _healthcareApisServiceServicesRestClient ??= new ServicesRestOperations(Pipeline, Diagnostics.ApplicationId, Endpoint, GetApiVersionOrNull(HealthcareApisServiceResource.ResourceType));
        private ClientDiagnostics HealthcareApisWorkspaceWorkspacesClientDiagnostics => _healthcareApisWorkspaceWorkspacesClientDiagnostics ??= new ClientDiagnostics("Azure.ResourceManager.HealthcareApis", HealthcareApisWorkspaceResource.ResourceType.Namespace, Diagnostics);
        private WorkspacesRestOperations HealthcareApisWorkspaceWorkspacesRestClient => _healthcareApisWorkspaceWorkspacesRestClient ??= new WorkspacesRestOperations(Pipeline, Diagnostics.ApplicationId, Endpoint, GetApiVersionOrNull(HealthcareApisWorkspaceResource.ResourceType));

        private string GetApiVersionOrNull(ResourceType resourceType)
        {
            TryGetApiVersion(resourceType, out string apiVersion);
            return apiVersion;
        }

        /// <summary>
        /// Get all the service instances in a subscription.
        /// Request Path: /subscriptions/{subscriptionId}/providers/Microsoft.HealthcareApis/services
        /// Operation Id: Services_List
        /// </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        /// <returns> An async collection of <see cref="HealthcareApisServiceResource" /> that may take multiple service requests to iterate over. </returns>
        public virtual AsyncPageable<HealthcareApisServiceResource> GetHealthcareApisServicesAsync(CancellationToken cancellationToken = default)
        {
            async Task<Page<HealthcareApisServiceResource>> FirstPageFunc(int? pageSizeHint)
            {
                using var scope = HealthcareApisServiceServicesClientDiagnostics.CreateScope("SubscriptionResourceExtensionClient.GetHealthcareApisServices");
                scope.Start();
                try
                {
                    var response = await HealthcareApisServiceServicesRestClient.ListAsync(Id.SubscriptionId, cancellationToken: cancellationToken).ConfigureAwait(false);
                    return Page.FromValues(response.Value.Value.Select(value => new HealthcareApisServiceResource(Client, value)), response.Value.NextLink, response.GetRawResponse());
                }
                catch (Exception e)
                {
                    scope.Failed(e);
                    throw;
                }
            }
            async Task<Page<HealthcareApisServiceResource>> NextPageFunc(string nextLink, int? pageSizeHint)
            {
                using var scope = HealthcareApisServiceServicesClientDiagnostics.CreateScope("SubscriptionResourceExtensionClient.GetHealthcareApisServices");
                scope.Start();
                try
                {
                    var response = await HealthcareApisServiceServicesRestClient.ListNextPageAsync(nextLink, Id.SubscriptionId, cancellationToken: cancellationToken).ConfigureAwait(false);
                    return Page.FromValues(response.Value.Value.Select(value => new HealthcareApisServiceResource(Client, value)), response.Value.NextLink, response.GetRawResponse());
                }
                catch (Exception e)
                {
                    scope.Failed(e);
                    throw;
                }
            }
            return PageableHelpers.CreateAsyncEnumerable(FirstPageFunc, NextPageFunc);
        }

        /// <summary>
        /// Get all the service instances in a subscription.
        /// Request Path: /subscriptions/{subscriptionId}/providers/Microsoft.HealthcareApis/services
        /// Operation Id: Services_List
        /// </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        /// <returns> A collection of <see cref="HealthcareApisServiceResource" /> that may take multiple service requests to iterate over. </returns>
        public virtual Pageable<HealthcareApisServiceResource> GetHealthcareApisServices(CancellationToken cancellationToken = default)
        {
            Page<HealthcareApisServiceResource> FirstPageFunc(int? pageSizeHint)
            {
                using var scope = HealthcareApisServiceServicesClientDiagnostics.CreateScope("SubscriptionResourceExtensionClient.GetHealthcareApisServices");
                scope.Start();
                try
                {
                    var response = HealthcareApisServiceServicesRestClient.List(Id.SubscriptionId, cancellationToken: cancellationToken);
                    return Page.FromValues(response.Value.Value.Select(value => new HealthcareApisServiceResource(Client, value)), response.Value.NextLink, response.GetRawResponse());
                }
                catch (Exception e)
                {
                    scope.Failed(e);
                    throw;
                }
            }
            Page<HealthcareApisServiceResource> NextPageFunc(string nextLink, int? pageSizeHint)
            {
                using var scope = HealthcareApisServiceServicesClientDiagnostics.CreateScope("SubscriptionResourceExtensionClient.GetHealthcareApisServices");
                scope.Start();
                try
                {
                    var response = HealthcareApisServiceServicesRestClient.ListNextPage(nextLink, Id.SubscriptionId, cancellationToken: cancellationToken);
                    return Page.FromValues(response.Value.Value.Select(value => new HealthcareApisServiceResource(Client, value)), response.Value.NextLink, response.GetRawResponse());
                }
                catch (Exception e)
                {
                    scope.Failed(e);
                    throw;
                }
            }
            return PageableHelpers.CreateEnumerable(FirstPageFunc, NextPageFunc);
        }

        /// <summary>
        /// Check if a service instance name is available.
        /// Request Path: /subscriptions/{subscriptionId}/providers/Microsoft.HealthcareApis/checkNameAvailability
        /// Operation Id: Services_CheckNameAvailability
        /// </summary>
        /// <param name="content"> Set the name parameter in the CheckNameAvailabilityParameters structure to the name of the service instance to check. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public virtual async Task<Response<HealthcareApisNameAvailabilityResult>> CheckHealthcareApisNameAvailabilityAsync(HealthcareApisNameAvailabilityContent content, CancellationToken cancellationToken = default)
        {
            using var scope = HealthcareApisServiceServicesClientDiagnostics.CreateScope("SubscriptionResourceExtensionClient.CheckHealthcareApisNameAvailability");
            scope.Start();
            try
            {
                var response = await HealthcareApisServiceServicesRestClient.CheckNameAvailabilityAsync(Id.SubscriptionId, content, cancellationToken).ConfigureAwait(false);
                return response;
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }

        /// <summary>
        /// Check if a service instance name is available.
        /// Request Path: /subscriptions/{subscriptionId}/providers/Microsoft.HealthcareApis/checkNameAvailability
        /// Operation Id: Services_CheckNameAvailability
        /// </summary>
        /// <param name="content"> Set the name parameter in the CheckNameAvailabilityParameters structure to the name of the service instance to check. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public virtual Response<HealthcareApisNameAvailabilityResult> CheckHealthcareApisNameAvailability(HealthcareApisNameAvailabilityContent content, CancellationToken cancellationToken = default)
        {
            using var scope = HealthcareApisServiceServicesClientDiagnostics.CreateScope("SubscriptionResourceExtensionClient.CheckHealthcareApisNameAvailability");
            scope.Start();
            try
            {
                var response = HealthcareApisServiceServicesRestClient.CheckNameAvailability(Id.SubscriptionId, content, cancellationToken);
                return response;
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }

        /// <summary>
        /// Lists all the available workspaces under the specified subscription.
        /// Request Path: /subscriptions/{subscriptionId}/providers/Microsoft.HealthcareApis/workspaces
        /// Operation Id: Workspaces_ListBySubscription
        /// </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        /// <returns> An async collection of <see cref="HealthcareApisWorkspaceResource" /> that may take multiple service requests to iterate over. </returns>
        public virtual AsyncPageable<HealthcareApisWorkspaceResource> GetHealthcareApisWorkspacesAsync(CancellationToken cancellationToken = default)
        {
            async Task<Page<HealthcareApisWorkspaceResource>> FirstPageFunc(int? pageSizeHint)
            {
                using var scope = HealthcareApisWorkspaceWorkspacesClientDiagnostics.CreateScope("SubscriptionResourceExtensionClient.GetHealthcareApisWorkspaces");
                scope.Start();
                try
                {
                    var response = await HealthcareApisWorkspaceWorkspacesRestClient.ListBySubscriptionAsync(Id.SubscriptionId, cancellationToken: cancellationToken).ConfigureAwait(false);
                    return Page.FromValues(response.Value.Value.Select(value => new HealthcareApisWorkspaceResource(Client, value)), response.Value.NextLink, response.GetRawResponse());
                }
                catch (Exception e)
                {
                    scope.Failed(e);
                    throw;
                }
            }
            async Task<Page<HealthcareApisWorkspaceResource>> NextPageFunc(string nextLink, int? pageSizeHint)
            {
                using var scope = HealthcareApisWorkspaceWorkspacesClientDiagnostics.CreateScope("SubscriptionResourceExtensionClient.GetHealthcareApisWorkspaces");
                scope.Start();
                try
                {
                    var response = await HealthcareApisWorkspaceWorkspacesRestClient.ListBySubscriptionNextPageAsync(nextLink, Id.SubscriptionId, cancellationToken: cancellationToken).ConfigureAwait(false);
                    return Page.FromValues(response.Value.Value.Select(value => new HealthcareApisWorkspaceResource(Client, value)), response.Value.NextLink, response.GetRawResponse());
                }
                catch (Exception e)
                {
                    scope.Failed(e);
                    throw;
                }
            }
            return PageableHelpers.CreateAsyncEnumerable(FirstPageFunc, NextPageFunc);
        }

        /// <summary>
        /// Lists all the available workspaces under the specified subscription.
        /// Request Path: /subscriptions/{subscriptionId}/providers/Microsoft.HealthcareApis/workspaces
        /// Operation Id: Workspaces_ListBySubscription
        /// </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        /// <returns> A collection of <see cref="HealthcareApisWorkspaceResource" /> that may take multiple service requests to iterate over. </returns>
        public virtual Pageable<HealthcareApisWorkspaceResource> GetHealthcareApisWorkspaces(CancellationToken cancellationToken = default)
        {
            Page<HealthcareApisWorkspaceResource> FirstPageFunc(int? pageSizeHint)
            {
                using var scope = HealthcareApisWorkspaceWorkspacesClientDiagnostics.CreateScope("SubscriptionResourceExtensionClient.GetHealthcareApisWorkspaces");
                scope.Start();
                try
                {
                    var response = HealthcareApisWorkspaceWorkspacesRestClient.ListBySubscription(Id.SubscriptionId, cancellationToken: cancellationToken);
                    return Page.FromValues(response.Value.Value.Select(value => new HealthcareApisWorkspaceResource(Client, value)), response.Value.NextLink, response.GetRawResponse());
                }
                catch (Exception e)
                {
                    scope.Failed(e);
                    throw;
                }
            }
            Page<HealthcareApisWorkspaceResource> NextPageFunc(string nextLink, int? pageSizeHint)
            {
                using var scope = HealthcareApisWorkspaceWorkspacesClientDiagnostics.CreateScope("SubscriptionResourceExtensionClient.GetHealthcareApisWorkspaces");
                scope.Start();
                try
                {
                    var response = HealthcareApisWorkspaceWorkspacesRestClient.ListBySubscriptionNextPage(nextLink, Id.SubscriptionId, cancellationToken: cancellationToken);
                    return Page.FromValues(response.Value.Value.Select(value => new HealthcareApisWorkspaceResource(Client, value)), response.Value.NextLink, response.GetRawResponse());
                }
                catch (Exception e)
                {
                    scope.Failed(e);
                    throw;
                }
            }
            return PageableHelpers.CreateEnumerable(FirstPageFunc, NextPageFunc);
        }
    }
}
