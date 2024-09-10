using Blaise.Nuget.Api.Contracts.Enums;
using StatNeth.Blaise.API.ServerManager;

namespace Blaise.Cli.Core.Interfaces
{
    public interface IBlaiseQuestionnaireService
    {
        void InstallQuestionnaire(string questionnaireName, string serverParkName, string questionnaireFile, string installOptions);
    }
}
