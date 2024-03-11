using CommandLine;

namespace Blaise.Cli.Core.Models
{
    [Verb("questionnaireinstall", HelpText = "Data delivery pipeline options")]
    public class QuestionnaireOptions
    {
        [Option('q', "questionnaireName", Required = true, HelpText = "The name of the questionnaire")]
        public string QuestionnaireName { get; set; }

        [Option('s', "serverParkName", Required = true, HelpText = "The name of the server park that houses the questionnaire data")]
        public string ServerParkName { get; set; }

        [Option('f', "questionnaireFile", Required = true, HelpText = "The package file containing the questionnaire file")]
        public string File { get; set; }
    }
}