// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ServerComponentTestFixture.cs" company="RHEA System S.A.">
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
    using CDP4.COMET.HERMES.ViewModels.Components.Server.Interfaces;

    using CDP4Common.SiteDirectoryData;

    using global::COMET.Web.Common.Model.DTO;
    using global::COMET.Web.Common.Test.Helpers;

    using Microsoft.Extensions.DependencyInjection;

    using Moq;

    using NUnit.Framework;
    using TestContext = Bunit.TestContext;

    [TestFixture]
    public class ServerComponentTestFixture
    {
        private TestContext context;
        private IRenderedComponent<ServerComponent> component;
        private Mock<ISyncViewModel> mockSyncViewModel;
        private Mock<IServerViewModel> mockServerViewModel;

        [SetUp]
        public void SetUp()
        {
            this.context = new TestContext();
            this.context.ConfigureDevExpressBlazor();
            this.mockSyncViewModel = new Mock<ISyncViewModel>();
            this.mockServerViewModel = new Mock<IServerViewModel>();
            this.mockServerViewModel.Setup(x => x.AuthenticationDto).Returns(new AuthenticationDto());
            this.context.Services.AddSingleton(this.mockSyncViewModel.Object);
            this.context.Services.AddSingleton(this.mockServerViewModel.Object);

            this.component = this.context.RenderComponent<ServerComponent>();
        }

        [Test]
        public void GetThingsFromSiteDirectoryTest()
        {
            var emptySiteDirectory = ServerComponent.GetThingsFromSiteDirectory(null);
            Assert.That(emptySiteDirectory, Is.Empty);
            var siteDirectory = new SiteDirectory();
            siteDirectory.Domain.Add(new DomainOfExpertise {  Name = "DomainName1" });
            var nonEmptySiteDirectory = ServerComponent.GetThingsFromSiteDirectory(siteDirectory);
            Assert.That(nonEmptySiteDirectory, Is.Not.Empty);
        }
    }
}