// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SessionDiffComponent.razor.cs" company="RHEA System S.A.">
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
    using System.Collections;

    using CDP4.COMET.HERMES.Models.DTO;
    using CDP4.COMET.HERMES.Models.Enums;

    using CDP4Common.CommonData;

    using global::COMET.Web.Common.Model.DTO;

    using Microsoft.AspNetCore.Components;

    /// <summary>
    /// A class responsible for display the difference between two different session objects
    /// </summary>
    public partial class ServerDiffComponent
    {
        /// <summary>
        /// The source session from where we're importing the data
        /// </summary>
        [Parameter]
        public IEnumerable<IEnumerable<Thing>> SourceThings { get; set; }

        /// <summary>
        /// The target sessesion to where we're exporting the data to
        /// </summary>
        [Parameter]
        public IEnumerable<IEnumerable<Thing>> TargetThings { get; set; }
        
        /// <summary>
        /// A toggle property that says if we're only showing the different results or all of them
        /// </summary>
        public bool ShowAllResults { get; set; } = false;
        
        /// <summary>
        /// Method invoked when the component is ready to start, having received its
        /// initial parameters from its parent in the render tree.
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();
            this.GetDiffItemDtos();
        }

        /// <summary>
        /// Calculate the level of difference between a source thing and a target thing
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <returns>
        /// <see cref="DifferenceLevel.Equal"/> when the two things are completely equal <br/>
        /// <see cref="DifferenceLevel.PartiallyDifferent"/> when the two things have equal Iids but their Alias, Hyperlinks and Definitions do not match <br/>
        /// <see cref="DifferenceLevel.CompletelyDifferent"/> when the two things are completely different
        /// </returns>
        private static DifferenceLevel GetDifferenceDegree(Thing source, Thing target)
        {
            var sourceDefinedThing = source as DefinedThing;
            var targetDefinedThing = target as DefinedThing;
            var isEqualIid = source.Iid == target.Iid;
            if (!isEqualIid && (sourceDefinedThing == null || targetDefinedThing == null))
            {
                return DifferenceLevel.CompletelyDifferent;
            }

            if (isEqualIid && (sourceDefinedThing != null && targetDefinedThing != null))
            {

                var hasEqualAlias = sourceDefinedThing.Alias.All(sourceAlias => targetDefinedThing.Alias.Any(targetAlias => targetAlias.Iid.Equals(sourceAlias.Iid)));
                var hasEqualHyperlinks = sourceDefinedThing.HyperLink.All(sourceHyperlink => targetDefinedThing.HyperLink.Any(targetHyperlink => targetHyperlink.Iid.Equals(sourceHyperlink.Iid)));
                var hasEqualDefinition = sourceDefinedThing.Definition.All(sourceDefinition => targetDefinedThing.Definition.Any(targetDefinition => targetDefinition.Iid.Equals(sourceDefinition.Iid)));

                if (hasEqualAlias && hasEqualHyperlinks && hasEqualDefinition)
                {
                    return DifferenceLevel.Equal;
                }

                if (hasEqualAlias || hasEqualHyperlinks || hasEqualDefinition)
                {
                    return DifferenceLevel.PartiallyDifferent;
                }
            }

            return DifferenceLevel.CompletelyDifferent;
        }

        /// <summary>
        /// Gets all of the items from the target server plus the new items being added
        /// </summary>
        /// <returns></returns>
        private Dictionary<string, IEnumerable<DiffItemDto>> GetAllItems()
        {
            var dict = new Dictionary<string, IEnumerable<DiffItemDto>>();

            foreach (var target in this.TargetThings)
            {
                var targetType = target.GetType();
                var source = this.SourceThings.FirstOrDefault(x => x.GetType() == targetType)?.ToList() ?? new List<Thing>();

                var diffItemDtos = target.Select(targetThing =>
                {
                    var diffItemDto = new DiffItemDto();
                    var itemInSource = source?.FirstOrDefault(sourceThing => sourceThing.Iid.Equals(targetThing.Iid));
                    diffItemDto.Item = targetThing;
                    diffItemDto.DifferenceLevel = itemInSource == null ? DifferenceLevel.Equal : GetDifferenceDegree(targetThing, itemInSource);
                    return diffItemDto;
                }).ToList();
                
                diffItemDtos.AddRange(source.Where(x => !target.Any(c => c.Iid == x.Iid)).Select(x =>
                {
                    var diffItemDto = new DiffItemDto
                    {
                        Item = x,
                        DifferenceLevel = DifferenceLevel.CompletelyDifferent
                    };
                    return diffItemDto;
                }));

                if (!diffItemDtos.Any())
                {
                    continue;
                }
                
                var targetName = target.FirstOrDefault();
                var typeName = targetName != null ? target?.FirstOrDefault()?.GetType().Name : source?.FirstOrDefault()?.GetType().Name;

                if (!string.IsNullOrEmpty(typeName))
                { 
                    dict.Add(typeName, diffItemDtos);
                }
            }

            return dict;
        }

        /// <summary>
        /// Gets the diff amount based on the difference levels of the <see cref="DiffItemDto"/> on the list
        /// </summary>
        /// <param name="diffItemDtos">The list of DiffItems</param>
        /// <returns></returns>
        private static string GetDiffValues(IEnumerable<DiffItemDto> diffItemDtos)
        {
            if (!diffItemDtos.Any())
            {
                return "";
            }
            var differentItems = diffItemDtos.Count(x => x.DifferenceLevel is DifferenceLevel.CompletelyDifferent or DifferenceLevel.PartiallyDifferent);
            var equalItemsCount = diffItemDtos.Count(x => x.DifferenceLevel is DifferenceLevel.Equal);

            var result = "";

            if (differentItems > 0)
            {
                result += $"+{differentItems}";
            }

            if (equalItemsCount > 0)
            {
                result += $", ={equalItemsCount}";
            }

            return result;
        }
        
        /// <summary>
        /// Enumerates through the list of selected values and calculate a corresponding difference level per item
        /// </summary>
        /// <returns>
        /// A KeyValuePair list of items where Key is equal to the type name and the Value is list of <see cref="DiffItem"/>
        /// </returns>
        private Dictionary<string, IEnumerable<DiffItemDto>> GetDiffItemDtos()
        {
            var dict = new Dictionary<string, IEnumerable<DiffItemDto>>();
            
            foreach (var source in this.SourceThings)
            {
                var sourceType = source.GetType();
                var target = this.TargetThings.Select(x => x).FirstOrDefault(x => x.GetType() == sourceType)?.ToList();
                
                if (target == null || !target.Any())
                {
                    continue;
                }
                
                var diff = source.Select(sourceThing =>
                {
                    var itemInTarget = target.FirstOrDefault(targetThing => targetThing.Iid.Equals(sourceThing.Iid));
                    var diffItemDto = new DiffItemDto
                    {
                        Item = sourceThing,
                        DifferenceLevel = itemInTarget == null ? DifferenceLevel.CompletelyDifferent : GetDifferenceDegree(sourceThing, itemInTarget)
                    };

                    return diffItemDto;
                }).ToList();
                
                if (!diff.Any())
                {
                    continue;
                }

                var typeName = source.FirstOrDefault()?.GetType().Name;

                if (!string.IsNullOrEmpty(typeName))
                {
                    dict.Add(typeName, diff);
                }
            }

            return dict;
        }

        /// <summary>
        /// Returns the list of available diff items
        /// </summary>
        /// <returns>
        /// If <see cref="ShowAllResults"/> is true, return all items (target + diff items)
        /// If <see cref="ShowAllResults"/> is false, return only the diff items
        /// </returns>
        private Dictionary<string, IEnumerable<DiffItemDto>> GetData()
        {
            return this.ShowAllResults ? this.GetAllItems() : this.GetDiffItemDtos();
        }
    }
}