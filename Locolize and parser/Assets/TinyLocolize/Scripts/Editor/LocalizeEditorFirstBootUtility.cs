using UnityEditor;

public class LocalizeEditorFirstBootUtility
{
    [MenuItem("Locolize/Boot")]
    public static void Boot()
    {
        ILocolizeSerializer parser = new LocalizeFileParser();
        ILocalLoader loader = new LocalFileStreamingAssetsLoader();

        loader.WriteLocal(parser.GetSerialized(new LocolizeDataTransferObject(1, new LocolizeNode[0])));
    }    
}