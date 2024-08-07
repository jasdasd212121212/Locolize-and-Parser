using UnityEngine;

public class LocolizeSettingsSaver
{
    private LocolizeSettingsModel _model;

    public LocolizeSettingsSaver(LocolizeSettingsModel model) 
    {
        _model = model;
    }

    ~LocolizeSettingsSaver()
    {
        Save();
    }

    public void Save()
    {
        PlayerPrefs.SetInt(LocolizeSaverConfigConstants.LOCLIZE_SAVE_KEY, _model.CurrentLanguageIndex);
    }

    public void Load()
    {
        if (PlayerPrefs.HasKey(LocolizeSaverConfigConstants.LOCLIZE_SAVE_KEY) == false)
        {
            _model.SetLanguage(0);

            return;
        }

        _model.SetLanguage(PlayerPrefs.GetInt(LocolizeSaverConfigConstants.LOCLIZE_SAVE_KEY));
    }
}