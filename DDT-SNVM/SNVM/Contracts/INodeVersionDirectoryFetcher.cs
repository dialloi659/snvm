namespace DDT_Node_Tool.Contracts
{
    public interface INodeVersionDirectoryFetcher
    {
        Dictionary<string, string> GetNodeVersionDirectories();
    }
}