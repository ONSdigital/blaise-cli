using Blaise.Cli.Core.Interfaces;
using Blaise.Cli.Core.Services;
using Blaise.Nuget.Api.Contracts.Interfaces;
using Moq;
using NUnit.Framework;
using StatNeth.Blaise.API.DataInterface;
using System;

namespace Blaise.Cli.Tests.Unit.Services
{
    public class BlaiseFileServiceTests
    {
        private Mock<IBlaiseFileApi> _blaiseFileApi;
        private IBlaiseFileService _sut;

        private readonly string _serverParkName = "gusty";
        private readonly string _questionnaireName = "OPN2101A";
        private readonly string _fileName = "OPN2101A.bpkg";
        private readonly bool _auditOptions = false;

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

            Assert.IsNotNull(exception);
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

            Assert.IsNotNull(exception);
            Assert.AreEqual("Value cannot be null.\r\nParameter name: fileName", exception.Message);
        }

        [Test]
        public void Given_We_Have_Passed_Valid_Parameters_When_We_Call_UpdateQuestionnairePackageWithData_With_No_Options_Then_The_Correct_API_Call_Is_Made()
        {
            //act
            _sut.UpdateQuestionnairePackageWithData(_serverParkName, _questionnaireName, _fileName);

            //assert
            _blaiseFileApi.Verify(b => b.UpdateQuestionnaireFileWithData(_serverParkName, _questionnaireName, _fileName, false), Times.Once);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void Given_We_Have_Passed_Valid_Parameters_When_We_Call_UpdateQuestionnairePackageWithData_With_No_Audit_Options_Then_The_Correct_API_Call_Is_Made(bool auditOption)
        {
            //act
            _sut.UpdateQuestionnairePackageWithData(_serverParkName, _questionnaireName, _fileName, auditOption);

            //assert
            _blaiseFileApi.Verify(b => b.UpdateQuestionnaireFileWithData(_serverParkName, _questionnaireName, _fileName, auditOption), Times.Once);
            _blaiseFileApi.Verify(b => b.UpdateQuestionnaireFileWithBatchedData(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<bool>()), Times.Never);
        }

        [TestCase(true, 10)]
        [TestCase(false, 20)]
        public void Given_We_Have_Passed_Valid_Parameters_When_We_Call_UpdateQuestionnairePackageWithData_With_Batch_Size_Options_Then_The_Correct_API_Call_Is_Made(bool auditOption, int batchSize)
        {
            //act
            _sut.UpdateQuestionnairePackageWithData(_serverParkName, _questionnaireName, _fileName, auditOption, batchSize);

            //assert
            _blaiseFileApi.Verify(b => b.UpdateQuestionnaireFileWithBatchedData(_serverParkName, _questionnaireName, _fileName, batchSize, auditOption), Times.Once);
            _blaiseFileApi.Verify(b => b.UpdateQuestionnaireFileWithData(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>()), Times.Never);
        }

        [Test]
        public void Given_We_Have_Passed_Valid_Parameters_When_We_Call_UpdateQuestionnairePackageWithData_Then_The_Correct_API_Call_Is_Made()
        {
            //act
            _sut.UpdateQuestionnairePackageWithData(_serverParkName, _questionnaireName, _fileName, _auditOptions);

            //assert
            _blaiseFileApi.Verify(b => b.UpdateQuestionnaireFileWithData(_serverParkName, _questionnaireName, _fileName, _auditOptions), Times.Once);
        }

        [Test]
        public void Given_We_Have_Called_UpdateQuestionnairePackageWithData_When_We_have_supplied_An_Empty_ServerParkName_Then_An_Error_Is_Thrown()
        {
            //act && assert
            var exception = Assert.Throws<ArgumentException>(() => _sut.UpdateQuestionnairePackageWithData(string.Empty, _questionnaireName, _fileName));

            Assert.IsNotNull(exception);
            Assert.AreEqual("A value for the argument 'serverParkName' must be supplied", exception.Message);
        }

        [Test]
        public void Given_We_Have_Called_UpdateQuestionnairePackageWithData_When_We_have_supplied_A_Null_Value_For_ServerParkName_Then_An_Error_Is_Thrown()
        {
            //act && assert
            var exception = Assert.Throws<ArgumentNullException>(() => _sut.UpdateQuestionnairePackageWithData(null, _questionnaireName, _fileName));

            Assert.IsNotNull(exception);
            Assert.AreEqual("Value cannot be null.\r\nParameter name: serverParkName", exception.Message);
        }

        [Test]
        public void Given_We_Have_Called_UpdateQuestionnairePackageWithData_When_We_have_supplied_An_Empty_QuestionnaireName_Then_An_Error_Is_Thrown()
        {
            //act && assert
            var exception = Assert.Throws<ArgumentException>(() => _sut.UpdateQuestionnairePackageWithData(_serverParkName, string.Empty, _fileName));

            Assert.IsNotNull(exception);
            Assert.AreEqual("A value for the argument 'questionnaireName' must be supplied", exception.Message);
        }

        [Test]
        public void Given_We_Have_Called_UpdateQuestionnairePackageWithData_When_We_have_supplied_A_Null_Value_For_QuestionnaireName_Then_An_Error_Is_Thrown()
        {
            //act && assert
            var exception = Assert.Throws<ArgumentNullException>(() => _sut.UpdateQuestionnairePackageWithData(_serverParkName, null, _fileName));

            Assert.IsNotNull(exception);
            Assert.AreEqual("Value cannot be null.\r\nParameter name: questionnaireName", exception.Message);
        }

        [Test]
        public void Given_We_Have_Called_UpdateQuestionnairePackageWithData_When_We_have_supplied_An_Empty_FileName_Then_An_Error_Is_Thrown()
        {
            //act && assert
            var exception = Assert.Throws<ArgumentException>(() => _sut.UpdateQuestionnairePackageWithData(_serverParkName, _questionnaireName, string.Empty));

            Assert.IsNotNull(exception);
            Assert.AreEqual("A value for the argument 'fileName' must be supplied", exception.Message);
        }

        [Test]
        public void Given_We_Have_Called_UpdateQuestionnairePackageWithData_When_We_have_supplied_A_Null_Value_For_FileName_Then_An_Error_Is_Thrown()
        {
            //act && assert
            var exception = Assert.Throws<ArgumentNullException>(() => _sut.UpdateQuestionnairePackageWithData(_serverParkName, _questionnaireName, null));

            Assert.IsNotNull(exception);
            Assert.AreEqual("Value cannot be null.\r\nParameter name: fileName", exception.Message);
        }

        [Test]
        public void Given_We_Have_Passed_Valid_Parameters_When_We_Call_UpdateQuestionnaireFileWithSqlConnection_Then_The_Correct_API_Call_Is_Made()
        {
            //act
            _sut.UpdateQuestionnaireFileWithSqlConnection(_fileName);

            //assert
            _blaiseFileApi.Verify(b => b.UpdateQuestionnaireFileWithSqlConnection(_questionnaireName, _fileName), Times.Once);
        }

        [Test]
        public void Given_We_Have_Called_UpdateQuestionnaireFileWithSqlConnection_When_We_have_supplied_An_Empty_FileName_Then_An_Error_Is_Thrown()
        {
            //act && assert
            var exception = Assert.Throws<ArgumentException>(() => _sut.UpdateQuestionnaireFileWithSqlConnection(string.Empty));

            Assert.IsNotNull(exception);
            Assert.AreEqual("A value for the argument 'fileName' must be supplied", exception.Message);
        }

        [Test]
        public void Given_We_Have_Called_UpdateQuestionnaireFileWithSqlConnection_When_We_have_supplied_A_Null_Value_For_FileName_Then_An_Error_Is_Thrown()
        {
            //act && assert
            var exception = Assert.Throws<ArgumentNullException>(() => _sut.UpdateQuestionnaireFileWithSqlConnection(null));

            Assert.IsNotNull(exception);
            Assert.AreEqual("Value cannot be null.\r\nParameter name: fileName", exception.Message);
        }
    }
}
