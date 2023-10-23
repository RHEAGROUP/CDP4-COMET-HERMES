// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ISyncViewModel.cs" company="RHEA System S.A.">
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

namespace CDP4.COMET.HERMES.ViewModels.Components.Server.Interfaces
{
    using CDP4.COMET.HERMES.Models.Enums;

    using CDP4Common.SiteDirectoryData;

    using CDP4Dal;

    /// <summary>
    /// A ViewModel responsible for handling the sync of data between a source session and a target session
    /// </summary>
    public interface ISyncViewModel
    {
        /// <summary>
        /// The source session where the data being selected from
        /// </summary>
        ISession SourceSession { get; set; }
        
        /// <summary>
        /// A target session where the data is being transferred to
        /// </summary>
        ISession TargetSession { get; set; }
        
        /// <summary>
        /// Defines the current step the user is at in the sync process
        /// </summary>
        SyncStep CurrentSyncStep { get; set; }

        /// <summary>
        /// The <see cref="SiteDirectory"/> object to hold the data that we want to transfer to the target server
        /// </summary>
        SiteDirectory SelectedSiteDirectory { get; set; }
    }
}
