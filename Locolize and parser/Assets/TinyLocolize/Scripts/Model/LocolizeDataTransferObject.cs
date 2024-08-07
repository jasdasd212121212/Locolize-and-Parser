public class LocolizeDataTransferObject
{
    public int LanguagesCount { get; private set; } 
    public LocolizeNode[] Nodes { get; private set; }

    public LocolizeDataTransferObject(int languagesCount, LocolizeNode[] nodes)
    {
        LanguagesCount = languagesCount;
        Nodes = nodes;
    }
}