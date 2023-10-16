// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ServerViewModel.cs" company="RHEA System S.A.">
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

namespace CDP4.COMET.HERMES.ViewModels.Components.Server
{
    using CDP4.COMET.HERMES.Models.Enums;
    using CDP4.COMET.HERMES.ViewModels.Components.Server.Interfaces;

    using CDP4Dal;
    using CDP4Dal.DAL;
    using CDP4Dal.Exceptions;

    using CDP4ServicesDal;

    using DevExpress.ReportServer.ServiceModel.Client;

    using global::COMET.Web.Common.Model.DTO;

    using ReactiveUI;
    
    using ISession = CDP4Dal.ISession;

    /// <summary>
    /// View model that handles the logic authentication related to <see cref="ISession" />
    /// </summary>
    public class ServerViewModel : ReactiveObject, IServerViewModel
    {
        /// <summary>
        /// The <see cref="AuthenticationDto" /> used for perfoming a login
        /// </summary>
        public AuthenticationDto AuthenticationDto { get; set; } = new();
        
        /// <summary>
        /// Gets or sets the <see cref="AuthenticationStateKind" />
        /// </summary>
        public AuthenticationStateKind? AuthenticationState { get; set; }
        
        /// <summary>
        /// Gets or sets the <see cref="ISession" />
        /// </summary>
        public ISession Session { get; set; }
        
        /// <summary>
        /// Creates a new session with the given credentials
        /// </summary>
        public async Task Authenticate()
        {
            if (string.IsNullOrEmpty(this.AuthenticationDto.SourceAddress))
            {
                this.AuthenticationState = AuthenticationStateKind.Fail;
                return;
            }

            var credentials = new Credentials(this.AuthenticationDto.UserName, this.AuthenticationDto.Password, new Uri(this.AuthenticationDto.SourceAddress));
            this.Session = new Session(new CdpServicesDal(), credentials);

            try
            {
                await this.Session.Open();
                this.AuthenticationState = this.Session.RetrieveSiteDirectory() != null ? AuthenticationStateKind.Success : AuthenticationStateKind.Fail;
            }
            catch (DalReadException)
            {
                this.AuthenticationState =  AuthenticationStateKind.Fail;
            }
            catch (HttpRequestException)
            {
                this.AuthenticationState =  AuthenticationStateKind.ServerFail;
            }
        }
    }
}