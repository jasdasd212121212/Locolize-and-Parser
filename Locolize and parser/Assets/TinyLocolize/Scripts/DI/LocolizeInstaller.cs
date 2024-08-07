using UnityEngine;
using Zenject;

public class LocolizeInstaller : MonoInstaller
{
    [SerializeField] private LanguagesHolderScriptableObject _languages;

    public override void InstallBindings()
    {
        ILocalLoader loader = new LocalFileStreamingAssetsLoader();
        LocolizeSettingsModel settingsModel = new LocolizeSettingsModel(_languages);

        Container.Bind<LocolizeSettingsModel>().FromInstance(settingsModel).AsSingle().NonLazy();
        LocolizeSystemFacade.Initialize(new LocalizeFileParser(loader.LoadLocal()), _languages, settingsModel);
    }
}