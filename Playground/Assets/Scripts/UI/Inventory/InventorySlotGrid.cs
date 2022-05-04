using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlotGrid : UISlotGrid
{
    public PlayerInventory.InventoryType invType;

    protected override GameObject MakeSlot()
    {
        GameObject newSlot = Instantiate(PlayerUIController.emptySprite, rTransform);
        newSlot.AddComponent<PlayerInventorySlot>();

        newSlot.GetComponent<PlayerInventorySlot>().inventoryIndexOffset = invType == PlayerInventory.InventoryType.Inventory ? 9 : 0;

        return newSlot;
    }
}
