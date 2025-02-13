// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

using System.Collections.Generic;
using Azure.Core;

namespace Azure.ResourceManager.Reservations.Models
{
    /// <summary> The ReservationDetailPatch. </summary>
    public partial class ReservationDetailPatch
    {
        /// <summary> Initializes a new instance of ReservationDetailPatch. </summary>
        public ReservationDetailPatch()
        {
            AppliedScopes = new ChangeTrackingList<string>();
        }

        /// <summary> Type of the Applied Scope. </summary>
        public AppliedScopeType? AppliedScopeType { get; set; }
        /// <summary> List of the subscriptions that the benefit will be applied. Do not specify if AppliedScopeType is Shared. </summary>
        public IList<string> AppliedScopes { get; }
        /// <summary> Turning this on will apply the reservation discount to other VMs in the same VM size group. Only specify for VirtualMachines reserved resource type. </summary>
        public InstanceFlexibility? InstanceFlexibility { get; set; }
        /// <summary> Name of the Reservation. </summary>
        public string Name { get; set; }
        /// <summary> Setting this to true will automatically purchase a new reservation on the expiration date time. </summary>
        public bool? IsRenewEnabled { get; set; }
        /// <summary> Gets or sets the renew properties. </summary>
        internal PatchPropertiesRenewProperties RenewProperties { get; set; }
        /// <summary> Gets or sets the purchase properties. </summary>
        public ReservationPurchaseContent RenewPurchaseProperties
        {
            get => RenewProperties is null ? default : RenewProperties.PurchaseProperties;
            set
            {
                if (RenewProperties is null)
                    RenewProperties = new PatchPropertiesRenewProperties();
                RenewProperties.PurchaseProperties = value;
            }
        }
    }
}
