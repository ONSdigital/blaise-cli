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

        private readonly string _serverParkName = "gusty";
        private readonly string _instrumentName = "OPN2101A";
        private readonly string _fileName = "OPN2101A.bpkg";

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
            //act
            _sut.CreateDataInterfaceFile(applicationType, _fileName);

            //assert
            _blaiseFileApi.Verify(b => b.CreateSettingsDataInterfaceFile(applicationType, _fileName), Times.Once);
        }

        [TestCase(ApplicationType.Cati)]
        [TestCase(ApplicationType.AuditTrail)]
        [TestCase(ApplicationType.Cari)]
        [TestCase(ApplicationType.Configuration)]
        [TestCase(ApplicationType.Meta)]
        [TestCase(ApplicationType.Session)]
        public void Given_We_Have_Called_CreateDataInterface_When_We_have_supplied_An_Empty_FileName_Then_An_Error_Is_Thrown(ApplicationType applicationType)
        {
            //act && assert
            var exception = Assert.Throws<ArgumentException>(() => _sut.CreateDataInterfaceFile(applicationType, string.Empty));

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
            //act && assert
            var exception = Assert.Throws<ArgumentNullException>(() => _sut.CreateDataInterfaceFile(applicationType, null));

            Assert.AreEqual("Value cannot be null.\r\nParameter name: fileName", exception.Message);
        }

        [Test]
        public void Given_We_Have_Passed_Valid_Parameters_When_We_Call_UpdateInstrumentPackageWithData_Then_The_Correct_API_Call_Is_Made()
        {
            //act
            _sut.UpdateInstrumentPackageWithData(_serverParkName, _instrumentName, _fileName);

            //assert
            _blaiseFileApi.Verify(b => b.UpdateInstrumentFileWithData(_serverParkName, _instrumentName, _fileName), Times.Once);
        }

        [Test]
        public void Given_We_Have_Called_UpdateInstrumentPackageWithData_When_We_have_supplied_An_Empty_ServerParkName_Then_An_Error_Is_Thrown()
        {
            //act && assert
            var exception = Assert.Throws<ArgumentException>(() => _sut.UpdateInstrumentPackageWithData(string.Empty, _instrumentName, _fileName));

            Assert.AreEqual("A value for the argument 'serverParkName' must be supplied", exception.Message);
        }

        [Test]
        public void Given_We_Have_Called_UpdateInstrumentPackageWithData_When_We_have_supplied_A_Null_Value_For_ServerParkName_Then_An_Error_Is_Thrown()
        {
            //act && assert
            var exception = Assert.Throws<ArgumentNullException>(() => _sut.UpdateInstrumentPackageWithData(null, _instrumentName, _fileName));

            Assert.AreEqual("Value cannot be null.\r\nParameter name: serverParkName", exception.Message);
        }

        [Test]
        public void Given_We_Have_Called_UpdateInstrumentPackageWithData_When_We_have_supplied_An_Empty_InstrumentName_Then_An_Error_Is_Thrown()
        {
            //act && assert
            var exception = Assert.Throws<ArgumentException>(() => _sut.UpdateInstrumentPackageWithData(_serverParkName, string.Empty, _fileName));

            Assert.AreEqual("A value for the argument 'instrumentName' must be supplied", exception.Message);
        }

        [Test]
        public void Given_We_Have_Called_UpdateInstrumentPackageWithData_When_We_have_supplied_A_Null_Value_For_InstrumentName_Then_An_Error_Is_Thrown()
        {
            //act && assert
            var exception = Assert.Throws<ArgumentNullException>(() => _sut.UpdateInstrumentPackageWithData(_serverParkName, null, _fileName));

            Assert.AreEqual("Value cannot be null.\r\nParameter name: instrumentName", exception.Message);
        }

        [Test]
        public void Given_We_Have_Called_UpdateInstrumentPackageWithData_When_We_have_supplied_An_Empty_FileName_Then_An_Error_Is_Thrown()
        {
            //act && assert
            var exception = Assert.Throws<ArgumentException>(() => _sut.UpdateInstrumentPackageWithData(_serverParkName, _instrumentName, string.Empty));

            Assert.AreEqual("A value for the argument 'fileName' must be supplied", exception.Message);
        }

        [Test]
        public void Given_We_Have_Called_UpdateInstrumentPackageWithData_When_We_have_supplied_A_Null_Value_For_FileName_Then_An_Error_Is_Thrown()
        {
            //act && assert
            var exception = Assert.Throws<ArgumentNullException>(() => _sut.UpdateInstrumentPackageWithData(_serverParkName, _instrumentName, null));

            Assert.AreEqual("Value cannot be null.\r\nParameter name: fileName", exception.Message);
        }
    }
}
