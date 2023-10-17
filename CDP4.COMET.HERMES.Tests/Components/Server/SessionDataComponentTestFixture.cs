// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SessionDataComponentTestFixture.cs" company="RHEA System S.A.">
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
    using Bunit;

    using CDP4.COMET.HERMES.Components.Server;

    using CDP4Common.CommonData;
    using CDP4Common.SiteDirectoryData;

    using CDP4Dal;

    using global::COMET.Web.Common.Test.Helpers;

    using Moq;

    using NUnit.Framework;
    using TestContext = Bunit.TestContext;

    [TestFixture]
    public class SessionDataComponentTestFixture
    {
        private TestContext context;
        private IRenderedComponent<SessionDataComponent> component;
        private bool clickedOnItem;
        private Mock<ISession> mockSession;
        
        [SetUp]
        public void Setup()
        {
            this.context = new TestContext();
            this.context.ConfigureDevExpressBlazor();
            this.mockSession = new Mock<ISession>();
            var siteDirectory = new SiteDirectory();
            siteDirectory.Domain.Add(new DomainOfExpertise {  Name = "DomainName1" });
            this.mockSession.Setup(x => x.RetrieveSiteDirectory()).Returns(siteDirectory);
            
            Action<Thing> action = (_) =>
            {
                this.clickedOnItem = true;
            };
            this.component = this.context.RenderComponent<SessionDataComponent>(parameters =>
            {
                parameters.Add(p => p.OnClick, action);
                parameters.Add(p => p.Session, this.mockSession.Object);
            });
        }

        [Test]
        public void VerifyComponent()
        {
            var containerTitles = this.component.FindAll(".dxbl-treeview-item-container");
            Assert.That(containerTitles.Count, Is.EqualTo(2));

            var domainOfExpertiseTitle = containerTitles[0];
            var siteDirectoryTitle = containerTitles[1];

            Assert.Multiple(() =>
            {
                Assert.That(domainOfExpertiseTitle.TextContent.Trim(), Is.EqualTo("Domains of Expertise"));
                Assert.That(siteDirectoryTitle.TextContent.Trim(), Is.EqualTo("Site Directory"));
                Assert.That(this.clickedOnItem, Is.False);
            });
            
            var btnElement = this.component.Find(".dxbl-btn");
            Assert.That(btnElement, Is.Not.Null);
            btnElement.Click();
            
            var content = this.component.FindAll(".dxbl-treeview-items-container");
            Assert.That(content.Count, Is.EqualTo(3));

            var sourceData = this.component.FindAll(".source-data");
            Assert.That(sourceData.Count, Is.EqualTo(1));

            var firstItem = sourceData[0];

            Assert.Multiple(() =>
            {
                Assert.That(firstItem.TextContent, Is.EqualTo("DomainName1"));
                Assert.That(this.clickedOnItem, Is.False);
            });

            firstItem.Click();
            Assert.That(this.clickedOnItem, Is.True);
        }
    }
}