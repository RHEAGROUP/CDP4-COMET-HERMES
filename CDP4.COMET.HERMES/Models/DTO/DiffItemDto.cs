// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DiffItemDto.cs" company="RHEA System S.A.">
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

namespace CDP4.COMET.HERMES.Models.DTO
{
    using CDP4.COMET.HERMES.Models.Enums;

    using CDP4Common.CommonData;

    /// <summary>
    /// A DTO class responsible of holding information of a diff for a particular item
    /// </summary>
    public class DiffItemDto
    {
        /// <summary>
        /// The <see cref="DifferenceLevel"/> of this particular item
        /// </summary>
        public DifferenceLevel? DifferenceLevel { get; set; }
        /// <summary>
        /// The <see cref="Thing"/> item that holds all of the data
        /// </summary>
        public Thing Item { get; set; }
        /// <summary>
        /// The named of the thing, if the <see cref="Item"/> implements an <see cref="INamedThing"/>
        /// </summary>
        public string Name => this.Item != null ? ((INamedThing)this.Item).Name : "";
    }
}