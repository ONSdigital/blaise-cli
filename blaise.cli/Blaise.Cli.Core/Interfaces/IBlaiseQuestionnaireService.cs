using Blaise.Nuget.Api.Contracts.Enums;

namespace Blaise.Cli.Core.Interfaces
{
    public interface IBlaiseQuestionnaireService
    {
        void InstallQuestionnaire(string questionnaireName, string serverParkName, string questionnaireFile);
    }
}
