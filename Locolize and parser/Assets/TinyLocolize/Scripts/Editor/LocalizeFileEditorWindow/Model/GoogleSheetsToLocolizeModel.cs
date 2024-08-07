using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;

public class GoogleSheetsToLocolizeModel
{
    public async UniTask ImportGoogleSheet(string sheetId, int locolizeSheetId, ILocalEditorPresenter presenter, Action sheetImported)
    {
        await GoogleSheetsFacade.Initialize(sheetId, locolizeSheetId);
        await GoogleSheetsFacade.ChangeSheet(sheetId, locolizeSheetId);

        List<LocolizeNode> locolizeNodes = new List<LocolizeNode>();

        locolizeNodes.AddRange(presenter.Deserialize());

        int languagesCount = presenter.DeserializeData().LanguagesCount;

        for (int i = 1; i <= GoogleSheetsFacade.FindLowerLine(); i++)
        {
            string key = GoogleSheetsFacade.GetContentOfCell(new Vector2Int(0, i));
            string[] locolizes = new string[languagesCount];

            for (int j = 0; j < languagesCount; j++)
            {
                locolizes[j] = GoogleSheetsFacade.GetContentOfCell(new Vector2Int(j + 1, i));
            }

            locolizeNodes.Add(new LocolizeNode(key, locolizes));
        }

        presenter.Serialize(locolizeNodes.ToArray());

        sheetImported?.Invoke();
    }
}