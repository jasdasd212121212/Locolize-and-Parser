public static class LocolizeSystemFacade
{
    private static LocolizeModel _model;
    private static LocolizeSettingsModel _settingsModel;
    private static LanguagesHolderScriptableObject _holder;

    private static bool _initialized;

    public static void Initialize(ILocolizeSerializer parser, LanguagesHolderScriptableObject holder, LocolizeSettingsModel settings)
    {
        if (_initialized == true)
        {
            return;
        }

        _settingsModel = settings;
        _model = new LocolizeModel(parser);
        _holder = holder;

        _initialized = true;
    }

    public static string GetLoclized(string key)
    {
        return _model.Locolize(key, _settingsModel.CurrentLanguage, _holder);
    }
}