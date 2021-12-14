using System;
using Blaise.Cli.Core.Command;
using Blaise.Cli.Core.Interfaces;
using Blaise.Cli.Core.Services;
using Blaise.Nuget.Api.Api;
using Blaise.Nuget.Api.Contracts.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Blaise.Cli
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
            .AddSingleton<ICommandParser, CommandParser>()
            .AddTransient<IBlaiseFileService, BlaiseFileService>()
            .AddTransient<IBlaiseFileApi, BlaiseFileApi>()
            .BuildServiceProvider();

            var commandParser = serviceProvider.GetService<ICommandParser>();

            if (commandParser == null)
            {
                throw new ApplicationException("There was an error in creating the command parser");
            }

            commandParser.ParseArguments(args);
        }
    }
}
