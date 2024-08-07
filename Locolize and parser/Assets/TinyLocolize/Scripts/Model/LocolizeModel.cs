using UnityEngine;
using System.Linq;

public class LocolizeModel
{
    public ILocolizeSerializer _parser;

    public LocolizeModel(ILocolizeSerializer parser)
    {
        _parser = parser;
    }

    public string Locolize(string key, LanguageSettingsScriptableObject language, LanguagesHolderScriptableObject holder)
    {
        for (int i = 0; i < _parser.Locolize.Nodes.Length; i++)
        {
            if (_parser.Locolize.Nodes[i].Key == key)
            {
                return _parser.Locolize.Nodes[i].Locolizes[holder.Languages.ToList().IndexOf(language)];
            }
        }

        Debug.LogError($"Critical error -> local are not contains key: {key}");
        return "ERROR -> Not existing error";
    }
}