using System;
using Blaise.Cli.Core.Interfaces;
using Blaise.Cli.Core.Services;
using Blaise.Nuget.Api.Contracts.Interfaces;
using Moq;
using NUnit.Framework;
using StatNeth.Blaise.API.DataInterface;

namespace Blaise.Cli.Tests.Unit.Services
{
    public class BlaiseFileServiceTests
    {
        private Mock<IBlaiseFileApi> _blaiseFileApi;
        private IBlaiseFileService _sut;

        [SetUp]
        public void SetupTests()
        {
            _blaiseFileApi = new Mock<IBlaiseFileApi>();
            _sut = new BlaiseFileService(_blaiseFileApi.Object);
        }

        [TestCase(ApplicationType.Cati)]
        [TestCase(ApplicationType.AuditTrail)]
        [TestCase(ApplicationType.Cari)]
        [TestCase(ApplicationType.Configuration)]
        [TestCase(ApplicationType.Meta)]
        [TestCase(ApplicationType.Session)]
        public void Given_We_Have_Passed_Valid_Parameters_When_We_Call_CreateDataInterfaceFile_Then_The_Correct_API_Call_Is_Made(ApplicationType applicationType)
        {
            //arrange
            var fileName = "file.ext";
            //act
            _sut.CreateDataInterfaceFile(applicationType, fileName);
            //assert
            _blaiseFileApi.Verify(b => b.CreateSettingsDataInterfaceFile(applicationType, fileName), Times.Once);
        }

        [TestCase(ApplicationType.Cati)]
        [TestCase(ApplicationType.AuditTrail)]
        [TestCase(ApplicationType.Cari)]
        [TestCase(ApplicationType.Configuration)]
        [TestCase(ApplicationType.Meta)]
        [TestCase(ApplicationType.Session)]
        public void Given_We_Have_Called_CreateDataInterface_When_We_have_supplied_An_Empty_FileName_Then_An_Error_Is_Thrown(ApplicationType applicationType)
        {
            //arrange
            var fileName = string.Empty;

            var exception = Assert.Throws<ArgumentException>(() => _sut.CreateDataInterfaceFile(applicationType,
                fileName));

            Assert.AreEqual("A value for the argument 'fileName' must be supplied", exception.Message);
        }

        [TestCase(ApplicationType.Cati)]
        [TestCase(ApplicationType.AuditTrail)]
        [TestCase(ApplicationType.Cari)]
        [TestCase(ApplicationType.Configuration)]
        [TestCase(ApplicationType.Meta)]
        [TestCase(ApplicationType.Session)]
        public void Given_We_Have_Called_CreateDataInterface_When_We_have_supplied_A_Null_Value_For_FileName_Then_An_Error_Is_Thrown(ApplicationType applicationType)
        {
            //arrange
            string fileName = null;

            var exception = Assert.Throws<ArgumentNullException>(() => _sut.CreateDataInterfaceFile(applicationType,
                fileName));

            Assert.AreEqual("Value cannot be null.\r\nParameter name: fileName", exception.Message);
        }
    }
}
