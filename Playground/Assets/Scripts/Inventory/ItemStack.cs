using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemStack
{
    // Both of these *might* become set-private sometime to enforce using ItemStack methods instead of direct assignment
    // (but this makes ItemStacks un-editable in inpector)
    public ItemData item;
    public int count;

    static ItemData nullItem = ScriptableObject.CreateInstance<ItemData>();

    public ItemStack(ItemData item, int count){
        this.item = item;
        this.count = count;
    }
    public ItemStack(ItemStack stack)
    {
        this.item = stack.item;
        this.count = stack.count;
    }

    public bool incrementStack(int num){
        if (this.count + num >= item.maxStackSize || this.count + num < 0){
            return false;
        } else {
            this.count += num;
            return true;
        }
    }
    public bool setStackSize(int num){
     if (num >= item.maxStackSize || num < 0){
            return false;
        } else {
            this.count = num;
            return true;
        }
    }

    // Transfers amount of item "amount" from stack "other", if the same item type.
    // other's item will be set to nullItem if it is empty.
    public void TransferAmountFromStack(ItemStack other, int amount = int.MaxValue)
    {
        if (item.itemName == null)
        {
            item = other.item;
            count = other.count;
            other.count = 0;
        }
        else if (other.item.itemName != item.itemName)
        {
            return;
        }
        else
        {
            if (amount > other.count) amount = other.count;
            int addable = Mathf.Min(amount, item.maxStackSize - count);
            count += addable;
            other.count -= addable;
        }

        if (other.count <= 0) other.item = nullItem;
    }
    // Creates a new ItemStack containing amount "amount" of this item, or as much as is available.
    // If this empties the stack, its item is set to nullItem.
    public ItemStack TakeAmount(int amount)
    {
        if (amount > count) amount = count;
        ItemStack outStack = new ItemStack(item, amount);
        count -= amount;

        if (count <= 0) item = nullItem;

        return outStack;
    }
    // Swaps this stack with stack other
    public void SwapWith(ItemStack other)
    {
        ItemStack thisCopy = new ItemStack(this);

        item = other.item;
        count = other.count;

        other.item = thisCopy.item;
        other.count = thisCopy.count;
    }

    // Returns the empty stack, with item nullItem and count 0.
    // To check for empty stack, use item.itemName == null.
    public static ItemStack GetEmpty()
    {
        return new ItemStack(nullItem, 0);
    }
}
