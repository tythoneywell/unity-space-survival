using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerInventorySlot : UISlot
{
    public int inventoryIndexOffset;

    public override void PrimaryActivate()
    {
        PlayerInventory.main.CursorInteractWith(index + inventoryIndexOffset);
    }
    public override void SecondaryActivate()
    {
        PlayerInventory.main.CursorAlternateInteractWith(index + inventoryIndexOffset);
    }
    public override void TertiaryActivate()
    {
        ItemStack toDuplicate = PlayerInventory.main.GetItem(index + inventoryIndexOffset);
        PlayerInteraction.main.inventory.cursorStack = new ItemStack(toDuplicate);
    }
}
