using System.Collections.Generic;
using UnityEngine;

public class SheetClassicIndexator
{
    private Dictionary<string, int> _stringHorizontalAdress = new Dictionary<string, int>();
    private SheetAdressProcessor _addressProcessor;

    public SheetClassicIndexator()
    {
        _addressProcessor = new SheetAdressProcessor();
        GenerateHorizontalAdresses(16256);
    }

    public Vector2Int AddresToVector(string addres)
    {
        addres = addres.ToUpper();

        int lineIndex = _addressProcessor.GetLineIndex(addres);
        int columnIndex;

        _stringHorizontalAdress.TryGetValue(_addressProcessor.GetColumtIndex(addres), out columnIndex);

        return new Vector2Int(Mathf.Clamp(columnIndex - 1, 0, int.MaxValue), Mathf.Clamp(lineIndex - 1, 0, int.MaxValue));
    }

    private void GenerateHorizontalAdresses(int count)
    {
        for (int i = 0; i < count; i++)
        {
            _stringHorizontalAdress.Add(ColumnIndexToColumnLetter(i), i);
        }
    }

    private string ColumnIndexToColumnLetter(int colIndex)
    {
        int div = colIndex;
        string colLetter = string.Empty;
        int mod = 0;

        while (div > 0)
        {
            mod = (div - 1) % 26;
            colLetter = (char)(65 + mod) + colLetter;
            div = (int)((div - mod) / 26);
        }

        return colLetter;
    }
}