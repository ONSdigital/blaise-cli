using System;
using Blaise.Cli.Core.Interfaces;
using Blaise.Cli.Core.OptionModels;
using Blaise.Cli.Interfaces;
using CommandLine;

namespace Blaise.Cli.Services
{
    public class CommandParser : ICommandParser
    {
        private readonly IBlaiseFileService _blaiseFileService;

        public CommandParser(IBlaiseFileService blaiseFileService)
        {
            _blaiseFileService = blaiseFileService;
        }

        public int ParseArguments(string[] args)
        {
            var parser = new Parser(with =>
            {
                with.CaseInsensitiveEnumValues = true;
                with.AutoHelp = true;
                with.AutoVersion = true;
                with.HelpWriter = Console.Out;
            });

            return parser.ParseArguments<DataInterfaceOptions>(args)
              .MapResult(
                (DataInterfaceOptions options) => { return CreateDataInterface(options); },
                errors => 1);
        }

        private int CreateDataInterface(DataInterfaceOptions options)
        {
            _blaiseFileService.CreateDataInterfaceFile(options.ApplicationType, options.File);
            return 0;
        }
    }
}
