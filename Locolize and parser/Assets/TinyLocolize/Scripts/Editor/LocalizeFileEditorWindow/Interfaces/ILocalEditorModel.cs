public interface ILocalEditorModel
{
    LocolizeNode[] Nodes { get; }
    int LanguagesCount { get; }
    void Save(LocolizeNode[] nodes);
    void Save(LocolizeNode[] nodes, int languagesCount);
    void IncrementLanguageCount();
    void DecrementLanguageCount();
    LocolizeNode[] Load();
}