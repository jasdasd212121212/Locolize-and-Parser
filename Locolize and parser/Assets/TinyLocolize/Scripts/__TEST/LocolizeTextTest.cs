using UnityEngine;

public class LocolizeTextTest : MonoBehaviour
{
    [SerializeField] private string key;
    [SerializeField] private TMP_TextLocolizer _text;

    [ContextMenu("Run")]
    private void Run()
    {
        _text.SetText(key);
    }
}