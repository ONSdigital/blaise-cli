using Blaise.Cli.Core.Interfaces;
using Blaise.Cli.Core.Models;
using CommandLine;
using System;
// ReSharper disable All

namespace Blaise.Cli.Core.Services
{
    public class CommandService : ICommandService
    {
        private readonly IBlaiseFileService _blaiseFileService;

        public CommandService(IBlaiseFileService blaiseFileService)
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
                  (DataDeliveryOptions opts) => UpdateQuestionnairePackageWithData(opts),
                  errors => 1);
        }

        private int CreateDataInterface(DataInterfaceOptions options)
        {
            _blaiseFileService.CreateDataInterfaceFile(options.ApplicationType, options.File);
            _blaiseFileService.CreateDataInterfaceFile(options.ApplicationType, options.File);

            return 0;
        }

        private int UpdateQuestionnairePackageWithData(DataDeliveryOptions options)
        {
            var auditOption = Convert.ToBoolean(options.Audit);
            _blaiseFileService.UpdateQuestionnairePackageWithData(options.ServerParkName, options.QuestionnaireName,
                options.File, auditOption);

            return 0;
        }
    }
}
