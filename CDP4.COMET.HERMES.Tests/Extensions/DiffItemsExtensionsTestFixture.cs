// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DiffItemsExtensionsTestFixture.cs" company="RHEA System S.A.">
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

namespace CDP4.COMET.HERMES.Tests.Extensions
{
    using CDP4.COMET.HERMES.Extensions.DTO;
    using CDP4.COMET.HERMES.Models.DTO;
    using CDP4.COMET.HERMES.Models.Enums;

    using CDP4Common.SiteDirectoryData;

    using NUnit.Framework;

    [TestFixture]
    public class DiffItemsExtensionsTestFixture
    {
        private DiffItemDto diffItemDto;
        
        [SetUp]
        public void Setup()
        {
            this.diffItemDto = new DiffItemDto
            {
                DifferenceLevel = DifferenceLevel.Equal,
                Item = new DomainOfExpertise { Name = "DomainTest" }
            };
        }

        [Test]
        public void VerifyExtension()
        {
            Assert.Multiple(() =>
            {
                Assert.That(this.diffItemDto.Item, Is.Not.Null);
                Assert.That(this.diffItemDto.Name, Is.EqualTo("DomainTest"));
                Assert.That(this.diffItemDto.GetDiffItemDtoCssClassByDifferenceLevel(), Is.EqualTo("equal"));
            });

            this.diffItemDto.DifferenceLevel = DifferenceLevel.CompletelyDifferent;
            Assert.That(this.diffItemDto.GetDiffItemDtoCssClassByDifferenceLevel(), Is.EqualTo("completely-different"));
            this.diffItemDto.DifferenceLevel = DifferenceLevel.PartiallyDifferent;
            Assert.That(this.diffItemDto.GetDiffItemDtoCssClassByDifferenceLevel(), Is.EqualTo("partially-different"));
            this.diffItemDto.DifferenceLevel = null;
            Assert.That(this.diffItemDto.GetDiffItemDtoCssClassByDifferenceLevel(), Is.EqualTo(""));
        }
    }
}