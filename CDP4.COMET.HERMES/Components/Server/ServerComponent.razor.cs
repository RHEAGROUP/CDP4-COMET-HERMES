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
        /// <summary>
        /// The ViewModel associated with the sync feature, which will store all the data related to what needs to be synced
        /// </summary>
        [Inject]
        public ISyncViewModel SyncViewModel { get; set; }

        /// <summary>
        /// Reetrieves all the different things from within a <see cref="SiteDirectory"/> object
        /// </summary>
        /// <param name="siteDirectory"></param>
        /// <returns>A list of lists of Things></returns>
        public static IEnumerable<IEnumerable<Thing>> GetThingsFromSiteDirectory(SiteDirectory siteDirectory)
        {
            if (siteDirectory == null)
            {
                return Enumerable.Empty<IEnumerable<Thing>>();
            }

            return siteDirectory.ContainerLists.Select(x => (IEnumerable<Thing>)x);
        }

        /// <summary>
        /// Sets the new session onto the view model
        /// </summary>
        /// <param name="session">The new session that is going to be set</param>
        /// <param name="isSourceSession">A bit that defines if we're setting a source or a target. Default is true.</param>
        public void SetSession(ISession session, bool isSourceSession = true)
        {
            if (isSourceSession)
            { 
                this.SyncViewModel.SourceSession = session;
            }
            else
            {
                this.SyncViewModel.TargetSession = session;
            }

            var isOppositeSessionSet = isSourceSession ? this.SyncViewModel.TargetSession != null : this.SyncViewModel.SourceSession != null;

            if (isOppositeSessionSet)
            {
                this.SyncViewModel.CurrentSyncStep++;
            }

            this.StateHasChanged();
        }
    }
}
