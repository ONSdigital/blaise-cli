namespace Blaise.Cli.Core.Services
{
    using Blaise.Cli.Core.Extensions;
    using Blaise.Cli.Core.Interfaces;
    using Blaise.Nuget.Api.Contracts.Enums;
    using Blaise.Nuget.Api.Contracts.Extensions;
    using Blaise.Nuget.Api.Contracts.Interfaces;
    using Blaise.Nuget.Api.Contracts.Models;
    using StatNeth.Blaise.API.ServerManager;

    public class BlaiseQuestionnaireService : IBlaiseQuestionnaireService
    {
        private readonly IBlaiseQuestionnaireApi _blaiseQuestionnaireApi;
        private readonly IBlaiseFileApi _blaiseFileApi;

        public BlaiseQuestionnaireService(
            IBlaiseQuestionnaireApi blaiseQuestionnaireApi,
            IBlaiseFileApi blaiseFileApi)
        {
            _blaiseQuestionnaireApi = blaiseQuestionnaireApi;
            _blaiseFileApi = blaiseFileApi;
        }

        public void InstallQuestionnaire(string questionnaireName, string serverParkName, string questionnaireFile, bool overwriteExistingData = true)
        {
            questionnaireName.ThrowExceptionIfNullOrEmpty("questionnaireName");
            serverParkName.ThrowExceptionIfNullOrEmpty("serverParkName");
            questionnaireFile.ThrowExceptionIfNullOrEmpty("questionnaireFile");

            _blaiseFileApi.UpdateQuestionnaireFileWithSqlConnection(questionnaireName, questionnaireFile, overwriteExistingData);

            var installOptions = new InstallOptions
            {
                DataEntrySettingsName = QuestionnaireDataEntryType.StrictInterviewing.ToString(),
                InitialAppLayoutSetGroupName = QuestionnaireInterviewType.Cati.FullName(),
                LayoutSetGroupName = QuestionnaireInterviewType.Cati.FullName(),
                OverwriteMode = DataOverwriteMode.Always,
            };

            _blaiseQuestionnaireApi.InstallQuestionnaire(questionnaireName, serverParkName, questionnaireFile, installOptions);
        }
    }
}
