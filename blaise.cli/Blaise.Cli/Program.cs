using Blaise.Cli.Core.Interfaces;
using Blaise.Cli.Core.Services;
using Blaise.Nuget.Api.Api;
using Blaise.Nuget.Api.Contracts.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;

namespace Blaise.Cli
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Blaise ClI");
                Console.WriteLine("");

                var serviceProvider = new ServiceCollection()
                    .AddSingleton<ICommandService, CommandService>()
                    .AddTransient<IBlaiseFileService, BlaiseFileService>()
                    .AddTransient<IBlaiseFileApi, BlaiseFileApi>()
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
                Console.WriteLine($"Blaise configuration error: {e.Message}");
            }
            catch (FileNotFoundException ee)
            {
                Console.WriteLine($"Error: {ee.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error:: {ex.Message}");
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
