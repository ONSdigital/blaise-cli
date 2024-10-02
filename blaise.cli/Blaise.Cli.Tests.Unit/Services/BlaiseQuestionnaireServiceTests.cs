using Blaise.Cli.Core.Interfaces;
using Blaise.Cli.Core.Services;
using Blaise.Nuget.Api.Contracts.Enums;
using Blaise.Nuget.Api.Contracts.Extensions;
using Blaise.Nuget.Api.Contracts.Interfaces;
using Blaise.Nuget.Api.Contracts.Models;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using StatNeth.Blaise.API.ServerManager;
using System;

namespace Blaise.Cli.Tests.Unit.Services
{
    public class BlaiseQuestionnaireServiceTests
    {
        private Mock<IBlaiseQuestionnaireApi> _blaiseQuestionnaireApi;
        private Mock<IBlaiseFileApi> _blaiseFileApi;
        private IBlaiseQuestionnaireService _sut;
     
        private readonly string _serverParkName = "gusty";
        private readonly string _questionnaireName = "OPN2101A";
        private readonly string _fileName = "OPN2101A.bpkg";
        private readonly InstallOptions _questionnaireInstallOptions = new InstallOptions
        {
            DataEntrySettingsName = QuestionnaireDataEntryType.StrictInterviewing.ToString(),
            InitialAppLayoutSetGroupName = QuestionnaireInterviewType.Cati.FullName(),
            LayoutSetGroupName = QuestionnaireInterviewType.Cati.FullName(),
            OverwriteMode = DataOverwriteMode.Always,
            HarmlessDataModificationMode = HarmlessDataModificationMode.Always,
            GeneratePages = false,
            RemoveSessions = true,
            InitialAppCariSetting = "",
            Orientation = Orientation.Landscape,
            InitialAppDataEntrySettingsName = "",
            InitialModeName = "",
            EnableClose = true,
            EncryptDataFiles = true,
            DownloadSessionData = false,
            UploadSessionData = false,
            AllowDownloadOverMeteredConnection = false,
        };

        [SetUp]
        public void SetupTests()
        {
            _blaiseQuestionnaireApi = new Mock<IBlaiseQuestionnaireApi>();
            _blaiseFileApi = new Mock<IBlaiseFileApi>();
            _sut = new BlaiseQuestionnaireService(_blaiseQuestionnaireApi.Object, _blaiseFileApi.Object);
        }

        /*[Test]
        public void Given_We_Have_Passed_Valid_Parameters_When_We_Call_InstallQuestionnaire_Then_The_Correct_API_Call_Is_Made()
        {
            //act
            _sut.InstallQuestionnaire(_questionnaireName, _serverParkName, _fileName,_questionnaireInstallOptions);

            //assert
            _blaiseFileApi.Verify(b => b.UpdateQuestionnaireFileWithSqlConnection(_questionnaireName, _fileName), Times.Once);
            _blaiseQuestionnaireApi.Verify(b => b.InstallQuestionnaire(_questionnaireName, _serverParkName, 
                _fileName, _questionnaireInstallOptions), Times.Once);
        }*/

        [Test]
        public void Given_We_Have_Called_InstallQuestionnaire_When_We_have_supplied_An_Empty_QuestionnaireName_Then_An_Error_Is_Thrown()
        {
            //act && assert
            var exception = Assert.Throws<ArgumentException>(() => _sut.InstallQuestionnaire(string.Empty, _serverParkName,  _fileName, _questionnaireInstallOptions));

            Assert.IsNotNull(exception);
            Assert.AreEqual("A value for the argument 'questionnaireName' must be supplied", exception.Message);
        }

        [Test]
        public void Given_We_Have_Called_InstallQuestionnaire_When_We_have_supplied_A_Null_Value_For_QuestionnaireName_Then_An_Error_Is_Thrown()
        {
            //act && assert
            var exception = Assert.Throws<ArgumentNullException>(() => _sut.InstallQuestionnaire(null, _serverParkName,  _fileName, _questionnaireInstallOptions));

            Assert.IsNotNull(exception);
            Assert.AreEqual("Value cannot be null.\r\nParameter name: questionnaireName", exception.Message);
        }

        [Test]
        public void Given_We_Have_Called_InstallQuestionnaire_When_We_have_supplied_An_Empty_ServerParkName_Then_An_Error_Is_Thrown()
        {
            //act && assert
            var exception = Assert.Throws<ArgumentException>(() => _sut.InstallQuestionnaire(_questionnaireName,string.Empty, _fileName, _questionnaireInstallOptions));

            Assert.IsNotNull(exception);
            Assert.AreEqual("A value for the argument 'serverParkName' must be supplied", exception.Message);
        }

        [Test]
        public void Given_We_Have_Called_InstallQuestionnaire_When_We_have_supplied_A_Null_Value_For_ServerParkName_Then_An_Error_Is_Thrown()
        {
            //act && assert
            var exception = Assert.Throws<ArgumentNullException>(() => _sut.InstallQuestionnaire(_questionnaireName, null, _fileName, _questionnaireInstallOptions));

            Assert.IsNotNull(exception);
            Assert.AreEqual("Value cannot be null.\r\nParameter name: serverParkName", exception.Message);
        }

        [Test]
        public void Given_We_Have_Called_InstallQuestionnaire_When_We_have_supplied_An_Empty_FileName_Then_An_Error_Is_Thrown()
        {
            //act && assert
            var exception = Assert.Throws<ArgumentException>(() => _sut.InstallQuestionnaire(_questionnaireName, _serverParkName, string.Empty, _questionnaireInstallOptions));

            Assert.IsNotNull(exception);
            Assert.AreEqual("A value for the argument 'questionnaireFile' must be supplied", exception.Message);
        }

        [Test]
        public void Given_We_Have_Called_InstallQuestionnaire_When_We_have_supplied_A_Null_Value_For_FileName_Then_An_Error_Is_Thrown()
        {
            //act && assert
            var exception = Assert.Throws<ArgumentNullException>(() => _sut.InstallQuestionnaire(_questionnaireName, _serverParkName,  null, _questionnaireInstallOptions));

            Assert.IsNotNull(exception);
            Assert.AreEqual("Value cannot be null.\r\nParameter name: questionnaireFile", exception.Message);
        }
    }
}
