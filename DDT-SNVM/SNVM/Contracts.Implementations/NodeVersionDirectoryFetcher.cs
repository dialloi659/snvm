using DDT_Node_Tool.Contracts;
using DDT_Node_Tool.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Text.RegularExpressions;

namespace DDT_Node_Tool.Contracts.Implementations;

public class NodeVersionDirectoryFetcher : INodeVersionDirectoryFetcher
{
    NodeVersionOptions VersionOptions { get; }
    private readonly IFileSystemService _fileSystemService;

    public NodeVersionDirectoryFetcher(IOptions<NodeVersionOptions> options, IFileSystemService fileSystemService)
    {
        VersionOptions = options.Value;
        _fileSystemService = fileSystemService;
    }

    public Dictionary<string, string> GetNodeVersionDirectories()
    {
        // Check if parent directory path is valid
        if (!_fileSystemService.DirectoryExists(VersionOptions.VersionsDirectory!))
        {
            throw new ArgumentException($"{VersionOptions.VersionsDirectory} directory is not found.\nPlease, check that the folder exists and it is correctly filled in the appsettings.json file");
        }

        var directories = _fileSystemService.GetDirectories(VersionOptions.VersionsDirectory!);

        var directoryDict = directories
            .Select(directory => new DirectoryInfo(directory))
            .Where(directoryInfo => VersionOptions.GetVersionDirectoryNameRegex().IsMatch(directoryInfo.Name))
            .ToDictionary(directoryInfo => directoryInfo.Name, directoryInfo => directoryInfo.FullName);

        return directoryDict;
    }
}
