using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlotGrid : UISlotGrid
{
    public Inventory.InventoryType invType;

    protected override GameObject MakeSlot()
    {
        GameObject newSlot = Instantiate(PlayerUIController.emptySprite, rTransform);
        newSlot.AddComponent<InventorySlot>();

        newSlot.GetComponent<InventorySlot>().inventoryIndexOffset = invType == Inventory.InventoryType.Inventory ? 9 : 0;

        return newSlot;
    }
}
