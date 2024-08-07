using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TMP_TextLocolizer : MonoBehaviour
{
    private TextMeshProUGUI _text;

    private string _textKey;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
        _textKey = _text.text;

        LocolizeSettingObservableFacade.languageChanged += OnLocalChanged;
        OnLocalChanged();
    }

    private void OnDestroy()
    {
        LocolizeSettingObservableFacade.languageChanged -= OnLocalChanged;
    }

    private void OnLocalChanged()
    {
        SetText(_textKey);
    }

    public void SetText(string key)
    {
        _textKey = key;
        _text.text = LocolizeSystemFacade.GetLoclized(key);
    }
}