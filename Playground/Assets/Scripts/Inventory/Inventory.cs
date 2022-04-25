using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    public static Inventory main;

    ItemStack[] hotbar;
    ItemStack[] inventory;
    public ItemStack cursorStack;

    public ItemStack[] externalInventory;

    public enum InventoryType
    {
        Hotbar,
        Inventory,
        ExternalInventory,
        CraftingStation,
        LootableCorpse,
    }

    public Inventory()
    {
        hotbar = new ItemStack[9];
        inventory = new ItemStack[27];
        for (int i = 0; i < 27; i++){
            if (i < 9) {
                hotbar[i] = ItemStack.GetEmpty();
            }
            inventory[i] = ItemStack.GetEmpty();
        }
        cursorStack = ItemStack.GetEmpty();
    }

    // Adds item to any free index, if possible
    public bool AddItemNoIndex(ItemStack stack){
        //Attempt to add to existing hotbar stack
        for(int i = 0; i < 9; i++){
            ItemStack hotbarSlot = hotbar[i];
            if (hotbarSlot.item.itemName == stack.item.itemName)
            {
                bool result = hotbarSlot.incrementStack(stack.count);
                hotbar[i] = hotbarSlot;
                return result;
            }
        }
        //Attempt to add to existing inventory stack
        for(int i = 0; i < 27; i++) {
            ItemStack invSlot = inventory[i];
            if (invSlot.item.itemName == stack.item.itemName)
            {
                bool result = invSlot.incrementStack(stack.count);
                inventory[i] = invSlot;
                return result;
            }
        }
        //Attempt to add to empty hotbar stack
        for(int i = 0; i < 9; i++){
            ItemStack hotbarSlot = hotbar[i];
            if (hotbarSlot.item.itemName == null)
            {
                hotbar[i] = stack;
                return true;
            }
        }
        //Attempt to add to empty inventory stack
        for(int i = 0; i < 27; i++){
            ItemStack invSlot = inventory[i];
            if (invSlot.item.itemName == null)
            {
                inventory[i] = stack;
                return true;
            }
        }
        return false;
    }

    // Tries to add item stack to specified index, returns stack with amount unable to be added
    public ItemStack AddItem(int index, ItemStack stack, int amount = int.MaxValue)
    {
        if (amount == -1) amount = inventory[index].count / 2;
        if (amount == 0) amount = 1;

        if (index < 0 || index >= 36)
        {
            return stack;
        }
        if (index < 9)
        {
            hotbar[index].TransferAmountFromStack(stack, amount);
            return hotbar[index];
        }
        else
        {
            inventory[index - 9].TransferAmountFromStack(stack, amount);
            return inventory[index - 9];
        }
    }
    public ItemStack GetItem(int index){
        if (index < 0 || index >= 36) {
            return ItemStack.GetEmpty();
        }
        if (index < 9) {
            return hotbar[index];
        } else {
            return inventory[index - 9];
        }
    }
    // Removes and returns an item stack of desired size "amount" or as much as is available,
    // from inventory index "index".
    public ItemStack RemoveItem(int index, int amount = int.MaxValue)
    {
        if (amount == -1) amount = inventory[index].count / 2;
        if (amount == 0) amount = 1;

        if (index < 0 || index >= 36)
        {
            return ItemStack.GetEmpty();
        }
        if (index < 9)
        {
            return hotbar[index].TakeAmount(amount);
        }
        else
        {
            return inventory[index - 9].TakeAmount(amount);
        }
    }

    public void CursorInteractWith(int index)
    {
        if (cursorStack.item.itemName == null) MoveItemToCursor(index);
        else if (cursorStack.item.itemName != null) DropCursorItemToIndexHard(index);
    }
    public void CursorAlternateInteractWith(int index)
    {
        if (cursorStack.item.itemName == null) MoveItemToCursor(index, -1);
        else if (cursorStack.item.itemName != null) DropCursorItemToIndexSoft(index);
    }

    // Moves item at index "index" to the inventory cursor
    ItemStack MoveItemToCursor(int index, int amount = int.MaxValue)
    {
        if (amount == -1) amount = GetItem(index).count / 2;
        if (amount == 0) amount = 1;

        cursorStack = RemoveItem(index, amount);

        return cursorStack;
    }
    // Moves cursor item to index "index", swapping if items are different,
    // subtracting from its existing stack if item limit is reached on the inventory stack.
    ItemStack DropCursorItemToIndexHard(int index)
    {
        if (GetItem(index).item.itemName != cursorStack.item.itemName)
        {
            cursorStack.SwapWith(GetItem(index));
        }

        AddItem(index, cursorStack);

        return cursorStack;
    }
    // Moves cursor item to index "index", but only specified amount,
    // and will not swap stacks
    // Set amount to -1 to drop half of stack
    ItemStack DropCursorItemToIndexSoft(int index, int amount = 1)
    {
        if (amount == -1) amount = cursorStack.count / 2;
        if (amount == 0) amount = 1;

        AddItem(index, cursorStack, amount);

        return cursorStack;
    }

    public ItemStack[] ToArray()
    {
        ItemStack[] inv = new ItemStack[36];
        int counter = 0;
        foreach (ItemStack stack in hotbar)
        {
            inv[counter] = stack;
            counter += 1;
        }
        foreach (ItemStack stack in inventory)
        {
            inv[counter] = stack;
            counter += 1;
        }
        return inv;
    }
}
