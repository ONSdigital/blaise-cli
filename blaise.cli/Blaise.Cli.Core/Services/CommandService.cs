using System;
using System.Linq;
using Blaise.Cli.Core.Interfaces;
using Blaise.Cli.Core.Models;
using CommandLine;
using Newtonsoft.Json;

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

        public int ParseArguments(string[] args)
        {
            var parser = new Parser(with =>
            {
                with.CaseInsensitiveEnumValues = true;
                with.AutoHelp = true;
                with.AutoVersion = true;
                with.HelpWriter = Console.Out;
            });

            return parser.ParseArguments<DataInterfaceOptions, DataDeliveryOptions, QuestionnaireOptions>(args)
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
            var overwriteExistingData = Convert.ToBoolean(options.OverwriteExistingData);
            _blaiseQuestionnaireService.InstallQuestionnaire(options.QuestionnaireName, options.ServerParkName, options.QuestionnaireFile, overwriteExistingData);

            return 0;
        }
    }
}
