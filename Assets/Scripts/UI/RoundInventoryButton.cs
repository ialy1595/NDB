using UnityEngine;
using System.Collections;

public class RoundInventoryButton : MonoBehaviour {

    private Inventory inv;

    void Start()
    {
        inv = GameManager.gm.GetComponentInChildren<Inventory>();
    }
    public void OnClick()
    {
        inv.showInventory = !inv.showInventory;
    }
}
