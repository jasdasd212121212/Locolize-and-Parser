using UnityEngine;
using Cysharp.Threading.Tasks;

public static class GoogleSheetsFacade
{
    private static GoogleSheetsWebClient _client;
    private static GoogleSheetsModel _model;
    private static SheetClassicIndexator _indexator;

    private static string _googleCsvSheet;

    public static bool Initialized { get; private set; }

    public static async UniTask Initialize(string sheetId, int listId)
    {
        if (Initialized == true)
        {
            return;
        }

        Debug.Log($"Initialization (ID: {sheetId})...");

        _client = new GoogleSheetsWebClient(sheetId);
        _model = new GoogleSheetsModel();
        _indexator = new SheetClassicIndexator();

        _googleCsvSheet = await _client.GetGoogleSheetContent(listId);
        _model.SetSheet(_googleCsvSheet);

        Debug.Log("Google sheets client succesfully initialized !!!");

        Initialized = true;
    }

    public static string GetContentOfCell(string cellAddress)
    {
        return _model.GetCell(_indexator.AddresToVector(cellAddress));
    }

    public static string GetContentOfCell(Vector2Int position)
    {
        return _model.GetCell(position);
    }

    public static async UniTask ChangeSheet(string sheetId, int listId)
    {
        _googleCsvSheet = await _client.GetGoogleSheetContent(sheetId, listId);
        _model.SetSheet(_googleCsvSheet);
    }

    public static int FindLowerLine()
    {
        int lowerY = int.MinValue;

        foreach (SheetCellDataTransferObject cell in _model.Cells)
        {
            if (cell.Position.y > lowerY)
            {
                lowerY = cell.Position.y;
            }
        }

        return lowerY;
    }
}