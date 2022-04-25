using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : UISlot
{
    public int inventoryIndexOffset;

    public override void PrimaryActivate()
    {
        Inventory.main.CursorInteractWith(index + inventoryIndexOffset);
    }
    public override void SecondaryActivate()
    {
        Inventory.main.CursorAlternateInteractWith(index + inventoryIndexOffset);
    }
    public override void TertiaryActivate()
    {
        ItemStack toDuplicate = Inventory.main.GetItem(index + inventoryIndexOffset);
        PlayerInteraction.main.inventory.cursorStack = new ItemStack(toDuplicate);
    }
}
