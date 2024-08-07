using UnityEngine;
using System.Linq;

public class LocolizeSettingsModel
{
    private LanguageSettingsScriptableObject[] _languages;
    private LocolizeSettingsSaver _saver;
    private int _currentLanguageIndex;

    public LanguageSettingsScriptableObject[] Languages => _languages;
    public int CurrentLanguageIndex => _currentLanguageIndex;
    public LanguageSettingsScriptableObject CurrentLanguage { get; private set; }

    public LocolizeSettingsModel(LanguagesHolderScriptableObject languages)
    {
        _languages = languages.Languages;

        _saver = new LocolizeSettingsSaver(this);
        _saver.Load();
    }

    public void SetLanguage(int languageId)
    {
        if (languageId < 0 || languageId > (_languages.Length))
        {
            Debug.LogError($"Ciritical error -> can`t set invalid languageId = {languageId}");
            return;
        }

        CurrentLanguage = _languages[languageId];
        _currentLanguageIndex = languageId;

        _saver.Save();

        LocolizeSettingObservableFacade.languageChanged?.Invoke();
    }

    public void SetLanguage(LanguageSettingsScriptableObject language)
    {
        if (_languages.ToList().Contains(language) == false)
        {
            Debug.LogError($"Critical error -> can`t set language: {language} because languages holder are not contains {language} language");
            return;
        }

        for (int i = 0; i < _languages.Length; i++)
        {
            if (_languages[i] == language)
            {
                SetLanguage(i); 
                return;
            }
        }
    }
}