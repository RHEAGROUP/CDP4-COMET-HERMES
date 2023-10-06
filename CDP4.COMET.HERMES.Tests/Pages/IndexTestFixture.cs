// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IndexTestFixture.cs" company="RHEA System S.A.">
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

namespace CDP4.COMET.HERMES.Tests.Pages
{
    using Bunit;

    using CDP4.COMET.HERMES.Pages;
    using CDP4.COMET.HERMES.Services.VersionService;
    using CDP4.COMET.HERMES.ViewModels.Pages;

    using Microsoft.Extensions.DependencyInjection;

    using Moq;

    using NUnit.Framework;

    using TestContext = Bunit.TestContext;

    [TestFixture]
    public class IndexTestFixture
    {
        private TestContext context;
        private Mock<IIndexViewModel> indexViewModel;
        private Mock<IVersionService> versionService;

        [SetUp]
        public void Setup()
        {
            this.context = new TestContext();
            this.indexViewModel = new Mock<IIndexViewModel>();
            this.versionService = new Mock<IVersionService>();
            this.indexViewModel.Setup(x => x.VersionService).Returns(this.versionService.Object);
            this.versionService.Setup(x => x.GetVersion()).Returns("0.0.1.0");
            this.context.Services.AddSingleton(this.indexViewModel.Object);
        }

        [TearDown]
        public void Teardown()
        {
            this.context.Dispose();
        }

        [Test]
        public void VerifyIndexPage()
        {
            var renderer = this.context.RenderComponent<Index>();
            var footer = renderer.Find("footer");
            var versionDiv = footer.FirstChild!;
            Assert.That(versionDiv.TextContent, Contains.Substring(this.versionService.Object.GetVersion()));
        }
    }
}