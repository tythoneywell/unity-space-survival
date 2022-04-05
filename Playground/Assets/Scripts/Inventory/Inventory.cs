using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    ItemStack[] hotbar;
    ItemStack[] inventory;
    public ItemStack cursor;

    public ItemStack[] externalInventory;

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
        cursor = ItemStack.GetEmpty();
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
        if (index < 0 || index >= 36)
        {
            return stack;
        }
        if (index < 9)
        {
            ItemStack stackToFill = hotbar[index];
            if (stackToFill.item.itemName == null)
            {
                hotbar[index] = new ItemStack(stack.item, 0);
                hotbar[index].AddAmountFromStack(stack, amount);
                return stack;
            }
            if (stackToFill.item.itemName != stack.item.itemName)
            {
                return stack;
            }
            if (stackToFill.count + stack.count < stack.item.maxStackSize)
            {
                hotbar[index].count += stack.count;
                return ItemStack.GetEmpty();
            }
            else
            {
                hotbar[index].count += stack.item.maxStackSize;
                return new ItemStack(stackToFill.item, stack.count - (stack.item.maxStackSize - stackToFill.count));
            }
        }
        else
        {
            ItemStack stackToFill = inventory[index - 9];
            if (stackToFill.item.itemName == null)
            {
                inventory[index - 9] = new ItemStack(stack.item, 0);
                inventory[index - 9].AddAmountFromStack(stack, amount);
                return stack;
            }
            if (stackToFill.count + stack.count < stack.item.maxStackSize)
            {
                inventory[index - 9].count += stack.count;
                return ItemStack.GetEmpty();
            }
            else
            {
                inventory[index - 9].count += stack.item.maxStackSize;
                return new ItemStack(stackToFill.item, stack.count - (stack.item.maxStackSize - stackToFill.count));
            }
        }
    }
    public ItemStack[] ToArray(){
        ItemStack[] inv = new ItemStack[36];
        int counter = 0;
        foreach (ItemStack stack in hotbar){
            inv[counter] = stack;
            counter += 1;
        }
        foreach (ItemStack stack in inventory){
            inv[counter] = stack;
            counter += 1;
        }
        return inv;
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
        if (index < 0 || index >= 36)
        {
            return ItemStack.GetEmpty();
        }
        if (index < 9)
        {
            ItemStack stackToRemove = hotbar[index];
            ItemStack outStack = new ItemStack(stackToRemove.item, stackToRemove.count);
            if (stackToRemove.count <= amount)
            {
                hotbar[index] = ItemStack.GetEmpty();
                return outStack;
            }
            else
            {
                hotbar[index].count -= amount;
                outStack.count = amount;
                return outStack;
            }
        }
        else
        {
            ItemStack stackToRemove = inventory[index - 9];
            ItemStack outStack = new ItemStack(stackToRemove.item, stackToRemove.count);
            if (stackToRemove.count <= amount)
            {
                inventory[index - 9] = ItemStack.GetEmpty();
                return outStack;
            }
            else
            {
                inventory[index - 9].count -= amount;
                outStack.count = amount;
                return outStack;
            }
        }
    }

    public void CursorInteractWith(int index)
    {
        if (cursor.item.itemName == null) MoveItemToCursor(index);
        else if (cursor.item.itemName != null) DropCursorItemToIndexHard(index);
    }

    // Moves item at index "index" to the inventory cursor
    public ItemStack MoveItemToCursor(int index)
    {
        ItemStack targetItem = RemoveItem(index, int.MaxValue);
        cursor = targetItem;

        return cursor;
    }
    // Moves cursor item to index "index", swapping if items are different,
    // subtracting from its existing stack if item limit is reached on the inventory stack.
    public ItemStack DropCursorItemToIndexHard(int index)
    {
        if (cursor.item.name != GetItem(index).item.itemName)
        {
            ItemStack cursorPrevStack = cursor;
            cursor = RemoveItem(index, int.MaxValue);
            AddItem(index, cursorPrevStack);
        }
        else
        {
            cursor = AddItem(index, cursor);
        }

        return cursor;
    }
    // Moves cursor item to index "index", but only specified amount,
    // and will not swap stacks
    // Set amount to -1 to drop half of stack
    public ItemStack DropCursorItemToIndexSoft(int index, int amount = -1)
    {
        if (amount > cursor.count) amount = cursor.count;
        if (amount == -1) amount = cursor.count / 2;

        ItemStack stackToAdd = new ItemStack(cursor.item, amount);
        ItemStack remaining = AddItem(index, stackToAdd);

        cursor.count -= (amount - remaining.count);

        return cursor;
    }
}
