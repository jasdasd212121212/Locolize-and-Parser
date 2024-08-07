using UnityEngine;
using Zenject;

public class SetSettingsTest : MonoBehaviour
{
    [SerializeField][Min(0)] private int _languageId;

    [Inject]private LocolizeSettingsModel _settingsModel;

    [ContextMenu("Run")]
    private void Run()
    {
        _settingsModel.SetLanguage(_languageId);
    }
}