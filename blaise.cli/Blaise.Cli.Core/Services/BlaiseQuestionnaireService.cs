
using Blaise.Cli.Core.Interfaces;
using Blaise.Cli.Core.Extensions;
using Blaise.Nuget.Api.Contracts.Interfaces;
using Blaise.Nuget.Api.Contracts.Enums;

namespace Blaise.Cli.Core.Services
{
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

        public void InstallQuestionnaire(string questionnaireName, string serverParkName,  string questionnaireFile)
        {
            questionnaireName.ThrowExceptionIfNullOrEmpty("questionnaireName");
            serverParkName.ThrowExceptionIfNullOrEmpty("serverParkName");
            questionnaireFile.ThrowExceptionIfNullOrEmpty("questionnaireFile");

            _blaiseFileApi.UpdateQuestionnaireFileWithSqlConnection(questionnaireName, questionnaireFile);
            _blaiseQuestionnaireApi.InstallQuestionnaire(questionnaireName, serverParkName, questionnaireFile, QuestionnaireInterviewType.Capi);
        }
        
    }
}
