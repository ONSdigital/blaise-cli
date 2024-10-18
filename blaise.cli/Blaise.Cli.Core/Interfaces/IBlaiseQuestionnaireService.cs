using Blaise.Nuget.Api.Contracts.Models;

namespace Blaise.Cli.Core.Interfaces
{
    public interface IBlaiseQuestionnaireService
    {
        void InstallQuestionnaire(string questionnaireName, string serverParkName, string questionnaireFile, bool overwriteExistingData = true);
    }
}
