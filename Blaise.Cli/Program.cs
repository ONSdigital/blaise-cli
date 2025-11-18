namespace Blaise.Cli
{
    using System;
    using System.IO;
    using Blaise.Cli.Core.Interfaces;
    using Blaise.Cli.Core.Services;
    using Blaise.Nuget.Api.Api;
    using Blaise.Nuget.Api.Contracts.Interfaces;
    using Microsoft.Extensions.DependencyInjection;

    internal class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Blaise CLI");
                Console.WriteLine(string.Empty);

                var serviceProvider = ConfigureServices();
                var commandService = serviceProvider.GetService<ICommandService>();

                if (commandService == null)
                {
                    throw new ApplicationException("There was an error in creating the command parser");
                }

                commandService.ParseArguments(args);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
            finally
            {
                Console.WriteLine("Press any key to exit");
                Console.ReadKey();
            }
        }

        private static ServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();
            services.AddSingleton<ICommandService, CommandService>();
            services.AddTransient<IBlaiseFileService, BlaiseFileService>();
            services.AddTransient<IBlaiseFileApi, BlaiseFileApi>();
            services.AddTransient<IBlaiseQuestionnaireService, BlaiseQuestionnaireService>();
            services.AddTransient<IBlaiseQuestionnaireApi, BlaiseQuestionnaireApi>();

            return services.BuildServiceProvider();
        }
    }
}
