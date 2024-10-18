using CommandLine;

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

        [Option('o', "overwriteExistingData", Required = false, Default = "true", HelpText = "Overwrite any existing questionnaire data for the package to be installed")]
        public string OverwriteExistingData { get; set; }
    }
}
