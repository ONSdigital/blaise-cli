namespace Blaise.Cli.Core.Services
{
    using Blaise.Cli.Core.Extensions;
    using Blaise.Cli.Core.Interfaces;
    using Blaise.Nuget.Api.Contracts.Interfaces;
    using StatNeth.Blaise.API.DataInterface;

    public class BlaiseFileService : IBlaiseFileService
    {
        private readonly IBlaiseFileApi _blaiseFileApi;

        public BlaiseFileService(IBlaiseFileApi blaiseFileApi)
        {
            _blaiseFileApi = blaiseFileApi;
        }

        public void CreateDataInterfaceFile(ApplicationType applicationType, string fileName)
        {
            fileName.ThrowExceptionIfNullOrEmpty("fileName");

            _blaiseFileApi.CreateSettingsDataInterfaceFile(applicationType, fileName);
        }

        public void UpdateQuestionnairePackageWithData(string serverParkName, string questionnaireName, string fileName, bool auditOption = false, int batchSize = 0)
        {
            serverParkName.ThrowExceptionIfNullOrEmpty("serverParkName");
            questionnaireName.ThrowExceptionIfNullOrEmpty("questionnaireName");
            fileName.ThrowExceptionIfNullOrEmpty("fileName");

            if (batchSize > 0)
            {
                _blaiseFileApi.UpdateQuestionnaireFileWithBatchedData(serverParkName, questionnaireName, fileName, batchSize, auditOption);
                return;
            }

            _blaiseFileApi.UpdateQuestionnaireFileWithData(serverParkName, questionnaireName, fileName, auditOption);
        }

        public void UpdateQuestionnaireFileWithSqlConnection(string fileName)
        {
            fileName.ThrowExceptionIfNullOrEmpty("fileName");
            var questionnaireName = fileName.GetQuestionnaireNameFromFile();

            _blaiseFileApi.UpdateQuestionnaireFileWithSqlConnection(
                questionnaireName,
                fileName);
        }
    }
}
