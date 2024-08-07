using Cysharp.Threading.Tasks;
using System;
using UnityEditor;
using UnityEngine;

public class ImportGoogleSheetDrawer : IModeDrawer
{
    private ILocalEditorPresenter _presenter;
    private LanguagesHolderScriptableObject _languagesHolder;
    private GoogleSheetsToLocolizeModel _googleSheetsToLocolizeModel;

    private string _sheetId;
    private int _listIndex;
    private bool _sheetProcessing;

    private Action sheetImported;
    private Action backButtonPressed;

    private const string SHEET_ID_SAVE_KEY = "Editor_SheetId";

    public ImportGoogleSheetDrawer(ILocalEditorPresenter presenter, Action sheetImported, Action backButtonPressed)
    {
        if (PlayerPrefs.HasKey(SHEET_ID_SAVE_KEY))
        {
            _sheetId = PlayerPrefs.GetString(SHEET_ID_SAVE_KEY);
        }

        _presenter = presenter;
        _googleSheetsToLocolizeModel = new GoogleSheetsToLocolizeModel();

        this.sheetImported = sheetImported;
        this.backButtonPressed = backButtonPressed;
    }

    ~ImportGoogleSheetDrawer()
    {
        SaveSheetId();
    }

    public void SetLanguages(LanguagesHolderScriptableObject holder)
    {
        _languagesHolder = holder;
    }

    public void Draw()
    {
        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.LabelField("Shield ID: ");
        _sheetId = EditorGUILayout.TextField(_sheetId);

        EditorGUILayout.LabelField("List ID: ");
        _listIndex = EditorGUILayout.IntField(_listIndex);

        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space(10);

        if (_sheetProcessing == false)
        {
            if (GUILayout.Button("Back"))
            {
                backButtonPressed?.Invoke();
            }
        }

        EditorGUILayout.Space(30);

        if (GUILayout.Button("Import"))
        {
            SaveSheetId();

            _sheetProcessing = true;
            _googleSheetsToLocolizeModel.ImportGoogleSheet(_sheetId, 0, _presenter, () => { _sheetProcessing = false; sheetImported?.Invoke(); }).Forget();
        }
    }

    private void SaveSheetId()
    {
        PlayerPrefs.SetString(SHEET_ID_SAVE_KEY, _sheetId);
    }
}