using System;
using UnityEditor;
using UnityEngine;

public class SeleclModeDrawer : IModeDrawer
{
    private ILocalEditorPresenter _presenter;
    private LanguagesHolderScriptableObject _languagesHolder;

    private Vector2 _scroll;
    private string _searchQuery = string.Empty;

    private Action<LocolizeNode> buttonPressed;
    private Action backButtonPressed;

    public SeleclModeDrawer(ILocalEditorPresenter presenter, Action<LocolizeNode> buttonPressed, Action backButtonPressed)
    {
        _presenter = presenter;

        this.buttonPressed = buttonPressed;
        this.backButtonPressed = backButtonPressed;
    }

    public void Draw()
    {
        if (GUILayout.Button("Back"))
        {
            backButtonPressed?.Invoke();
        }

        EditorGUILayout.Space(30);

        DrawUndoMenu();
        DrawSearchLine();

        EditorGUILayout.Space(5);

        LocolizeNode[] nodes = DrawButtons();

        EditorGUILayout.Space(10);

        DrawMenu(nodes);
        DrawLanguageMenu();
    }

    public void SetLanguages(LanguagesHolderScriptableObject holder)
    {
        _languagesHolder = holder;
    }

    private void DrawButton(LocolizeNode node)
    {
        if (GUILayout.Button(node.Key))
        {
            buttonPressed?.Invoke(node);
        }
    }

    private LocolizeNode[] DrawButtons()
    {
        LocolizeNode[] nodes = _presenter.Deserialize();

        _scroll = EditorGUILayout.BeginScrollView(_scroll);

        foreach (LocolizeNode node in nodes)
        {
            if (string.IsNullOrEmpty(_searchQuery) || node.Key.Contains(_searchQuery))
            {
                DrawButton(node);
            }
        }

        EditorGUILayout.EndScrollView();

        return nodes;
    }

    private void DrawMenu(LocolizeNode[] nodes)
    {
        EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button("Add"))
        {
            _presenter.AddNode($"New node {Guid.NewGuid().ToString().Split("-")[0]}");
        }

        if (GUILayout.Button("Remove last"))
        {
            try
            {
                _presenter.RemoveNode(nodes[nodes.Length - 1]);
            }
            catch { }
        }

        EditorGUILayout.EndHorizontal();
    }

    private void DrawLanguageMenu()
    {
        EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button("Add language"))
        {
            _presenter.AddLanguage(_languagesHolder);
        }

        if (GUILayout.Button("Remove last language"))
        {
            _presenter.RemoveLanguage();
        }

        EditorGUILayout.EndHorizontal();
    }

    private void DrawUndoMenu()
    {
        EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button("Undo"))
        {
            _presenter.Undo();
        }

        if (GUILayout.Button("Rendo"))
        {
            _presenter.Rendo();
        }

        EditorGUILayout.EndHorizontal();
    }

    private void DrawSearchLine()
    {
        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.LabelField("Search: ");
        _searchQuery = EditorGUILayout.TextField(_searchQuery);

        EditorGUILayout.EndHorizontal();
    }
}