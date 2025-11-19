using System;
using System.IO;
using Blaise.Cli.Core.Interfaces;
using Blaise.Cli.Core.Services;
using Blaise.Nuget.Api.Api;
using Blaise.Nuget.Api.Contracts.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Blaise.Cli
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Blaise CLI");
                Console.WriteLine(string.Empty);

                var serviceProvider = new ServiceCollection()
                    .AddSingleton<ICommandService, CommandService>()
                    .AddTransient<IBlaiseFileService, BlaiseFileService>()
                    .AddTransient<IBlaiseFileApi, BlaiseFileApi>()
                    .AddTransient<IBlaiseQuestionnaireService, BlaiseQuestionnaireService>()
                    .AddTransient<IBlaiseQuestionnaireApi, BlaiseQuestionnaireApi>()
                    .BuildServiceProvider();

                var commandService = serviceProvider.GetService<ICommandService>();

                if (commandService == null)
                {
                    throw new ApplicationException("There was an error in creating the command parser");
                }

                commandService.ParseArguments(args);
            }
            catch (ArgumentException e)
            {
#if DEBUG
                Console.WriteLine($"Blaise configuration error: {e.Message}");
#else
                throw new ArgumentException($"Blaise configuration error: {e.Message}");
#endif
            }
            catch (FileNotFoundException e)
            {
#if DEBUG
                Console.WriteLine($"File Not Found: {e.Message}");
#else
                throw new FileNotFoundException($"File Not Found: {e.Message}");
#endif
            }
            catch (Exception e)
            {
#if DEBUG
                Console.WriteLine($"Error: {e.Message}");
#else
                throw new FileNotFoundException($"Error: {e.Message}");
#endif
            }
            finally
            {
#if DEBUG
                Console.WriteLine("Press any key to exit");
                Console.ReadKey();
#endif
            }
        }
    }
}
