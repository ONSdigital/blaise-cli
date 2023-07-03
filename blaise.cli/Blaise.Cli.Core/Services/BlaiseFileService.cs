using Blaise.Cli.Core.Extensions;
using Blaise.Cli.Core.Interfaces;
using Blaise.Nuget.Api.Contracts.Interfaces;
using StatNeth.Blaise.API.DataInterface;

namespace Blaise.Cli.Core.Services
{
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

        public void UpdateQuestionnairePackageWithData(string serverParkName, string questionnaireName, string fileName, bool auditOption = false)
        {
            serverParkName.ThrowExceptionIfNullOrEmpty("serverParkName");
            questionnaireName.ThrowExceptionIfNullOrEmpty("questionnaireName");
            fileName.ThrowExceptionIfNullOrEmpty("fileName");

            _blaiseFileApi.UpdateQuestionnaireFileWithData(serverParkName, questionnaireName, fileName, auditOption);
        }
    }
}
