using UnityEngine;

public class GetCellTest : MonoBehaviour
{
    [SerializeField] private string _addres;
    [SerializeField] private Vector2Int _vectorAddress;

    [ContextMenu("ByAddres")]
    private void GetByAddress()
    {
        Debug.Log(GoogleSheetsFacade.GetContentOfCell(_addres));
    }

    [ContextMenu("ByVector")]
    private void GetByVector()
    {
        Debug.Log(GoogleSheetsFacade.GetContentOfCell(_vectorAddress));
    }
}