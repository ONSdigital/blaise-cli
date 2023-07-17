﻿using System;
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
    }
}