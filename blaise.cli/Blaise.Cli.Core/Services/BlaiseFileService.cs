using StatNeth.Blaise.API.DataInterface;
using Blaise.Cli.Core.Extensions;
using Blaise.Cli.Core.Interfaces;
using Blaise.Nuget.Api.Contracts.Interfaces;

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

        public void UpdateInstrumentPackageWithData(string serverParkName, string instrumentName, string fileName)
        {
            serverParkName.ThrowExceptionIfNullOrEmpty("serverParkName");
            instrumentName.ThrowExceptionIfNullOrEmpty("instrumentName");
            fileName.ThrowExceptionIfNullOrEmpty("fileName");

            _blaiseFileApi.UpdateInstrumentFileWithData(serverParkName, instrumentName, fileName);
        }
    }
}
