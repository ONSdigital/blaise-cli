using CommandLine;

namespace Blaise.Cli.Core.Models
{
    [Verb("datadelivery", HelpText = "Data delivery pipeline options")]
    public class DataDeliveryOptions
    {
        [Option('s', "serverParkName", Required = true, HelpText = "The name of the server park that houses the questionnaire data")]
        public string ServerParkName { get; set; }

        [Option('q', "questionnaireName", Required = true, HelpText = "The name of the questionnaire")]
        public string QuestionnaireName { get; set; }

        [Option('f', "file", Required = true, HelpText = "The package file containing the questionnaire file")]
        public string File { get; set; }

        [Option('a', "audit", HelpText = "The audit trail data for the questionnaire")]
        public string Audit { get; set; } = "false";

        [Option('b', "batchSize", HelpText = "The number of cases to use for batching")]
        public string BatchSize { get; set; } = "0";
    }
}
