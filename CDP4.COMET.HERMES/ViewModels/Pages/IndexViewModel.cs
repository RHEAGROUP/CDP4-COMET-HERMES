// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IndexViewModel.cs" company="RHEA System S.A.">
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

namespace CDP4.COMET.HERMES.ViewModels.Pages
{
    using CDP4.COMET.HERMES.Services.VersionService;

    /// <summary>
    /// ViewModel that handle behavior of the Index page
    /// </summary>
    public class IndexViewModel: IIndexViewModel
    {
        /// <summary>
        /// Gets the <see cref="IVersionService"/>
        /// </summary>
        public IVersionService VersionService { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="IndexViewModel" /> class.
        /// </summary>
        /// <param name="versionService">The <see cref="IVersionService"/></param>
        public IndexViewModel(IVersionService versionService)
        {
            this.VersionService = versionService;
        }
    }
}