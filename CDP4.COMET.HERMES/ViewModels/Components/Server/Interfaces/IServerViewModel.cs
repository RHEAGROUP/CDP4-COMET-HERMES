// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IServerViewModel.cs" company="RHEA System S.A.">
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

    using CDP4Dal;

    using global::COMET.Web.Common.Model.DTO;

    public interface IServerViewModel
    {
        ISession GetSessionData(AuthenticationDto authenticationDto);
        Task Authenticate();
        AuthenticationDto AuthenticationDto { get; set; }
        AuthenticationStateKind? AuthenticationState { get; set; }
        ISession Session { get; set; }
    }
}