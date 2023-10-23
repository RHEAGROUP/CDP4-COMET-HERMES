// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SourceDataComponentTestFixture.cs" company="RHEA System S.A.">
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

namespace CDP4.COMET.HERMES.Tests.Components.Server
{
    using AngleSharp.Dom;

    using Bunit;

    using CDP4.COMET.HERMES.Components.Server;

    using CDP4Common.CommonData;
    using CDP4Common.SiteDirectoryData;

    using global::COMET.Web.Common.Test.Helpers;

    using NUnit.Framework;

    using TestContext = Bunit.TestContext;

    [TestFixture]
    public class SourceDataComponentTestFixture
    {
        private TestContext context;
        private IRenderedComponent<SourceDataComponent<Thing>> component;
        private bool clickedOnItem;

        [SetUp]
        public void Setup()
        {
            this.context = new TestContext();
            this.context.ConfigureDevExpressBlazor();

            var things = new List<Thing>
            {
                new DomainOfExpertise
                {
                    Name = "DomainTest1",
                    ShortName = "DomainTest1"
                },
                new DomainOfExpertise
                {
                    Name = "DomainTest2",
                    ShortName = "DomainTest2"
                },
            };

            Action<Thing> action = (_) => { this.clickedOnItem = true; };

            this.component = this.context.RenderComponent<SourceDataComponent<Thing>>(parameters =>
            {
                parameters.Add(p => p.Expanded, true);
                parameters.Add(p => p.Title, "ComponentTitle");
                parameters.Add(p => p.Source, things);
                parameters.Add(p => p.OnClick, action);
            });
        }

        [TearDown]
        public void Teardown()
        {
            this.context.CleanContext();
        }

        [Test]
        public void VerifyComponent()
        {
            var titleElement = this.component.Find(".dxbl-treeview-item-text");

            Assert.Multiple(() =>
            {
                Assert.That(titleElement, Is.Not.Null);
                Assert.That(titleElement.Text(), Is.EqualTo("ComponentTitle"));
            });

            var btnElement = this.component.Find(".dxbl-btn");
            Assert.That(btnElement, Is.Not.Null);
            btnElement.Click();

            var content = this.component.FindAll(".dxbl-treeview-items-container");
            Assert.That(content.Count, Is.EqualTo(2));

            var sourceData = this.component.FindAll(".source-data");
            Assert.That(sourceData.Count, Is.EqualTo(2));

            var firstItem = sourceData[0];

            Assert.Multiple(() =>
            {
                Assert.That(firstItem.TextContent, Is.EqualTo("DomainTest1"));
                Assert.That(this.clickedOnItem, Is.False);
            });

            firstItem.Click();
            Assert.That(this.clickedOnItem, Is.True);
        }
    }
}
