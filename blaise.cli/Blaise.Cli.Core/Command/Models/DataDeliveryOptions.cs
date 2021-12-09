using CommandLine;

namespace Blaise.Cli.Core.Command.Models
{
    [Verb("datadelivery", HelpText = "Data delivery pipeline options")]
    public class DataDeliveryOptions
    {
        [Option('s', "serverParkName", Required = true, HelpText = "The name of the server park that houses the instrument data")]
        public string ServerParkName { get; set; }

        [Option('i', "instrumentName", Required = true, HelpText = "The name of the instrument")]
        public string InstrumentName { get; set; }

        [Option('f', "file", Required = true, HelpText = "The package file containing the instrument file")]
        public string File { get; set; }
    }
}
