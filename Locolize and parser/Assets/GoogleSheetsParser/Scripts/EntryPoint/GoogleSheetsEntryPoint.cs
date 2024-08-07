using Cysharp.Threading.Tasks;
using System.IO;
using UnityEngine;

public class GoogleSheetsEntryPoint
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Construct()
    {
        string file = File.ReadAllText($"{Application.streamingAssetsPath}/config.txt");

        if (file.Split('\n')[1] == "Off")
        {
            return;
        }

        GoogleSheetsFacade.Initialize(file.Split('\n')[0].Trim(), 0).Forget();
    }
}