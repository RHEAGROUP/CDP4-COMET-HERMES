// --------------------------------------------------------------------------------------------------------------------
// <copyright file="VersionServiceTestFixture.cs" company="RHEA System S.A.">
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

namespace CDP4.COMET.HERMES.Tests.Services.VersionService
{
    using CDP4.COMET.HERMES.Services.VersionService;

    using NUnit.Framework;

    [TestFixture]
    public class VersionServiceTestFixture
    {
        private VersionService versionService;

        [SetUp]
        public void SetUp()
        {
            this.versionService = new VersionService();
        }

        [Test]
        public void VerifyThatVersionServiceReturnsFourDigitVersion()
        {
            var version = this.versionService.GetVersion();

            var periodCount = version.ToCharArray().Count(c => c == '.');

            Assert.That(periodCount, Is.EqualTo(3));

            var versionSplitted = version.Split('.');

            foreach (var versionField in versionSplitted)
            {
                Assert.That(() => int.Parse(versionField), Throws.Nothing);
            }
        }
    }
}