namespace Blaise.Cli.Tests.Unit.Services
{
    using System;
    using Blaise.Cli.Core.Interfaces;
    using Blaise.Cli.Core.Services;
    using Moq;
    using NUnit.Framework;
    using StatNeth.Blaise.API.DataInterface;

    public class CommandServiceTests
    {
        private readonly string _serverParkName = "gusty";
        private readonly string _questionnaireName = "OPN2101A";
        private readonly string _fileName = "OPN2101A.bpkg";

        private Mock<IBlaiseFileService> _blaiseFileService;
        private Mock<IBlaiseQuestionnaireService> _blaiseQuestionnaireService;
        private ICommandService _sut;

        [SetUp]
        public void SetupTests()
        {
            _blaiseFileService = new Mock<IBlaiseFileService>();
            _blaiseQuestionnaireService = new Mock<IBlaiseQuestionnaireService>();
            _sut = new CommandService(_blaiseFileService.Object, _blaiseQuestionnaireService.Object);
        }

        [Test]
        public void Given_We_Pass_DataDelivery_Arguments_With_No_Options_When_We_Call_ParseArguments_Then_The_Correct_Method_Is_Called_With_The_Correct_Arguments()
        {
            // Arrange
            var args = new[] { "datadelivery", "-s", _serverParkName, "-q", _questionnaireName, "-f", _fileName };

            // Act
            _sut.ParseArguments(args);

            // Assert
            _blaiseFileService.Verify(b => b.UpdateQuestionnairePackageWithData(_serverParkName, _questionnaireName, _fileName, false, 0));
        }

        [TestCase("false")]
        [TestCase("False")]
        [TestCase("true")]
        [TestCase("True")]
        public void Given_We_Pass_DataDelivery_Arguments_With_Audit_Options_When_We_Call_ParseArguments_Then_The_Correct_Method_Is_Called_With_The_Correct_Arguments(string auditOptions)
        {
            // Arrange
            var args = new[] { "datadelivery", "-s", _serverParkName, "-q", _questionnaireName, "-f", _fileName, "-a", auditOptions };

            // Act
            _sut.ParseArguments(args);

            // Assert
            _blaiseFileService.Verify(b => b.UpdateQuestionnairePackageWithData(_serverParkName, _questionnaireName, _fileName, Convert.ToBoolean(auditOptions), 0));
        }

        [TestCase("0")]
        [TestCase("1")]
        [TestCase("10")]
        [TestCase("20")]
        public void Given_We_Pass_DataDelivery_Arguments_With_Batch_Size_Options_When_We_Call_ParseArguments_Then_The_Correct_Method_Is_Called_With_The_Correct_Arguments(string batchSize)
        {
            // Arrange
            var args = new[] { "datadelivery", "-s", _serverParkName, "-q", _questionnaireName, "-f", _fileName, "-b", batchSize };

            // Act
            _sut.ParseArguments(args);

            // Assert
            _blaiseFileService.Verify(b => b.UpdateQuestionnairePackageWithData(_serverParkName, _questionnaireName, _fileName, false, Convert.ToInt32(batchSize)));
        }

        [TestCase("false", "10")]
        [TestCase("true", "10")]
        public void Given_We_Pass_DataDelivery_Arguments_With_FullNames_When_We_Call_ParseArguments_Then_The_Correct_Method_Is_Called_With_The_Correct_Arguments(string auditOptions, string batchSize)
        {
            // Arrange
            var args = new[] { "datadelivery", "--serverParkName", _serverParkName, "--questionnaireName", _questionnaireName, "--file", _fileName, "--audit", auditOptions, "--batchSize", batchSize };

            // Act
            _sut.ParseArguments(args);

            // Assert
            _blaiseFileService.Verify(b => b.UpdateQuestionnairePackageWithData(
                _serverParkName,
                _questionnaireName,
                _fileName,
                Convert.ToBoolean(auditOptions),
                Convert.ToInt32(batchSize)));
        }

        [Test]
        public void Given_We_Do_Not_Pass_A_ServerParkName_When_We_Call_ParseArguments_Then_Response_Of_1_is_returned()
        {
            // Arrange
            var args = new[] { "datadelivery", "--questionnaireName", _questionnaireName, "--file", _fileName };

            // Act
            var result = _sut.ParseArguments(args);

            // Assert
            Assert.AreEqual(1, result);
        }

        [Test]
        public void Given_We_Do_Not_Pass_An_QuestionnaireName_When_We_Call_ParseArguments_Then_Response_Of_1_is_returned()
        {
            // Arrange
            var args = new[] { "datadelivery", "--serverParkName", _serverParkName, "--file", _fileName };

            // Act
            var result = _sut.ParseArguments(args);

            // Assert
            Assert.AreEqual(1, result);
        }

        [Test]
        public void Given_We_Do_Not_Pass_A_File_When_We_Call_ParseArguments_Then_Response_Of_1_is_returned()
        {
            // Arrange
            var args = new[] { "datadelivery", "--serverParkName", _serverParkName, "--questionnaireName", _questionnaireName };

            // Act
            var result = _sut.ParseArguments(args);

            // Assert
            Assert.AreEqual(1, result);
        }

        [Test]
        public void Given_We_Do_Not_Pass_Audit_Or_Batch_Size_Options_When_We_Call_ParseArguments_Then_Response_Of_0_is_returned()
        {
            // Arrange
            var args = new[] { "datadelivery", "-s", _serverParkName, "-q", _questionnaireName, "-f", _fileName };

            // Act
            var result = _sut.ParseArguments(args);

            // Assert
            Assert.AreEqual(0, result);
        }

