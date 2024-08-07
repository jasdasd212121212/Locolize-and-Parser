using System.Text.RegularExpressions;

public class SheetAdressProcessor
{
    private readonly char[] _numbers = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

    public string GetColumtIndex(string address)
    {
        return Regex.Replace(address, @"[\d-]", string.Empty);
    }

    public int GetLineIndex(string address)
    {
        string result = "";

        for (int i = 0; i < address.Length; i++)
        {
            for (int j = 0; j < _numbers.Length; j++)
            {
                if (address[i] == _numbers[j])
                {
                    result += address[i];
                }
            }
        }

        return int.Parse(result);
    }
}