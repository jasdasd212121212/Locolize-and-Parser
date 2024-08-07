using System;
using Cysharp.Threading.Tasks;
using UnityEngine.Networking;

public class GoogleSheetsWebClient
{
    private string _sheetId;

    private const string URL = "https://docs.google.com/spreadsheets/d/{0}/export?format=csv&gid={1}";

    public GoogleSheetsWebClient(string sheetId)
    {
        _sheetId = sheetId;
    }

    public async UniTask GetGoogleSheetContent(Action<string> doneCallback, string sheetId, int listId) 
    {
        UnityWebRequest request = UnityWebRequest.Get(string.Format(URL, sheetId, listId));

        await request.SendWebRequest();

        doneCallback?.Invoke(request.downloadHandler.text);
    }

    public async UniTask GetGoogleSheetContent(Action<string> doneCallback, int listId)
    {
        await GetGoogleSheetContent(doneCallback, _sheetId, listId);
    }

    public async UniTask<string> GetGoogleSheetContent(int listId)
    {
        string text = "";

        await GetGoogleSheetContent((string responseContent) => { text = responseContent; }, listId);

        return text;
    }

    public async UniTask<string> GetGoogleSheetContent(string sheetId, int listId)
    {
        string text = "";

        await GetGoogleSheetContent((string responseContent) => { text = responseContent; }, sheetId, listId);

        return text;
    }
}