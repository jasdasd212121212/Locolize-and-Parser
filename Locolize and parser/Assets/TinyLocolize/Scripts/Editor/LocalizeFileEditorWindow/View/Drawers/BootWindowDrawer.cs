using System;
using UnityEditor;
using UnityEngine;

public class BootWindowDrawer : IModeDrawer
{
    private Action<LanguagesHolderScriptableObject> bootedCallback;
    private Action<LanguagesHolderScriptableObject> openSheetImporter;

    public LanguagesHolderScriptableObject Holder { get; private set; }

    private const string PATH = "Locolize/_Holder";

    public BootWindowDrawer(Action<LanguagesHolderScriptableObject> bootedCallback, Action<LanguagesHolderScriptableObject> openSheetImporter) 
    {
        Holder = Resources.Load<LanguagesHolderScriptableObject>(PATH);

        this.bootedCallback = bootedCallback;
        this.openSheetImporter = openSheetImporter;
    }

    [Obsolete]
    public void Draw()
    {
        Holder = (LanguagesHolderScriptableObject)EditorGUILayout.ObjectField(Holder, typeof(LanguagesHolderScriptableObject));

        EditorGUILayout.Space(25);

        if (Holder != null)
        {
            if (GUILayout.Button("Edit local manually"))
            {
                CallCallback();
            }

            if (GUILayout.Button("Import google sheet"))
            {
                openSheetImporter?.Invoke(Holder);
            }
        }
        else
        {
            EditorGUILayout.LabelField($"Holder cant be loaded by path: <{PATH}>");
        }
    }

    private void CallCallback()
    {
        bootedCallback?.Invoke(Holder);
    }
}