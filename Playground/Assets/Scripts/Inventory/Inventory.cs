using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    ItemStack[] hotbar;
    ItemStack[] inventory;
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
    }

    public bool addItem(ItemStack stack){
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
}
