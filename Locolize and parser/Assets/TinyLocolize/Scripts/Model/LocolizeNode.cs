public class LocolizeNode
{
    public string Key { get; private set; }
    public string[] Locolizes { get; private set; }

    public LocolizeNode(string key, string[] locolizes)
    {
        Key = key;
        Locolizes = locolizes;
    }
}