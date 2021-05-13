using CommandLine;
using StatNeth.Blaise.API.DataInterface;

namespace Blaise.Cli.Core.OptionModels
{
    [Verb("datainterface", HelpText = "Spells out your name")]
    public class DataInterfaceOptions
    {
        [Option('t', "type", Required = true, HelpText = "Type of data interface you want to create")]
        public ApplicationType ApplicationType { get; set; }

        [Option('f', "file", Required = true, HelpText = "The file you wish to create")]
        public string File { get; set; }
    }
}
