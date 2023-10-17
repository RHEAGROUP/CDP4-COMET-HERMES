// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ServerAuthenticationComponentTestFixture.cs" company="RHEA System S.A.">
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
    using CDP4.COMET.HERMES.Models.Enums;
    using CDP4.COMET.HERMES.ViewModels.Components.Server;
    using CDP4.COMET.HERMES.ViewModels.Components.Server.Interfaces;

    using CDP4Common.SiteDirectoryData;

    using CDP4Dal;

    using global::COMET.Web.Common.Model.DTO;
    using global::COMET.Web.Common.Test.Helpers;

    using Microsoft.AspNetCore.Components.Forms;
    using Microsoft.AspNetCore.Components.Web;
    using Microsoft.Extensions.DependencyInjection;

    using Moq;

    using NUnit.Framework;
    
    using TestContext = Bunit.TestContext;
    
    [TestFixture]
    public class ServerAuthenticationComponentTestFixture
    {
        private TestContext context;
        private Mock<ISession> sessionMock;
        private IServerViewModel serverViewModel;
        
        [SetUp]
        public void Setup()
        {
            this.context = new TestContext();
            this.sessionMock = new Mock<ISession>();
            this.sessionMock.Setup(x => x.Open(It.IsAny<bool>())).Returns(Task.CompletedTask);
            this.sessionMock.Setup(x => x.RetrieveSiteDirectory()).Returns(new SiteDirectory());
            this.serverViewModel = new ServerViewModel();
            this.serverViewModel.Session = this.sessionMock.Object;
            this.context.Services.AddSingleton(this.serverViewModel);
            this.context.ConfigureDevExpressBlazor();
        }
        
        [TearDown]
        public void Teardown()
        {
            this.context.CleanContext();
        }
        
        [Test]
        public async Task VerifyErrorsShown()
        {
            var renderer = this.context.RenderComponent<ServerAuthenticationComponent>();
            var errorsElement = renderer.Find(".validation-errors");
            var numberOfRequiredFieldsInFirstLoginTry = renderer.Instance.FieldsFocusedStatus.Count - 1;

            Assert.That(errorsElement.InnerHtml, Is.Empty);

            await renderer.Find("button").ClickAsync(new MouseEventArgs());
            Assert.That(errorsElement.ChildElementCount, Is.EqualTo(numberOfRequiredFieldsInFirstLoginTry));

            var sourceAddressField = renderer.Find("input");
            await sourceAddressField.FocusAsync(new FocusEventArgs());

            Assert.Multiple(() =>
            {
                Assert.That(renderer.Instance.FieldsFocusedStatus["SourceAddress"], Is.True);
                Assert.That(errorsElement.ChildElementCount, Is.EqualTo(numberOfRequiredFieldsInFirstLoginTry));
            });

            await sourceAddressField.BlurAsync(new FocusEventArgs());

            Assert.Multiple(() =>
            {
                Assert.That(renderer.Instance.FieldsFocusedStatus["SourceAddress"], Is.False);
                Assert.That(errorsElement.ChildElementCount, Is.EqualTo(numberOfRequiredFieldsInFirstLoginTry));
            });
        }
        
        [Test]
        public void VerifyFocusingAndBluring()
        {
            var renderer = this.context.RenderComponent<ServerAuthenticationComponent>();

            Assert.That(renderer.Instance.FieldsFocusedStatus, Is.EqualTo(new Dictionary<string, bool>()
            {
                { "SourceAddress", false },
                { "UserName", false },
                { "Password", false }
            }));

            const string fieldToFocusOn = "UserName";
            Assert.That(renderer.Instance.FieldsFocusedStatus[fieldToFocusOn], Is.False);
            renderer.Instance.HandleFieldFocus(fieldToFocusOn);
            
            Assert.Multiple(()=>
            {
                foreach (var fieldStatus in renderer.Instance.FieldsFocusedStatus)
                {
                    Assert.That(fieldStatus.Value, fieldStatus.Key == fieldToFocusOn ? Is.True : Is.False);
                }
            });

            renderer.Instance.HandleFieldBlur(fieldToFocusOn);

            Assert.Multiple(() =>
            {
                foreach (var fieldStatus in renderer.Instance.FieldsFocusedStatus)
                {
                    Assert.That(fieldStatus.Value, Is.False);
                }
            });
        }
        
        [Test]
        public async Task VerifyPerformLogin()
        {
            var renderer = this.context.RenderComponent<ServerAuthenticationComponent>();
            var editForm = renderer.FindComponent<EditForm>();

            Assert.That(renderer.Instance.FieldsFocusedStatus, Is.EqualTo(new Dictionary<string, bool>()
            {
                { "SourceAddress", false },
                { "UserName", false },
                { "Password", false }
            }));

            await renderer.InvokeAsync(editForm.Instance.OnValidSubmit.InvokeAsync);

            Assert.Multiple(() =>
            {
                Assert.That(renderer.Instance.ErrorMessage, Is.Not.Null);
            });

            await renderer.InvokeAsync(editForm.Instance.OnValidSubmit.InvokeAsync);

            Assert.Multiple(() =>
            {
                Assert.That(renderer.Instance.ErrorMessage, Is.Not.Null);
            });

            this.serverViewModel.AuthenticationDto = new AuthenticationDto
            {
                UserName = "user",
                Password = "pass"
            };
            
            await renderer.InvokeAsync(editForm.Instance.OnValidSubmit.InvokeAsync);
            
            Assert.Multiple(() =>
            {
                Assert.That(renderer.Instance.ErrorMessage, Is.Not.Null);
                Assert.That(this.serverViewModel.AuthenticationState, Is.EqualTo(AuthenticationStateKind.Fail));
            });
            
            this.serverViewModel.AuthenticationDto = new AuthenticationDto
            {
                SourceAddress = "http://localhost",
                UserName = "user",
                Password = "pass"
            };
            
            await renderer.InvokeAsync(editForm.Instance.OnValidSubmit.InvokeAsync);

            Assert.Multiple(() =>
            {
                Assert.That(renderer.Instance.ErrorMessage, Is.Empty);
                Assert.That(this.serverViewModel.AuthenticationState, Is.EqualTo(AuthenticationStateKind.Success));
            });
        }
    }
}