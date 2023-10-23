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
        private string[] siteDirectoryNames;
        private string[] siteDirectoryTitles;

        [SetUp]
        public void Setup()
        {
            this.context = new TestContext();
            this.context.ConfigureDevExpressBlazor();
            this.mockSession = new Mock<ISession>();
            var siteDirectory = new SiteDirectory();
            siteDirectory.Domain.Add(new DomainOfExpertise { Name = "DomainName1" });
            siteDirectory.Organization.Add(new Organization { Name = "OrganizationName1" });
            siteDirectory.NaturalLanguage.Add(new NaturalLanguage { Name = "NaturalLanguage1" });
            siteDirectory.DomainGroup.Add(new DomainOfExpertiseGroup { Name = "DomainGroup1" });
            siteDirectory.Model.Add(new EngineeringModelSetup { Name = "Model1" });
            siteDirectory.ParticipantRole.Add(new ParticipantRole { Name = "ParticipantRole1" });
            siteDirectory.Person.Add(new Person() { GivenName = "PersonName1" });
            siteDirectory.PersonRole.Add(new PersonRole { Name = "PersonRole1" });
            siteDirectory.SiteReferenceDataLibrary.Add(new SiteReferenceDataLibrary { Name = "SiteReferenceDataLibrary1" });

            this.siteDirectoryNames = new[]
            {
                "DomainName1", "OrganizationName1", "NaturalLanguage1", "DomainGroup1", "Model1", "ParticipantRole1", "PersonName1 ", "PersonRole1", "SiteReferenceDataLibrary1"
            };

            //Site directory titles are present on the component
            this.siteDirectoryTitles = new[]
            {
                "Domains of Expertise", "Organizations", "Natural Languages", "Domain Groups", "Models", "Participant Roles", "Persons", "Person Roles",
                "Site Reference Data Library", "Site Directory"
            };

            this.mockSession.Setup(x => x.RetrieveSiteDirectory()).Returns(siteDirectory);

            Action<Thing> action = (_) => { this.clickedOnItem = true; };

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
            Assert.That(containerTitles.Count, Is.EqualTo(10));

            for (var i = 0; i < containerTitles.Count; i++)
            {
                var element = containerTitles[i];

                Assert.Multiple(() =>
                {
                    Assert.That(element, Is.Not.Null);
                    Assert.That(element.TextContent.Trim(), Is.EqualTo(this.siteDirectoryTitles[i]));
                    Assert.That(this.clickedOnItem, Is.False);
                });
            }

            var btnElements = this.component.FindAll(".dxbl-btn");
            Assert.That(btnElements.Count, Is.EqualTo(10));

            var content = this.component.FindAll(".dxbl-treeview-items-container");
            Assert.That(content.Count, Is.EqualTo(10));

            for (var i = 0; i < btnElements.Count - 1; i++)
            {
                var btnElement = btnElements[i];
                btnElement.Click();
                Assert.That(btnElement, Is.Not.Null);
                var sourceData = this.component.FindAll(".source-data");
                Assert.That(sourceData.Count, Is.EqualTo(1));
                var firstItem = sourceData[0];

                Assert.Multiple(() =>
                {
                    Assert.That(firstItem.TextContent, Is.EqualTo(this.siteDirectoryNames[i]));
                    Assert.That(this.clickedOnItem, Is.False);
                });

                firstItem.Click();
                Assert.That(this.clickedOnItem, Is.True);
                this.clickedOnItem = false;
                btnElement.Click();
            }
        }
    }
}
