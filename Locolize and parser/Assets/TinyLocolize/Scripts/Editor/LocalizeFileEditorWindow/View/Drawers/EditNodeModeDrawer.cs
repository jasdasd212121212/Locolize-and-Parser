using System;
using UnityEditor;
using UnityEngine;

public class EditNodeModeDrawer : IModeDrawer
{
    private LocolizeNode _node;
    private LanguagesHolderScriptableObject _holder;

    private ILocalEditorPresenter _presenter;

    private string _name;
    private string[] _locolizes;

    private Action savedCallback;

    public EditNodeModeDrawer(ILocalEditorPresenter presenter, Action saved)
    {
        _presenter = presenter;
        savedCallback = saved;
    }

    public void SetNode(LocolizeNode node, LanguagesHolderScriptableObject holder)
    {
        _holder = holder;

        _node = node;
        _locolizes = new string[_node.Locolizes.Length];

        _name = _node.Key;

        for (int i = 0; i < _node.Locolizes.Length; i++)
        {
            _locolizes[i] = _node.Locolizes[i];
        }
    }

    public void Draw()
    {
        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.LabelField("KEY: ");
        _name = EditorGUILayout.TextField(_name);

        EditorGUILayout.EndHorizontal();

        GUILayout.Space(20);

        EditorGUILayout.LabelField("Localizations:");

        GUILayout.Space(5);

        for (int i = 0; i < _node.Locolizes.Length; i++)
        {
            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.LabelField(_holder.Languages[i].Name);
            _locolizes[i] = EditorGUILayout.TextField(_locolizes[i]);

            EditorGUILayout.EndHorizontal();
        }

        GUILayout.Space(150);

        if (GUILayout.Button("Save"))
        {
            _presenter.ReplaceNode(_node, new LocolizeNode(_name, _locolizes));

            savedCallback?.Invoke();
        }

        if (GUILayout.Button("Back"))
        {
            savedCallback?.Invoke();
        }

        EditorGUILayout.Space(20);

        if (GUILayout.Button("Remove"))
        {
            _presenter.RemoveNode(_node);

            savedCallback?.Invoke();
        }
    }
}