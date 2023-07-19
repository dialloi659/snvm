using Spectre.Console;

namespace DDT_Node_Tool.Contracts.Implementations;

public class VersionSelector : IVersionSelector
{
    private readonly IConsoleService _consoleService;

    public VersionSelector(IConsoleService consoleService)
    {
        _consoleService = consoleService;
    }

    public string SelectVersion(IEnumerable<string> versions)
    {
        ArgumentNullException.ThrowIfNull(versions);

        if (!versions.Any())
            throw new ArgumentException("The list of version to select must not be empty.");
            
        return _consoleService.PromptSelection(versions, "Select available node version below:");
    }
}

