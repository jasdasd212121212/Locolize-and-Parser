using UnityEditor;

public class LocolizeFileEditorEntryPoint : EditorWindow
{
    [MenuItem("Locolize/Editor")]
    public static void OpenWindow()
    {
        LocolizeFileEditorUndoModel undoModel = new LocolizeFileEditorUndoModel();

        ILocalEditorView locolizeWindow = GetWindow<LocolizeFileEditorWindowView>();
        ILocalEditorModel locolizeWindowModel = new LocolizeFileEditorModel(new LocalFileStreamingAssetsLoader(), new LocalizeFileParser());
        ILocalEditorPresenter locolizeWindowPresenter = new LocolizeFileEditorPresenter(locolizeWindowModel, undoModel);

        undoModel.Initialize(locolizeWindowPresenter);

        locolizeWindow.Construct(locolizeWindowPresenter);
    }
}