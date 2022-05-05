using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : Inventory
{
    public static PlayerInventory main;

    public ItemStack cursorStack;

    const int playerInvSize = 36;

    public PlayerInventory() : base(playerInvSize)
    {
        cursorStack = ItemStack.GetEmpty();
    }

    public void CursorInteractWith(int index, Inventory otherInv = null)
    {
        if (otherInv == null) otherInv = this;
        if (cursorStack.item.itemName == null) MoveItemToCursor(index, otherInv);
        else if (cursorStack.item.itemName != null) DropCursorItemToIndexHard(index, otherInv);
    }
    public void CursorAlternateInteractWith(int index, Inventory otherInv = null)
    {
        if (otherInv == null) otherInv = this;
        if (cursorStack.item.itemName == null) MoveItemToCursor(index, otherInv, -1);
        else if (cursorStack.item.itemName != null) DropCursorItemToIndexSoft(index, otherInv);
    }

    // Moves item at index "index" to the inventory cursor
    ItemStack MoveItemToCursor(int index, Inventory otherInv = null, int amount = int.MaxValue)
    {
        if (otherInv == null) otherInv = this;
        if (amount == -1) amount = otherInv.GetItem(index).count / 2;
        if (amount == 0) amount = 1;

        cursorStack = otherInv.RemoveItem(index, amount);

        return cursorStack;
    }
    // Moves cursor item to index "index", swapping if items are different,
    // subtracting from its existing stack if item limit is reached on the inventory stack.
    ItemStack DropCursorItemToIndexHard(int index, Inventory otherInv = null)
    {
        if (otherInv == null) otherInv = this;
        if (otherInv.GetItem(index).item.itemName != cursorStack.item.itemName)
        {
            cursorStack.SwapWith(otherInv.GetItem(index));
        }

        otherInv.AddItem(index, cursorStack);

        return cursorStack;
    }
    // Moves cursor item to index "index", but only specified amount,
    // and will not swap stacks
    // Set amount to -1 to drop half of stack
    ItemStack DropCursorItemToIndexSoft(int index, Inventory otherInv = null, int amount = 1)
    {
        if (otherInv == null) otherInv = this;
        if (amount == -1) amount = cursorStack.count / 2;
        if (amount == 0) amount = 1;

        otherInv.AddItem(index, cursorStack, amount);

        return cursorStack;
    }
}
