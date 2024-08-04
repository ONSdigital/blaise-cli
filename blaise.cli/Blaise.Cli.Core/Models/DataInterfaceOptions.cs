using CommandLine;
using StatNeth.Blaise.API.DataInterface;

namespace Blaise.Cli.Core.Models
{
    [Verb("datainterface", HelpText = "Data interface options")]
    public class DataInterfaceOptions
    {
        [Option('t', "type", Required = true, HelpText = "Type of data interface file to be created")]
        public ApplicationType ApplicationType { get; set; }

        [Option('f', "file", Required = true, HelpText = "File name of data interface file to be created")]
        public string File { get; set; }
    }
}
