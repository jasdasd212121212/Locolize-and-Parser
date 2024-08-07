using UnityEngine;

public class GoogleSheetsModel
{
    private CsvFormatParser _parser;

    private string _sheetText;

    public SheetCellDataTransferObject[] Cells { get; private set; }

    public GoogleSheetsModel()
    {
        _parser = new CsvFormatParser();
    }

    public GoogleSheetsModel(string csvSheetText) : this()
    {
        SetSheet(csvSheetText);
    }

    public void SetSheet(string csvSheetText)
    {
        Cells = _parser.Parse(csvSheetText);
    }

    public string GetCell(Vector2Int position)
    {
        if (position.x < 0 || position.y < 0)
        {
            Debug.LogError($"Critical error -> invalid cell position; POS: {position}");
            return $"CANT GET CELL: {position}";
        }

        foreach (SheetCellDataTransferObject cell in Cells)
        {
            if (cell.Position == position)
            {
                return cell.Content;
            }
        }

        return $"MISSING CELL: {position}";
    }
}