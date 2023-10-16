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
    using CDP4.COMET.HERMES.Models.Enums;
    using CDP4.COMET.HERMES.ViewModels.Components.Server.Interfaces;

    using CDP4Dal;

    using DevExpress.ReportServer.ServiceModel.Client;

    using global::COMET.Web.Common.Model.DTO;

    using Microsoft.AspNetCore.Components;
    using Microsoft.AspNetCore.Components.Authorization;

    using ReactiveUI;

    /// <summary>
    /// A component that is responsible for enabling the user to authenticate to a specific server
    /// </summary>
    public partial class ServerAuthenticationComponent
    {
        /// <summary>
        /// Gets or sets the <see cref="IServerViewModel"/>
        /// </summary>
        [Inject]
        public IServerViewModel ViewModel { get; set; }
        
        /// <summary>
        /// An action that is executed when the user successfully authenticates.
        /// </summary>
        /// <returns>The session object</returns>
        [Parameter]
        public Action<ISession> OnAuthenticate { get; set; }

        /// <summary>
        /// The dictionary of focus status from the form fields
        /// </summary>
        private Dictionary<string, bool> FieldsFocusedStatus { get; set; }

        /// <summary>
        /// The text of the login button
        /// </summary>
        public string LoginButtonDisplayText { get; private set; } = "Connect";

        /// <summary>
        /// An error message to display after a login failure
        /// </summary>
        private string ErrorMessage { get; set; }

        /// <summary>
        /// Value indicating if the login button is enabled or not
        /// </summary>
        public bool LoginEnabled { get; set; } = true;
        
        /// <summary>
        /// Method invoked when the component is ready to start, having received its
        /// initial parameters from its parent in the render tree.
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();
                        
            this.FieldsFocusedStatus = new Dictionary<string, bool>()
            {
                { "SourceAddress", false },
                { "UserName", false },
                { "Password", false }
            };
            
            this.Disposables.Add(this.WhenAnyValue(x => x.ViewModel.AuthenticationState)
                .Subscribe(_ => this.ComputeDisplayProperties()));
        }

        /// <summary>
        /// Compute display properties based on the <see cref="AuthenticationState" />
        /// </summary>
        private void ComputeDisplayProperties()
        {
            this.LoginButtonDisplayText = this.ViewModel.AuthenticationState switch
            {
                AuthenticationStateKind.None => "Connect",
                AuthenticationStateKind.Authenticating => "Connecting",
                AuthenticationStateKind.ServerFail or AuthenticationStateKind.Fail => "Retry",
                _ => this.LoginButtonDisplayText
            };

            this.ErrorMessage = this.ViewModel.AuthenticationState switch
            {
                AuthenticationStateKind.ServerFail => "The server could not be reached",
                AuthenticationStateKind.Fail => "Login failed.",
                _ => string.Empty
            };

            this.InvokeAsync(this.StateHasChanged);
        }
        
        /// <summary>
        /// Handles the focus event of the given fieldName
        /// </summary>
        /// <param name="fieldName">Form field name, as indexed in <see cref="FieldsFocusedStatus"/></param>
        public void HandleFieldFocus(string fieldName)
        {
            this.FieldsFocusedStatus[fieldName] = true; // Set the field as focused
        }

        /// <summary>
        /// Handles the blur event of the given fieldName
        /// </summary>
        /// <param name="fieldName">Form field name, as indexed in <see cref="FieldsFocusedStatus"/></param>
        public void HandleFieldBlur(string fieldName)
        {
            this.FieldsFocusedStatus[fieldName] = false; // Set the field as not focused when it loses focus
        }
        
        /// <summary>
        /// Authenticates the user with the given form data
        /// </summary>
        private async Task Authenticate()
        {
            await this.ViewModel.Authenticate();
            this.StateHasChanged();

            if (this.ViewModel.Session != null)
            {
                this.OnAuthenticate.Invoke(this.ViewModel.Session);
            }
        }
    }
}