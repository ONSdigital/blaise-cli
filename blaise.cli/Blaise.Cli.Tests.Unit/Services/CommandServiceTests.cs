using Blaise.Cli.Core.Interfaces;
using Blaise.Cli.Core.Services;
using Moq;
using NUnit.Framework;
using StatNeth.Blaise.API.DataInterface;

namespace Blaise.Cli.Tests.Unit.Services
{
    public class CommandServiceTests
    {
        private Mock<IBlaiseFileService> _blaiseFileService;
        private ICommandService _sut;

        private readonly string _serverParkName = "gusty";
        private readonly string _questionnaireName = "OPN2101A";
        private readonly string _fileName = "OPN2101A.bpkg";
        private bool _auditOptions;

        [SetUp]
        public void SetupTests()
        {
            _blaiseFileService = new Mock<IBlaiseFileService>();
            _sut = new CommandService(_blaiseFileService.Object);
        }

        [Test]
        public void Given_We_Pass_DataDelivery_Arguments_When_We_Call_ParseArguments_Then_The_Correct_Method_Is_Called_With_The_Correct_Arguments()
        {
            //Arrange
            var args = new[] { "datadelivery", "-s", _serverParkName, "-q", _questionnaireName, "-f", _fileName, "-a", _auditOptions.ToString() };

            //Act
            _sut.ParseArguments(args);

            //Assert
            var audit = bool.Parse(_auditOptions.ToString());
            _blaiseFileService.Verify(b => b.UpdateQuestionnairePackageWithData(_serverParkName, _questionnaireName, _fileName, audit));
        }

        [Test]
        public void Given_We_Pass_DataDelivery_Audit_Arguments_When_We_Call_ParseArguments_Then_The_Correct_Method_Is_Called_With_The_Correct_Arguments()
        {
            //Arrange
            _auditOptions = true;
            var args = new[] { "datadelivery", "-s", _serverParkName, "-q", _questionnaireName, "-f", _fileName, "-a", _auditOptions.ToString() };

            //Act
            _sut.ParseArguments(args);

            //Assert
            var audit = bool.Parse(_auditOptions.ToString());
            _blaiseFileService.Verify(b => b.UpdateQuestionnairePackageWithData(_serverParkName, _questionnaireName, _fileName, audit));
        }

        [Test]
        public void Given_We_Pass_DataDelivery_Arguments_With_FullNames_When_We_Call_ParseArguments_Then_The_Correct_Method_Is_Called_With_The_Correct_Arguments()
        {
            //Arrange
            var args = new[] { "datadelivery", "--serverParkName", _serverParkName, "--questionnaireName", _questionnaireName, "--file", _fileName, "-a", _auditOptions.ToString() };
            //Act
            _sut.ParseArguments(args);

            //Assert
            _blaiseFileService.Verify(b => b.UpdateQuestionnairePackageWithData(_serverParkName, _questionnaireName, _fileName, false));
        }

        [Test]
        public void Given_We_Do_Not_Pass_A_ServerParkName_When_We_Call_ParseArguments_Then_Response_Of_1_is_returned()
        {
            //Arrange
            var args = new[] { "datadelivery", "--questionnaireName", _questionnaireName, "--file", _fileName, "-a", _auditOptions.ToString() };

            //Act
            var result = _sut.ParseArguments(args);

            //Assert
            Assert.AreEqual(1, result);
        }

        [Test]
        public void Given_We_Do_Not_Pass_An_QuestionnaireName_When_We_Call_ParseArguments_Then_Response_Of_1_is_returned()
        {
            //Arrange
            var args = new[] { "datadelivery", "--serverParkName", _serverParkName, "--file", _fileName, "-a", _auditOptions.ToString() };

            //Act
            var result = _sut.ParseArguments(args);

            //Assert
            Assert.AreEqual(1, result);
        }

        [Test]
        public void Given_We_Do_Not_Pass_A_File_When_We_Call_ParseArguments_Then_Response_Of_1_is_returned()
        {
            //Arrange
            var args = new[] { "datadelivery", "--serverParkName", _serverParkName, "--questionnaireName", _questionnaireName };

            //Act
            var result = _sut.ParseArguments(args);

            //Assert
            Assert.AreEqual(1, result);
        }

        [TestCase(ApplicationType.Cati)]
        [TestCase(ApplicationType.AuditTrail)]
        [TestCase(ApplicationType.Cari)]
        [TestCase(ApplicationType.Configuration)]
        [TestCase(ApplicationType.Meta)]
        [TestCase(ApplicationType.Session)]
        public void Given_We_Pass_DataInterface_Arguments_When_We_Call_ParseArguments_Then_The_Correct_Method_Is_Called_With_The_Correct_Arguments(ApplicationType applicationType)
        {
            //Arrange
            var args = new[] { "datainterface", "-t", $"{applicationType}", "-f", "file.ext" };

            //Act
            _sut.ParseArguments(args);

            //Assert
            _blaiseFileService.Verify(b => b.CreateDataInterfaceFile(applicationType, "file.ext"));
        }

        [TestCase(ApplicationType.Cati)]
        [TestCase(ApplicationType.AuditTrail)]
        [TestCase(ApplicationType.Cari)]
        [TestCase(ApplicationType.Configuration)]
        [TestCase(ApplicationType.Meta)]
        [TestCase(ApplicationType.Session)]
        public void Given_We_Pass_DataInterface_Arguments_With_FullNames_When_We_Call_ParseArguments_Then_The_Correct_Method_Is_Called_With_The_Correct_Arguments(ApplicationType applicationType)
        {
            //Arrange
            var args = new[] { "datainterface", "--type", $"{applicationType}", "--file", "file.ext" };

            //Act
            _sut.ParseArguments(args);

            //Assert
            _blaiseFileService.Verify(b => b.CreateDataInterfaceFile(applicationType, "file.ext"));
        }

        [Test]
        public void Given_We_Pass_An_Incorrect_ApplicationType_When_We_Call_ParseArguments_Then_Response_Of_1_is_returned()
        {
            //Arrange
            var args = new[] { "datainterface", "-t", "hello", "-f", "file.ext" };

            //Act
            var result = _sut.ParseArguments(args);

            //Assert
            Assert.AreEqual(1, result);
        }

        [Test]
        public void Given_We_Dont_Pass_In_TypeArgument_When_We_Call_ParseArguments_Then_Response_Of_1_is_returned()
        {
            //Arrange
            var args = new[] { "datainterface", "-f", "file.ext" };

            //Act
            var result = _sut.ParseArguments(args);

            //Assert
            Assert.AreEqual(1, result);
        }

        [Test]
        public void Given_We_Dont_Pass_In_FileArgument_When_We_Call_ParseArguments_Then_Response_Of_1_is_returned()
        {
            //Arrange
            var args = new[] { "datainterface", "-t", "hello" };

            //Act
            var result = _sut.ParseArguments(args);

            //Assert
            Assert.AreEqual(1, result);
        }
    }
}
