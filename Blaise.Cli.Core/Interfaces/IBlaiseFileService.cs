namespace Blaise.Cli.Core.Interfaces
{
    using StatNeth.Blaise.API.DataInterface;

    public interface IBlaiseFileService
    {
        void CreateDataInterfaceFile(ApplicationType applicationType, string fileName);

        void UpdateQuestionnairePackageWithData(
            string serverParkName,
            string questionnaireName,
            string questionnaireFile,
            bool auditOption = false,
            int batchSize = 0);

        void UpdateQuestionnaireFileWithSqlConnection(string questionnaireFile);
    }
}
