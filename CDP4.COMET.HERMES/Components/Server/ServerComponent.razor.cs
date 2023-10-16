// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ServerAuthenticationComponent.razor.cs" company="RHEA System S.A.">
// 
//    Copyright (c) 2023 RHEA System S.A.
// 
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
// 
//        http://www.apache.org/licenses/LICENSE-2.0
// 
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace CDP4.COMET.HERMES.Components.Server
{
    using CDP4.COMET.HERMES.ViewModels.Components.Server.Interfaces;

    using Microsoft.AspNetCore.Components;

    using System.Text.Json;

    using CDP4.COMET.HERMES.Models.Enums;

    using CDP4Common.CommonData;
    using CDP4Common.SiteDirectoryData;

    using CDP4Dal;

    using DevExpress.Blazor.Internal;

    using global::COMET.Web.Common.Model.DTO;

    using ReactiveUI;

    /// <summary>
    /// Component which should handle and store all authentication and validation to a particular server
    /// </summary>
    public partial class ServerComponent
    {
        [Inject]
        public ISyncViewModel SyncViewModel { get; set; }
        
        /// <summary>
        /// Method invoked when the component is ready to start, having received its
        /// initial parameters from its parent in the render tree.
        /// </summary>
        /// 
        protected override void OnInitialized()
        {
            base.OnInitialized();
            this.SyncViewModel.SelectedSiteDirectory = new();
        }

        private IEnumerable<IEnumerable<Thing>> GetThingsFromSiteDirectory(SiteDirectory siteDirectory)
        {
            return siteDirectory.ContainerLists.Select(x => (IEnumerable<Thing>)x);
        }

        /// <summary>
        /// Adds a thing to the site directory depending on their type
        /// </summary>
        /// <param name="thing">A thing to add to the selection.</param>
        public void OnAdd(Thing thing)
        {
            var thingType = thing.GetType();

            if (thingType == typeof(DomainOfExpertise))
            {
                this.SyncViewModel.SelectedSiteDirectory.Domain.Add((DomainOfExpertise)thing);
            }
            else if (thingType == typeof(Organization))
            {
                this.SyncViewModel.SelectedSiteDirectory.Organization.Add((Organization)thing);
            }
            else if (thingType == typeof(NaturalLanguage))
            {
                this.SyncViewModel.SelectedSiteDirectory.NaturalLanguage.Add((NaturalLanguage)thing);
            }
            else if (thingType == typeof(DomainOfExpertiseGroup))
            {
                this.SyncViewModel.SelectedSiteDirectory.DomainGroup.Add((DomainOfExpertiseGroup)thing);
            }
            else if (thingType == typeof(EngineeringModelSetup))
            {
                this.SyncViewModel.SelectedSiteDirectory.Model.Add((EngineeringModelSetup)thing);
            }
            else if (thingType == typeof(ParticipantRole))
            {
                this.SyncViewModel.SelectedSiteDirectory.ParticipantRole.Add((ParticipantRole)thing);
            }
            else if (thingType == typeof(Person))
            {
                this.SyncViewModel.SelectedSiteDirectory.Person.Add((Person)thing);
            }
            else if (thingType == typeof(PersonRole))
            {
                this.SyncViewModel.SelectedSiteDirectory.PersonRole.Add((PersonRole)thing);
            }
            else if (thingType == typeof(SiteReferenceDataLibrary))
            {
                this.SyncViewModel.SelectedSiteDirectory.SiteReferenceDataLibrary.Add((SiteReferenceDataLibrary)thing);
            }

            this.StateHasChanged();
        }

        private Task OnCommitClick()
        {
            //TODO: Changes commit logic
            this.StateHasChanged();
            return Task.CompletedTask;
        }
    }
}