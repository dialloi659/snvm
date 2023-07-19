namespace DDT_Node_Tool.Contracts
{
    public interface IVersionSelector
    {
        string SelectVersion(IEnumerable<string> versions);
    }
}