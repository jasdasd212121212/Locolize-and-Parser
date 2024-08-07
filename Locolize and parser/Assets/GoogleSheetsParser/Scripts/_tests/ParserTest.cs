using UnityEngine;

public class ParserTest : MonoBehaviour
{
    [SerializeField][TextArea] private string _testCsvContent;

    private void Start()
    {
        SheetCellDataTransferObject[] cells = new CsvFormatParser().Parse(_testCsvContent);

        foreach (SheetCellDataTransferObject cell in cells)
        {
            Debug.Log($"Pos: {cell.Position}, Content: {cell.Content}");
        }
    }
}