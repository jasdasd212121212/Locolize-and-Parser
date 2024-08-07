using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FileEditorModesStateMachine
{
    private List<IModeDrawer> _drawers = new List<IModeDrawer>();

    public IModeDrawer CurrentDrawer { get; private set; }

    public FileEditorModesStateMachine(IModeDrawer initialDrawer, IModeDrawer[] drawers)
    {
        if (drawers.Contains(initialDrawer) == false)
        {
            _drawers.Add(initialDrawer);
        }

        _drawers.AddRange(drawers.ToList());

        Switch(initialDrawer);
    }

    public void Switch(IModeDrawer drawer)
    {
        if (drawer == null)
        {
            Debug.LogError($"Critical error -> can`t switch to null drawer");
            return;
        }

        if (_drawers.Contains(drawer) == false)
        {
            Debug.LogError($"Critical error -> can`t switch to not defined drawer; Drawer: {drawer}");
            return;
        }

        CurrentDrawer = drawer;
    }

    public void Switch(int index)
    {
        if (index < 0 || index >= _drawers.Count)
        {
            Debug.LogError($"Critical error -> invalid index error; Index: {index}");
            return;
        }

        Switch(_drawers[index]);
    }

    public T Switch<T>() where T : IModeDrawer
    {
        foreach (IModeDrawer drawer in _drawers)
        {
            if (typeof(T).IsAssignableFrom(drawer.GetType()))
            {
                Switch(drawer);
                return (T)drawer;
            }
        }

        return default;
    }
}