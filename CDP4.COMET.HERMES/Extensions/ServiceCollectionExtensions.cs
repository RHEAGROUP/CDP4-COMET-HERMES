// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ServiceCollectionExtensions.cs" company="RHEA System S.A.">
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

namespace CDP4.COMET.HERMES.Extensions
{
    using CDP4.COMET.HERMES.Services.VersionService;
    using CDP4.COMET.HERMES.ViewModels.Components.Server;
    using CDP4.COMET.HERMES.ViewModels.Components.Server.Interfaces;
    using CDP4.COMET.HERMES.ViewModels.Pages;

    /// <summary>
    /// Extension class for the <see cref="IServiceCollection"/>
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Register required services for this application
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/></param>
        public static void RegisterHermesServices(this IServiceCollection services)
        {
            services.AddSingleton<IVersionService, VersionService>();
        }

        /// <summary>
        /// Register required view model for this application
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/></param>
        public static void RegisterHermesViewModels(this IServiceCollection services)
        {
            services.AddTransient<IIndexViewModel, IndexViewModel>();
            services.AddTransient<IServerViewModel, ServerViewModel>();
            services.AddSingleton<ISyncViewModel, SyncViewModel>();
        }
    }
}