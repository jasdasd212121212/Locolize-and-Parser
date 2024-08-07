using System.Collections.Generic;
using UnityEngine;

public class CsvFormatParser
{
    private const string CELL_SEPARATOR = ",";

    private const char QUOTE = '"';
    private readonly string ARGUMENTED_CELL_SEPARATOR_FORWARD = $",{QUOTE}";
    private readonly string ARGUMENTED_CELL_SEPARATOR_BACK = $"{QUOTE},";

    public SheetCellDataTransferObject[] Parse(string file)
    {
        string[] lines = file.Split('\n');
        List<SheetCellDataTransferObject> cells = new List<SheetCellDataTransferObject>();

        for (int i = 0; i < lines.Length; i++)
        {
            cells.AddRange(ParseLine(lines[i], i));
        }

        return cells.ToArray();
    }

    private SheetCellDataTransferObject[] ParseLine(string line, int lineIndex)
    {
        string[] firstSeparated = line.Split(ARGUMENTED_CELL_SEPARATOR_FORWARD);
        List<SheetCellDataTransferObject> cells = new List<SheetCellDataTransferObject>();

        for (int i = 0; i < firstSeparated.Length; i++)
        {
            char firstSeparatedLastChar = firstSeparated[i][firstSeparated[i].Length - 1];

            if (firstSeparatedLastChar != QUOTE && firstSeparated[i].Contains(ARGUMENTED_CELL_SEPARATOR_BACK) == false)
            {
                SecondProcess(firstSeparated[i], CELL_SEPARATOR, lineIndex, i, ref cells);
            }
            else if (firstSeparated[i].Contains(ARGUMENTED_CELL_SEPARATOR_BACK))
            {
                SecondProcess(firstSeparated[i], ARGUMENTED_CELL_SEPARATOR_BACK, lineIndex, i, ref cells);
            }
            else
            {
                cells.Add(new SheetCellDataTransferObject(new Vector2Int(i, lineIndex), firstSeparated[i]));
            }
        }

        return PostProcessContent(cells);
    }

    private void SecondProcess(string firstSeparated, string separator, int lineIndex, int indexInSeparated, ref List<SheetCellDataTransferObject> cells)
    {
        string[] secondSeparated = firstSeparated.Split(separator);

        for (int j = 0; j < secondSeparated.Length; j++)
        {
            cells.Add(new SheetCellDataTransferObject(new Vector2Int(indexInSeparated + j, lineIndex), secondSeparated[j]));
        }
    }

    private SheetCellDataTransferObject[] PostProcessContent(List<SheetCellDataTransferObject> rawCells)
    {
        SheetCellDataTransferObject[] cells = new SheetCellDataTransferObject[rawCells.Count];

        for (int i = 0; i < rawCells.Count; i++)
        {
            if (rawCells[i].Content.Contains(QUOTE))
            {
                cells[i] = PostProcessCellContent(PostProcessCellContent(rawCells[i]));
            }
            else
            {
                cells[i] = rawCells[i];
            }
        }

        return cells;
    }

    private SheetCellDataTransferObject PostProcessCellContent(SheetCellDataTransferObject rawCell)
    {
        SheetCellDataTransferObject cell = rawCell;

        if (rawCell.Content[0] == QUOTE)
        {
            cell = new SheetCellDataTransferObject(rawCell.Position, rawCell.Content.Remove(0, 1));
        }
        else if (rawCell.Content[rawCell.Content.Length - 1] == QUOTE)
        {
            cell = new SheetCellDataTransferObject(rawCell.Position, rawCell.Content.Remove(rawCell.Content.Length - 1, 1));
        }

        return cell;
    }
}