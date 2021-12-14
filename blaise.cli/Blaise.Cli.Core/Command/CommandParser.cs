using System;
using Blaise.Cli.Core.Command.Models;
using Blaise.Cli.Core.Interfaces;
using CommandLine;

namespace Blaise.Cli.Core.Command
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

            return parser.ParseArguments<DataInterfaceOptions, DataDeliveryOptions>(args)
              .MapResult(
                  (DataInterfaceOptions opts) => CreateDataInterface(opts),
                  (DataDeliveryOptions opts) => UpdateInstrumentPackageWithData(opts),
                  errors => 1);
        }

        private int CreateDataInterface(DataInterfaceOptions options)
        {
            _blaiseFileService.CreateDataInterfaceFile(options.ApplicationType, options.File);

            return 0;
        }

        private int UpdateInstrumentPackageWithData(DataDeliveryOptions options)
        {
            _blaiseFileService.UpdateInstrumentPackageWithData(options.ServerParkName, options.InstrumentName,
                options.File);

            return 0;
        }
    }
}
