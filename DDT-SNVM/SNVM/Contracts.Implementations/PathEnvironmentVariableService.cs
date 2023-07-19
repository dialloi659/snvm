using DDT_Node_Tool.Models;
using Microsoft.Extensions.Options;

namespace DDT_Node_Tool.Contracts.Implementations;
public class PathEnvironmentVariableService : IPathEnvironmentVariableService
{
    private readonly IEnvironmentService _environmentService;

    public string PathEnvironmentVariable { get => _environmentService.GetEnvironmentVariable(PATH_VARIABLE_NAME, EnvironmentVariableTarget.User)!; }
    public IEnumerable<string>? PathEnvironmentVariableValues { get => PathEnvironmentVariable!.Split(';'); }

    NodeVersionOptions VersionOptions { get; }

    const string PATH_VARIABLE_NAME = "PATH";

    public PathEnvironmentVariableService(IOptions<NodeVersionOptions> options, IEnvironmentService environmentService)
    {
        VersionOptions = options.Value;
        _environmentService = environmentService;
    }

    private void UpdatePathVariable(string newValue)
    {
        _environmentService.SetEnvironmentVariable(PATH_VARIABLE_NAME, newValue, EnvironmentVariableTarget.User);
    }

    private bool IsExistsInPathEnvironmentVariable(string value)
    {
        return PathEnvironmentVariableValues!.Any(path => path.Contains(value, StringComparison.OrdinalIgnoreCase));
    }

    private IEnumerable<string> GetNewPathEnvironmentVariableValues(string selectedVersionFolderPath)
    {
        foreach (string path in PathEnvironmentVariableValues!)
        {
            if (path.Contains(VersionOptions.VersionNamePrefix!, StringComparison.OrdinalIgnoreCase))
            {
                yield return selectedVersionFolderPath;
            }
            else
            {
                yield return path;
            }
        }
    }

    public void AddToPathEnvironmentVariable(string? selectedNodeVersion)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(selectedNodeVersion);

        if (IsExistsInPathEnvironmentVariable(selectedNodeVersion))
            return;

        if (IsExistsInPathEnvironmentVariable(VersionOptions.VersionNamePrefix!))
        {
            var updatedPaths = GetNewPathEnvironmentVariableValues(selectedNodeVersion);
            string updatedPathEnvVariable = string.Join(';', updatedPaths);
            UpdatePathVariable(updatedPathEnvVariable);
        }
        else
        {
            UpdatePathVariable($"{PathEnvironmentVariable}{selectedNodeVersion}");
        }
    }

}