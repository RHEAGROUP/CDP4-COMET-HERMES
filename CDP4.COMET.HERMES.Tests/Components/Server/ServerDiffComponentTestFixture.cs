// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ServerDiffComponentTestFixture.cs" company="RHEA System S.A.">
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

    using CDP4Common.SiteDirectoryData;

    using global::COMET.Web.Common.Test.Helpers;

    using NUnit.Framework;

    using TestContext = Bunit.TestContext;

    [TestFixture]
    public class ServerDiffComponentTestFixture
    {
        private TestContext context;
        private IRenderedComponent<ServerDiffComponent> component;

        [SetUp]
        public void Setup()
        {
            this.context = new TestContext();
            this.context.ConfigureDevExpressBlazor();

            var commonGuid = Guid.NewGuid();
            var sourceThings = new[] { new List<DomainOfExpertise> { new() { Iid = commonGuid, Name = "Domain1" }, new() { Iid = Guid.NewGuid(), Name = "Domain2" } } };
            var targetThings = new[] { new List<DomainOfExpertise> { new() { Iid = commonGuid, Name = "Domain1" }, new() { Iid = Guid.NewGuid(), Name = "Domain2-Target" } } };

            this.component = this.context.RenderComponent<ServerDiffComponent>(parameters =>
            {
                parameters.Add(p => p.SourceThings, sourceThings);
                parameters.Add(p => p.TargetThings, targetThings);
            });
        }

        [Test]
        public void VerifyComponent()
        {
            var content = this.component.FindAll(".dxbl-treeview-items-container");
            Assert.That(content.Count, Is.EqualTo(2));

            var domainOfExpertiseContainer = content[1];

            Assert.Multiple(() =>
            {
                Assert.That(domainOfExpertiseContainer.TextContent.Trim(), Is.EqualTo("DomainOfExpertise (+1, =1)"));
                Assert.That(this.component.Instance.ShowAllResults, Is.False);
            });

            this.component.Instance.ShowAllResults = true;
            this.component.Render();

            domainOfExpertiseContainer = this.component.FindAll(".dxbl-treeview-items-container")[1];

            Assert.Multiple(() =>
            {
                Assert.That(domainOfExpertiseContainer, Is.Not.Null);
                Assert.That(domainOfExpertiseContainer.TextContent.Trim(), Is.EqualTo("DomainOfExpertise (+1, =2)"));
                Assert.That(this.component.Instance.ShowAllResults, Is.True);
            });
        }
    }
}
