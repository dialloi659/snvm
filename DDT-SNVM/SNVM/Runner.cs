using DDT_Node_Tool.Contracts;
using DDT_Node_Tool.Contracts.Implementations;
using DDT_Node_Tool.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Spectre.Console;

namespace DDT_Node_Tool;
internal sealed class Runner
{
    private readonly INodeVersionDirectoryFetcher _versionDirectoryFetcher;
    private readonly IVersionSelector _versionSelector;
    private readonly IPathEnvironmentVariableService _pathEnvironmentVariableService;

    public Runner(
        INodeVersionDirectoryFetcher versionDirectoryFetcher,
        IVersionSelector versionSelector,
        IPathEnvironmentVariableService pathEnvironmentVariableService,
        IOptions<NodeVersionOptions> options
        )
    {
        _versionDirectoryFetcher = versionDirectoryFetcher;
        _versionSelector = versionSelector;
        _pathEnvironmentVariableService = pathEnvironmentVariableService;
    }

    public void Run()
    {
        // Get all available versions
        IDictionary<string, string> versionDirectoriesInfos = _versionDirectoryFetcher.GetNodeVersionDirectories();
        if (!versionDirectoriesInfos.Any())
        {
            AnsiConsole.MarkupLineInterpolated($"[orange1]No nodejs version folder found.[/]");
        }
        // Get selected version
        string selectedNodeVersion = _versionSelector.SelectVersion(versionDirectoriesInfos.Keys.ToList());

        // Set selected version for current user
        _pathEnvironmentVariableService.AddToPathEnvironmentVariable(versionDirectoriesInfos[selectedNodeVersion]);

        AnsiConsole.MarkupLineInterpolated($"[green]Done.[/]");
    }
}