        [TestCase(ApplicationType.Cati)]
        [TestCase(ApplicationType.AuditTrail)]
        [TestCase(ApplicationType.Cari)]
        [TestCase(ApplicationType.Configuration)]
        [TestCase(ApplicationType.Meta)]
        [TestCase(ApplicationType.Session)]
        public void Given_We_Pass_DataInterface_Arguments_When_We_Call_ParseArguments_Then_The_Correct_Method_Is_Called_With_The_Correct_Arguments(ApplicationType applicationType)
        {
            // Arrange
            var args = new[] { "datainterface", "-t", $"{applicationType}", "-f", "file.ext" };

            // Act
            _sut.ParseArguments(args);

            // Assert
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
            // Arrange
            var args = new[] { "datainterface", "--type", $"{applicationType}", "--file", "file.ext" };

            // Act
            _sut.ParseArguments(args);

            // Assert
            _blaiseFileService.Verify(b => b.CreateDataInterfaceFile(applicationType, "file.ext"));
        }

        [Test]
        public void Given_We_Pass_An_Incorrect_ApplicationType_When_We_Call_ParseArguments_Then_Response_Of_1_is_returned()
        {
            // Arrange
            var args = new[] { "datainterface", "-t", "hello", "-f", "file.ext" };

            // Act
            var result = _sut.ParseArguments(args);

            // Assert
            Assert.AreEqual(1, result);
        }

        [Test]
        public void Given_We_Dont_Pass_In_TypeArgument_When_We_Call_ParseArguments_Then_Response_Of_1_is_returned()
        {
            // Arrange
            var args = new[] { "datainterface", "-f", "file.ext" };

            // Act
            var result = _sut.ParseArguments(args);

            // Assert
            Assert.AreEqual(1, result);
        }

        [Test]
        public void Given_We_Dont_Pass_In_FileArgument_When_We_Call_ParseArguments_Then_Response_Of_1_is_returned()
        {
            // Arrange
            var args = new[] { "datainterface", "-t", "hello" };

            // Act
            var result = _sut.ParseArguments(args);

            // Assert
            Assert.AreEqual(1, result);
        }

        [TestCase("true", true)]
        [TestCase("True", true)]
        [TestCase("TRUE", true)]
        [TestCase("false", false)]
        [TestCase("False", false)]
        [TestCase("FALSE", false)]
        public void Given_We_Pass_QuestionnaireInstall_Arguments_When_We_Call_ParseArgument_Then_The_Correct_Method_Is_Called_With_The_Correct_Arguments(string arg, bool value)
        {
            // Arrange
            var args = new[] { "questionnaireinstall", "-q", _questionnaireName, "-s", _serverParkName, "-f", _fileName, "-o", arg };

            // Act
            var result = _sut.ParseArguments(args);

            // Assert
            _blaiseQuestionnaireService.Verify(b => b.InstallQuestionnaire(_questionnaireName, _serverParkName, _fileName, value));
        }

        [TestCase("true", true)]
        [TestCase("True", true)]
        [TestCase("TRUE", true)]
        [TestCase("false", false)]
        [TestCase("False", false)]
        [TestCase("FALSE", false)]
        public void Given_We_Pass_QuestionnaireInstall_Arguments_With_FullNames_When_We_Call_ParseArguments_Then_The_Correct_Method_Is_Called_With_The_Correct_Arguments(string arg, bool value)
        {
            // Arrange
            var args = new[] { "questionnaireinstall", "--serverParkName", _serverParkName, "--questionnaireName", _questionnaireName, "--questionnaireFile", _fileName, "--overwriteExistingData", arg };

            // Act
            var result = _sut.ParseArguments(args);

            // Assert
            _blaiseQuestionnaireService.Verify(b => b.InstallQuestionnaire(_questionnaireName, _serverParkName, _fileName, value));
        }

        [Test]
        public void Given_We_Do_Not_Pass_A_ServerParkName_When_We_Call_ParseArguments_Then_Response_Of_1_is_returned_QuestionnaireInstall()
        {
            // Arrange
            var args = new[] { "questionnaireinstall", "--questionnaireName", _questionnaireName, "--questionnaireFile", _fileName };

            // Act
            var result = _sut.ParseArguments(args);

            // Assert
            Assert.AreEqual(1, result);
        }

        [Test]
        public void Given_We_Do_Not_Pass_An_QuestionnaireName_When_We_Call_ParseArguments_Then_Response_Of_1_is_returned_QuestionnaireInstall()
        {
            // Arrange
            var args = new[] { "questionnaireinstall", "--serverParkName", _serverParkName, "--questionnaireFile", _fileName };

            // Act
            var result = _sut.ParseArguments(args);

            // Assert
            Assert.AreEqual(1, result);
        }

        [Test]
        public void Given_We_Do_Not_Pass_A_File_When_We_Call_ParseArguments_Then_Response_Of_1_is_returned_()
        {
            // Arrange
            var args = new[] { "questionnaireinstall", "--serverParkName", _serverParkName, "--questionnaireName", _questionnaireName };

            // Act
            var result = _sut.ParseArguments(args);

            // Assert
            Assert.AreEqual(1, result);
        }
    }
}
