using CommandLine;

namespace Blaise.Cli.Core.Models
{
    [Verb("datadelivery", HelpText = "Data delivery options")]
    public class DataDeliveryOptions
    {
        [Option('s', "serverParkName", Required = true, HelpText = "Name of the server park that houses the questionnaire to be delivered")]
        public string ServerParkName { get; set; }

        [Option('q', "questionnaireName", Required = true, HelpText = "Name of the questionnaire to be delivered")]
        public string QuestionnaireName { get; set; }

        [Option('f', "file", Required = true, HelpText = "File name of the questionnaire package to be delivered")]
        public string File { get; set; }

        [Option('a', "audit", HelpText = "Option to include audit trail data as part of the delivery")]
        public string Audit { get; set; } = "false";

        [Option('b', "batchSize", HelpText = "The number of cases to use for batching")]
        public string BatchSize { get; set; } = "0";
    }
}
