using Blaise.Cli.Core.Interfaces;
using Blaise.Cli.Core.Models;
using CommandLine;
using System;
using System.Threading;

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

            Console.WriteLine("Create Data Interface options");
            Console.WriteLine($"    Application Type: {options.ApplicationType}");
            Console.WriteLine($"    File: {options.File}");
            _blaiseFileService.CreateDataInterfaceFile(options.ApplicationType, options.File);
            return 0;
        }

        private int UpdateQuestionnairePackageWithData(DataDeliveryOptions options)
        {
            Console.WriteLine("Data Delivery options");
            Console.WriteLine($"    Server Park: {options.ServerParkName}");
            Console.WriteLine($"    Questionnaire Name: {options.QuestionnaireName}");
            Console.WriteLine($"    Questionnaire File: {options.File}");

            var auditOption = Convert.ToBoolean(options.Audit);
            Console.ForegroundColor = auditOption ? ConsoleColor.Green : ConsoleColor.Red;
            Console.WriteLine($"    Audit Enabled: {options.Audit}");
            Console.ResetColor();

            // Create a new thread for the spinning cursor
            var cursorThread = new Thread(SpinningCursor);
            cursorThread.Start();

            // Run task
            RunLongProcess(options);

            // Stop the spinning cursor
            cursorThread.Abort();
            Console.WriteLine("File created successfully");
            return 0;
        }

        private void RunLongProcess(DataDeliveryOptions options)
        {
            // Simulate a long process by introducing a delay
            Thread.Sleep(5000);

            var auditOption = Convert.ToBoolean(options.Audit);
            _blaiseFileService.UpdateQuestionnairePackageWithData(options.ServerParkName, options.QuestionnaireName,
                               options.File, auditOption);
        }

        private static void SpinningCursor()
        {
            // Define an array of characters for the spinning cursor animation
            char[] spinner = { '|', '/', '-', '\\' };
            var spinnerIndex = 0;

            // Continuously update and display the spinning cursor
            while (true)
            {
                Console.Write($"Processing: {spinner[spinnerIndex]}");
                spinnerIndex = (spinnerIndex + 1) % spinner.Length;
                Console.SetCursorPosition(Console.CursorLeft - 13, Console.CursorTop);
                Thread.Sleep(300);
            }
        }
    }
}
