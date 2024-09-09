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
        private readonly IBlaiseQuestionnaireService _blaiseQuestionnaireService;

        public CommandService(IBlaiseFileService blaiseFileService, IBlaiseQuestionnaireService blaiseQuestionnaireService)
        {
            _blaiseFileService = blaiseFileService;
            _blaiseQuestionnaireService = blaiseQuestionnaireService;
        }

        public int ParseArguments(object[] args)
        {
            var parser = new Parser(with =>
            {
                with.CaseInsensitiveEnumValues = true;
                with.AutoHelp = true;
                with.AutoVersion = true;
                with.HelpWriter = Console.Out;
            });

            return parser.ParseArguments<DataInterfaceOptions, DataDeliveryOptions, QuestionnaireOptions>((System.Collections.Generic.IEnumerable<string>)args)
              .MapResult(
                  (DataInterfaceOptions opts) => CreateDataInterface(opts),
                  (DataDeliveryOptions opts) => UpdateQuestionnairePackageWithData(opts),
                  (QuestionnaireOptions opts) => InstallQuestionnaire(opts),
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
            _blaiseFileService.UpdateQuestionnairePackageWithData(options.ServerParkName, options.QuestionnaireName,
                options.File, Convert.ToBoolean(options.Audit), Convert.ToInt32(options.BatchSize));

            return 0;
        }

        private int InstallQuestionnaire(QuestionnaireOptions options)
        {
            _blaiseQuestionnaireService.InstallQuestionnaire(options.QuestionnaireName, options.ServerParkName, options.QuestionnaireFile, options.InstallOptions);

            return 0;
        }
    }
}
