using UnityEngine;

public class LocolizeFileEditorModel : ILocalEditorModel
{
    private ILocalLoader _localLoader;
    private ILocolizeSerializer _parser;

    public LocolizeNode[] Nodes => _parser.GetDeserialized(_localLoader.LoadLocal()).Nodes;

    public int LanguagesCount => _parser.LanguagesCount;

    public LocolizeFileEditorModel(ILocalLoader loader, ILocolizeSerializer parser)
    {
        _localLoader = loader;
        _parser = parser;

        if (loader == null || parser == null)
        {
            Debug.LogError($"Critical error -> same argument is null; {nameof(loader)} = {loader}; {nameof(parser)} = {parser}");
        }
    }

    public void Save(LocolizeNode[] nodes, int languagesCount)
    {
        if (nodes == null)
        {
            Debug.LogError("Critical error -> can`t serialize a null Object");
            return;
        }

        _localLoader.WriteLocal(_parser.GetSerialized(new LocolizeDataTransferObject(languagesCount, nodes)));
    }

    public void Save(LocolizeNode[] nodes)
    {
        Save(nodes, _parser.LanguagesCount);
    }

    public LocolizeNode[] Load()
    {
        return _parser.GetDeserialized(_localLoader.LoadLocal()).Nodes;
    }

    public void IncrementLanguageCount()
    {
        _localLoader.WriteLocal(_parser.GetSerialized(new LocolizeDataTransferObject(_parser.LanguagesCount + 1, Nodes)));
    }

    public void DecrementLanguageCount()
    {
        if ((_parser.LanguagesCount - 1) < 1)
        {
            return;
        }

        _localLoader.WriteLocal(_parser.GetSerialized(new LocolizeDataTransferObject(_parser.LanguagesCount - 1, Nodes)));
    }
}