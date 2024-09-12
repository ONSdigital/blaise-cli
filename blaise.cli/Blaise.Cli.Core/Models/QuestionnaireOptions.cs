using Blaise.Nuget.Api.Contracts.Models;
using CommandLine;
using Newtonsoft.Json;
using StatNeth.Blaise.API.ServerManager;

namespace Blaise.Cli.Core.Models
{
    [Verb("questionnaireinstall", HelpText = "Questionnaire install options")]
    public class QuestionnaireOptions
    {
        [Option('q', "questionnaireName", Required = true, HelpText = "Name of the questionnaire to be installed")]
        public string QuestionnaireName { get; set; }

        [Option('s', "serverParkName", Required = true, HelpText = "Name of the server park to install the questionnaire")]
        public string ServerParkName { get; set; }

        [Option('f', "questionnaireFile", Required = true, HelpText = "File name of the questionnaire package to be installed")]
        public string QuestionnaireFile { get; set; }

        [Option('i', "installOptions", Required = true, HelpText = "Install options for Blaise Server Manager")]
        public string InstallOptions { get; set; }

        // This property will hold the deserialized object
        public InstallOptions ParsedObject { get; set; }

        // Method to deserialize the object from JSON string after parsing command-line args
        public void DeserializeObject()
        {
            ParsedObject = JsonConvert.DeserializeObject<InstallOptions>(InstallOptions);
        }
    }
}
