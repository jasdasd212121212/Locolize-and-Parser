public interface ILocolizeSerializer
{
    int LanguagesCount { get; }
    LocolizeDataTransferObject Locolize { get; }
    LocolizeDataTransferObject GetDeserialized(string fileText);
    string GetSerialized(LocolizeDataTransferObject data);
}