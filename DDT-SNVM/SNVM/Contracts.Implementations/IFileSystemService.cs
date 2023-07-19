namespace DDT_Node_Tool.Contracts.Implementations
{
    public interface IFileSystemService
    {
        bool DirectoryExists(string path);
        string[] GetDirectories(string path);
    }
}