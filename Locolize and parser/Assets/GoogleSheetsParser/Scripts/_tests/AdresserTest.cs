using UnityEngine;

public class AdresserTest : MonoBehaviour
{
    private void Start()
    {
        SheetClassicIndexator indexator = new SheetClassicIndexator();

        Debug.Log($"Address: A1; Pos: {indexator.AddresToVector("A1")}");
    }
}