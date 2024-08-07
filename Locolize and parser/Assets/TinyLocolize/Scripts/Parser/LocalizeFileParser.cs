using System.Collections.Generic;
using System.Text;

public class LocalizeFileParser : ILocolizeSerializer
{
    public LocolizeDataTransferObject Locolize { get; private set; }
    public int LanguagesCount { get; private set; }

    public LocalizeFileParser() { }

    public LocalizeFileParser(string fileText)
    {
        Locolize = GetDeserialized(fileText);
    }

    public LocolizeDataTransferObject GetDeserialized(string fileText)
    {
        string[] lines = fileText.Split(ParserConfigSettings.SEPARATING_SYMBOL);
        List<LocolizeNode> locolizeObjects = new List<LocolizeNode>();

        LanguagesCount = int.Parse(lines[0]);

        for (int i = 1; i < (lines.Length - 1); i++)
        {
            string[] keySplittedLine = lines[i].Split(ParserConfigSettings.SEPARATED_KEY_SYMBOL);

            string key = keySplittedLine[0].Replace("\n", "").Trim();
            string[] arguments = keySplittedLine[1].Split(ParserConfigSettings.SEPARATED_ARGUMENTS_SYMBOL);

            locolizeObjects.Add(new LocolizeNode(key, arguments));
        }

        return new LocolizeDataTransferObject(LanguagesCount, locolizeObjects.ToArray());
    }

    public string GetSerialized(LocolizeDataTransferObject data)
    {
        StringBuilder stringBuilder = new StringBuilder();

        stringBuilder.AppendLine(data.LanguagesCount.ToString() + ParserConfigSettings.SEPARATING_SYMBOL);

        for (int i = 0; i < data.Nodes.Length; i++)
        {
            string arguments = "";

            for (int j = 0; j < data.Nodes[i].Locolizes.Length; j++)
            {
                if (j < (data.Nodes[i].Locolizes.Length - 1))
                {
                    arguments += $"{data.Nodes[i].Locolizes[j]}{ParserConfigSettings.SEPARATED_ARGUMENTS_SYMBOL}";
                }
                else
                {
                    arguments += data.Nodes[i].Locolizes[j];
                }
            }

            stringBuilder.AppendLine($"{data.Nodes[i].Key}{ParserConfigSettings.SEPARATED_KEY_SYMBOL}{arguments}{ParserConfigSettings.SEPARATING_SYMBOL}");
        }

        return stringBuilder.ToString();
    }
}