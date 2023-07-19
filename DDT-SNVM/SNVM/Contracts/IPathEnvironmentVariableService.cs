namespace DDT_Node_Tool.Contracts
{
    public interface IPathEnvironmentVariableService
    {
        string? PathEnvironmentVariable { get; }
        IEnumerable<string>? PathEnvironmentVariableValues { get; }
        void AddToPathEnvironmentVariable(string? selectedNodeVersion);
    }
}
