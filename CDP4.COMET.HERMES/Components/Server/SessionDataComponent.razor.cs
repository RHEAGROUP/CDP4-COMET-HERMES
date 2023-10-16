// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SessionDataComponent.razor.cs" company="RHEA System S.A.">
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
    using CDP4Common.CommonData;
    using CDP4Common.SiteDirectoryData;

    using Microsoft.AspNetCore.Components;

    using ISession = CDP4Dal.ISession;

    /// <summary>
    /// Class for the component that is responsible for displaying all of the data for a particular session object
    /// </summary>
    public partial class SessionDataComponent
    {
        /// <summary>
        /// The session object where we're retrieving the data from
        /// </summary>
        [Parameter] 
        public ISession Session { get; set; }
        
        /// <summary>
        /// The action that is called when the user clicks one of the options
        /// </summary>
        [Parameter]
        public Action<Thing> OnClick { get; set; }
        
        /// <summary>
        /// The SiteDirectory associated with the current selected session
        /// </summary>
        private SiteDirectory currentSessionSiteDirectory;
        
        /// <summary>
        /// Method invoked when the component is ready to start, having received its
        /// initial parameters from its parent in the render tree.
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            if (Session != null)
            {
                this.currentSessionSiteDirectory = this.Session.RetrieveSiteDirectory();
            }
        }
    }
}