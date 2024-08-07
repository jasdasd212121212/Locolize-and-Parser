using System.Collections.Generic;

public class LocolizeFileEditorUndoModel
{
    private Stack<LocolizeDataTransferObject> _undoHistory = new Stack<LocolizeDataTransferObject>();
    private Stack<LocolizeDataTransferObject> _rendoHistory = new Stack<LocolizeDataTransferObject>();

    private ILocalEditorPresenter _presenter;

    private bool _initialized;

    public void Initialize(ILocalEditorPresenter presenter)
    {
        if (_initialized == true)
        {
            return;
        }

        _presenter = presenter;
        
        _initialized = true;
    }

    public void WriteHistory(LocolizeDataTransferObject data)
    {
        _undoHistory.Push(data);
    }

    public LocolizeDataTransferObject Undo(out bool hasHistory)
    {
        LocolizeDataTransferObject undo = null;

        try
        {
            undo = _undoHistory.Pop();
            _rendoHistory.Push(_presenter.DeserializeData());
        }
        catch
        {
            hasHistory = false;
            return null;
        }
        
        hasHistory = true;
        return undo;
    }

    public LocolizeDataTransferObject Redo(out bool hasHistory)
    {
        LocolizeDataTransferObject rendo = null;

        try
        {
            rendo = _rendoHistory.Pop();
            _undoHistory.Push(_presenter.DeserializeData());
        }
        catch
        {
            hasHistory = false;
            return null;
        }
        
        hasHistory = true;
        return rendo;
    }
}