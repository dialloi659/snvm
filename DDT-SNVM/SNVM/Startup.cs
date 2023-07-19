using DDT_Node_Tool.Contracts;
using DDT_Node_Tool.Contracts.Implementations;
using DDT_Node_Tool.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDT_Node_Tool
{
    internal static class Startup
    {
        public static IHost CreateBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(ConfigureApp)
                .ConfigureServices(ConfigureServices)
                .Build();
        }

        private static void ConfigureApp(HostBuilderContext app, IConfigurationBuilder builder)
        {
            builder.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
        }

        private static void ConfigureServices(HostBuilderContext app, IServiceCollection services)
        {
            services.AddOptions<NodeVersionOptions>()
                .BindConfiguration("NodeVersionOptions")
                .ValidateOnStart();

            services.AddSingleton<Runner>();

            services.AddTransient<INodeVersionDirectoryFetcher, NodeVersionDirectoryFetcher>();
            services.AddTransient<IFileSystemService, FileSystemService>();

            services.AddTransient<IVersionSelector, VersionSelector>();
            services.AddSingleton<IConsoleService, ConsoleService>();

            services.AddSingleton<IPathEnvironmentVariableService, PathEnvironmentVariableService>();

            services.AddSingleton<IEnvironmentService, EnvironmentService>();
        }

    }
}
