using Blaise.Cli.Core.Interfaces;
using Blaise.Cli.Core.Extensions;
using Blaise.Nuget.Api.Contracts.Interfaces;
using StatNeth.Blaise.API.DataInterface;
using Blaise.Nuget.Api.Contracts.Enums;

namespace Blaise.Cli.Core.Services
{
    public class BlaiseQuestionnaireService : IBlaiseQuestionnaireService
    {
        private readonly IBlaiseQuestionnaireApi _blaiseQuestionnaireApi;

        public BlaiseQuestionnaireService(IBlaiseQuestionnaireApi blaiseQuestionnaireApi)
        {
            _blaiseQuestionnaireApi = blaiseQuestionnaireApi;
        } 

        public void InstallQuestionnaire(string questionnaireName, string serverParkName,  string questionnaireFile)
        {
            questionnaireName.ThrowExceptionIfNullOrEmpty("questionnaireName");
            serverParkName.ThrowExceptionIfNullOrEmpty("serverParkName");
            questionnaireFile.ThrowExceptionIfNullOrEmpty("questionnaireFile");

            _blaiseQuestionnaireApi.InstallQuestionnaire(serverParkName, questionnaireName, questionnaireFile, QuestionnaireInterviewType.Capi);
        }
    }
}
