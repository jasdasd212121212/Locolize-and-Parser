using UnityEngine;

public class SheetCellDataTransferObject
{
    public Vector2Int Position { get; private set; }
    public string Content { get; private set; }

    public SheetCellDataTransferObject(Vector2Int position, string content)
    {
        Position = position;
        Content = content;
    }
}