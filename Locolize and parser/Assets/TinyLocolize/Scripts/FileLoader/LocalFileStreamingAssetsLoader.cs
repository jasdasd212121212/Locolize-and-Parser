using UnityEngine;
using System.IO;

public class LocalFileStreamingAssetsLoader : ILocalLoader
{
    private readonly string PATH = $"{Application.streamingAssetsPath}/{LocalFileLoaderConfigSettings.LOCAL_FILE_NAME}.{LocalFileLoaderConfigSettings.LOCAL_FILE_EXTENSION}";

    public string LoadLocal()
    {
        return File.ReadAllText(PATH);
    }

    public void WriteLocal(string text)
    {
        File.WriteAllText(PATH, text);
    }
}