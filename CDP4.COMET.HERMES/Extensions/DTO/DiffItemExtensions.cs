// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DiffItemExtensions.cs" company="RHEA System S.A.">
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

namespace CDP4.COMET.HERMES.Extensions.DTO
{
    using CDP4.COMET.HERMES.Models.DTO;
    using CDP4.COMET.HERMES.Models.Enums;

    using global::COMET.Web.Common.Model.DTO;

    /// <summary>
    /// Extensions class for <see cref="DiffItemDto"/>
    /// </summary>
    public static class DiffItemExtensions
    {
        /// <summary>
        /// Returns a CSS class depending on the difference level of the DiffItem
        /// </summary>
        /// <param name="diffItemDto"></param>
        /// <returns>
        /// If <see cref="DifferenceLevel.PartiallyDifferent"/> returns partially-different
        /// If <see cref="DifferenceLevel.CompletelyDifferent"/> returns completely-different
        /// If <see cref="DifferenceLevel.Equal"/> returns equal
        /// </returns>
        public static string GetDiffItemDtoCssClassByDifferenceLevel(this DiffItemDto diffItemDto) =>
            diffItemDto.DifferenceLevel switch
            {
                DifferenceLevel.PartiallyDifferent => "partially-different",
                DifferenceLevel.CompletelyDifferent => "completely-different",
                DifferenceLevel.Equal => "equal",
                _ => ""
            };
    }
}
