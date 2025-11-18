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
            // arrange
            var args = new[] { "datadelivery", "-s", _serverParkName, "-q", _questionnaireName, "-f", _fileName };

            // act
            _sut.ParseArguments(args);

            // assert
            _blaiseFileService.Verify(b => b.UpdateQuestionnairePackageWithData(_serverParkName, _questionnaireName, _fileName, false, 0));
        }

        [TestCase("false")]
        [TestCase("False")]
        [TestCase("true")]
        [TestCase("True")]
        public void Given_We_Pass_DataDelivery_Arguments_With_Audit_Options_When_We_Call_ParseArguments_Then_The_Correct_Method_Is_Called_With_The_Correct_Arguments(string auditOptions)
        {
            // arrange
            var args = new[] { "datadelivery", "-s", _serverParkName, "-q", _questionnaireName, "-f", _fileName, "-a", auditOptions };

            // act
            _sut.ParseArguments(args);

            // assert
            _blaiseFileService.Verify(b => b.UpdateQuestionnairePackageWithData(_serverParkName, _questionnaireName, _fileName, Convert.ToBoolean(auditOptions), 0));
        }

        [TestCase("0")]
        [TestCase("1")]
        [TestCase("10")]
        [TestCase("20")]
        public void Given_We_Pass_DataDelivery_Arguments_With_Batch_Size_Options_When_We_Call_ParseArguments_Then_The_Correct_Method_Is_Called_With_The_Correct_Arguments(string batchSize)
        {
            // arrange
            var args = new[] { "datadelivery", "-s", _serverParkName, "-q", _questionnaireName, "-f", _fileName, "-b", batchSize };

            // act
            _sut.ParseArguments(args);

            // assert
            _blaiseFileService.Verify(b => b.UpdateQuestionnairePackageWithData(_serverParkName, _questionnaireName, _fileName, false, Convert.ToInt32(batchSize)));
        }

        [TestCase("false", "10")]
        [TestCase("true", "10")]
        public void Given_We_Pass_DataDelivery_Arguments_With_FullNames_When_We_Call_ParseArguments_Then_The_Correct_Method_Is_Called_With_The_Correct_Arguments(string auditOptions, string batchSize)
        {
            // arrange
            var args = new[] { "datadelivery", "--serverParkName", _serverParkName, "--questionnaireName", _questionnaireName, "--file", _fileName, "--audit", auditOptions, "--batchSize", batchSize };

            // act
            _sut.ParseArguments(args);

            // assert
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
            // arrange
            var args = new[] { "datadelivery", "--questionnaireName", _questionnaireName, "--file", _fileName };

            // act
            var result = _sut.ParseArguments(args);

            // assert
            Assert.That(result, Is.EqualTo(1));
        }

        [Test]
        public void Given_We_Do_Not_Pass_An_QuestionnaireName_When_We_Call_ParseArguments_Then_Response_Of_1_is_returned()
        {
            // arrange
            var args = new[] { "datadelivery", "--serverParkName", _serverParkName, "--file", _fileName };

            // act
            var result = _sut.ParseArguments(args);

            // assert
            Assert.That(result, Is.EqualTo(1));
        }

        [Test]
        public void Given_We_Do_Not_Pass_A_File_When_We_Call_ParseArguments_Then_Response_Of_1_is_returned()
        {
            // arrange
            var args = new[] { "datadelivery", "--serverParkName", _serverParkName, "--questionnaireName", _questionnaireName };

            // act
            var result = _sut.ParseArguments(args);

            // assert
            Assert.That(result, Is.EqualTo(1));
        }

        [Test]
        public void Given_We_Do_Not_Pass_Audit_Or_Batch_Size_Options_When_We_Call_ParseArguments_Then_Response_Of_0_is_returned()
        {
            // arrange
            var args = new[] { "datadelivery", "-s", _serverParkName, "-q", _questionnaireName, "-f", _fileName };

            // act
            var result = _sut.ParseArguments(args);

            // assert
            Assert.That(result, Is.EqualTo(0));
        }

        [TestCase(ApplicationType.Cati)]
        [TestCase(ApplicationType.AuditTrail)]
        [TestCase(ApplicationType.Cari)]
        [TestCase(ApplicationType.Configuration)]
        [TestCase(ApplicationType.Meta)]
        [TestCase(ApplicationType.Session)]
        public void Given_We_Pass_DataInterface_Arguments_When_We_Call_ParseArguments_Then_The_Correct_Method_Is_Called_With_The_Correct_Arguments(ApplicationType applicationType)
        {
            // arrange
            var args = new[] { "datainterface", "-t", $"{applicationType}", "-f", "file.ext" };

            // act
            _sut.ParseArguments(args);

            // assert
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
            // arrange
            var args = new[] { "datainterface", "--type", $"{applicationType}", "--file", "file.ext" };

            // act
            _sut.ParseArguments(args);

            // assert
            _blaiseFileService.Verify(b => b.CreateDataInterfaceFile(applicationType, "file.ext"));
        }

        [Test]
        public void Given_We_Pass_An_Incorrect_ApplicationType_When_We_Call_ParseArguments_Then_Response_Of_1_is_returned()
        {
            // arrange
            var args = new[] { "datainterface", "-t", "hello", "-f", "file.ext" };

            // act
            var result = _sut.ParseArguments(args);

            // assert
            Assert.That(result, Is.EqualTo(1));
        }

        [Test]
        public void Given_We_Dont_Pass_In_TypeArgument_When_We_Call_ParseArguments_Then_Response_Of_1_is_returned()
        {
            // arrange
            var args = new[] { "datainterface", "-f", "file.ext" };

            // act
            var result = _sut.ParseArguments(args);

            // assert
            Assert.That(result, Is.EqualTo(1));
        }

        [Test]
        public void Given_We_Dont_Pass_In_FileArgument_When_We_Call_ParseArguments_Then_Response_Of_1_is_returned()
        {
            // arrange
            var args = new[] { "datainterface", "-t", "hello" };

            // act
            var result = _sut.ParseArguments(args);

            // assert
            Assert.That(result, Is.EqualTo(1));
        }

        [TestCase("true", true)]
        [TestCase("True", true)]
        [TestCase("TRUE", true)]
        [TestCase("false", false)]
        [TestCase("False", false)]
        [TestCase("FALSE", false)]
        public void Given_We_Pass_QuestionnaireInstall_Arguments_When_We_Call_ParseArgument_Then_The_Correct_Method_Is_Called_With_The_Correct_Arguments(string arg, bool value)
        {
            // arrange
            var args = new[] { "questionnaireinstall", "-q", _questionnaireName, "-s", _serverParkName, "-f", _fileName, "-o", arg };

            // act
            var result = _sut.ParseArguments(args);

            // assert
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
            // arrange
            var args = new[] { "questionnaireinstall", "--serverParkName", _serverParkName, "--questionnaireName", _questionnaireName, "--questionnaireFile", _fileName, "--overwriteExistingData", arg };

            // act
            var result = _sut.ParseArguments(args);

            // assert
            _blaiseQuestionnaireService.Verify(b => b.InstallQuestionnaire(_questionnaireName, _serverParkName, _fileName, value));
        }

        [Test]
        public void Given_We_Do_Not_Pass_A_ServerParkName_When_We_Call_ParseArguments_Then_Response_Of_1_is_returned_QuestionnaireInstall()
        {
            // arrange
            var args = new[] { "questionnaireinstall", "--questionnaireName", _questionnaireName, "--questionnaireFile", _fileName };

            // act
            var result = _sut.ParseArguments(args);

            // assert
            Assert.That(result, Is.EqualTo(1));
        }

        [Test]
        public void Given_We_Do_Not_Pass_An_QuestionnaireName_When_We_Call_ParseArguments_Then_Response_Of_1_is_returned_QuestionnaireInstall()
        {
            // arrange
            var args = new[] { "questionnaireinstall", "--serverParkName", _serverParkName, "--questionnaireFile", _fileName };

            // act
            var result = _sut.ParseArguments(args);

            // assert
            Assert.That(result, Is.EqualTo(1));
        }

        [Test]
        public void Given_We_Do_Not_Pass_A_File_When_We_Call_ParseArguments_Then_Response_Of_1_is_returned_()
        {
            // arrange
            var args = new[] { "questionnaireinstall", "--serverParkName", _serverParkName, "--questionnaireName", _questionnaireName };

            // act
            var result = _sut.ParseArguments(args);

            // assert
            Assert.That(result, Is.EqualTo(1));
        }
    }
}
