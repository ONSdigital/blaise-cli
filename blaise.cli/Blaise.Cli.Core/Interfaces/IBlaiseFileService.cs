using StatNeth.Blaise.API.DataInterface;

namespace Blaise.Cli.Core.Interfaces
{
    public interface IBlaiseFileService
    {
        void CreateDataInterfaceFile(ApplicationType applicationType, string fileName);
    }
}
