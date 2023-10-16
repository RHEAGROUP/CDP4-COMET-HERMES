// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SourceDataComponent.razor.cs" company="RHEA System S.A.">
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

    using Microsoft.AspNetCore.Components;

    /// <summary>
    /// Class for the component that is responsible for displaying the data inside a particular list
    /// </summary>
    public partial class SourceDataComponent<T> where T : Thing
    {
        /// <summary>
        /// The source of the data that we're displaying
        /// </summary>
        [Parameter]
        public IEnumerable<T> Source { get; set; }
        
        /// <summary>
        /// The event that will be invoked if the user clicks on an item
        /// </summary>
        [Parameter]
        public Action<T> OnClick { get; set; }
        
        /// <summary>
        /// The title of the node tree
        /// </summary>
        [Parameter]
        public string Title { get; set; }

        /// <summary>
        /// If the tree is expanded by default or not
        /// </summary>
        [Parameter]
        public bool Expanded { get; set; }
    }
}