using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    public ItemStack[] inventory;

    protected int invSize;

    public enum InventoryType
    {
        Hotbar,
        Inventory,
        ExternalInventory,
        CraftingStation,
        LootableCorpse,
    }

    public Inventory(int invSize)
    {
        this.invSize = invSize;
        inventory = new ItemStack[invSize];
        for (int i = 0; i < invSize; i++){
            inventory[i] = ItemStack.GetEmpty();
        }
    }

    // Adds item to any free index, if possible
    public bool AddItemNoIndex(ItemStack stack){
        //Attempt to add to existing inventory stack
        for(int i = 0; i < invSize; i++) {
            ItemStack invSlot = inventory[i];
            if (invSlot.item.itemName == stack.item.itemName)
            {
                invSlot.TransferAmountFromStack(stack);
            }
            if (stack.item.itemName == null) return true;
        }
        //Attempt to add to empty inventory stack
        for(int i = 0; i < invSize; i++)
        {
            ItemStack invSlot = inventory[i];
            invSlot.TransferAmountFromStack(stack);
            if (invSlot.item.itemName == null)
            {
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

        if (index < 0 || index >= invSize + 1)
        {
            return stack;
        }
        else
        {
            inventory[index].TransferAmountFromStack(stack, amount);
            return inventory[index];
        }
    }
    public ItemStack GetItem(int index){
        if (index < 0 || index >= invSize + 1) {
            return ItemStack.GetEmpty();
        }
        else
        {
            return inventory[index];
        }
    }
    // Removes and returns an item stack of desired size "amount" or as much as is available,
    // from inventory index "index".
    public ItemStack RemoveItem(int index, int amount = int.MaxValue)
    {
        if (amount == -1) amount = inventory[index].count / 2;
        if (amount == 0) amount = 1;

        if (index < 0 || index >= invSize + 1)
        {
            return ItemStack.GetEmpty();
        }
        else
        {
            return inventory[index].TakeAmount(amount);
        }
    }

    public bool HasRecipeIngredients(Recipe recipe)
    {
        foreach (ItemStack recipeStack in recipe.ingredients)
        {
            int invAmt = 0;
            foreach (ItemStack invStack in inventory)
            {
                if (invStack.item.itemName == recipeStack.item.itemName) invAmt += invStack.count;
            }
            if (invAmt < recipeStack.count) return false;
        }
        return true;
    }
    public void ConsumeRecipeIngredients(Recipe recipe)
    {
        foreach (ItemStack recipeStack in recipe.ingredients)
        {
            int remaining = recipeStack.count;
            foreach (ItemStack invStack in inventory)
            {
                if (invStack.item.itemName == recipeStack.item.itemName) remaining -= invStack.TakeAmount(remaining).count;
            }
            if (remaining > 0) Debug.Log("recipe was crafted without sufficient ingredients");
        }
    }
    public bool HasRecipeIngredients(ItemStack[] ingredients)
    {
        foreach (ItemStack recipeStack in ingredients)
        {
            int invAmt = 0;
            foreach (ItemStack invStack in inventory)
            {
                if (invStack.item.itemName == recipeStack.item.itemName) invAmt += invStack.count;
            }
            if (invAmt < recipeStack.count) return false;
        }
        return true;
    }
    public void ConsumeRecipeIngredients(ItemStack[] ingredients)
    {
        foreach (ItemStack recipeStack in ingredients)
        {
            int remaining = recipeStack.count;
            foreach (ItemStack invStack in inventory)
            {
                if (invStack.item.itemName == recipeStack.item.itemName) remaining -= invStack.TakeAmount(remaining).count;
            }
            if (remaining > 0) Debug.Log("recipe was crafted without sufficient ingredients");
        }
    }

    public ItemStack[] ToArray()
    {
        ItemStack[] inv = new ItemStack[36];
        int counter = 0;
        foreach (ItemStack stack in inventory)
        {
            inv[counter] = stack;
            counter += 1;
        }
        return inv;
    }
}
