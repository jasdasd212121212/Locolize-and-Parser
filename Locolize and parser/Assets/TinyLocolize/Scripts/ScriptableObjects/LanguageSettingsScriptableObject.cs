using UnityEngine;

[CreateAssetMenu(fileName = "Language", menuName = "Locolize/Language")]
public class LanguageSettingsScriptableObject : ScriptableObject
{
    [SerializeField] private string _name;

    public string Name => _name;
}