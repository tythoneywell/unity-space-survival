using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    ItemStack[] hotbar = new ItemStack[9];
    ItemStack[] inventory = new ItemStack[27];
   public Inventory(){
        for (int i = 0; i < 27; i++){
            if (i < 9) {
                hotbar[i] = ItemStack.empty();
            }
            inventory[i] = ItemStack.empty();
        }
   }
    // Start is called before the first frame updates
    public bool addItem(ItemStack stack){
        //Attempt to add to existing hotbar stack
        for(int i = 0; i < 9; i++){
            ItemStack hotbarSlot = hotbar[i];
            if (hotbarSlot.regName.Equals(stack.regName)){
                bool result = hotbarSlot.incrementStack(stack.count);
                hotbar[i] = hotbarSlot;
                return result;
            }
        }
        //Attempt to add to existing inventory stack
        for(int i = 0; i < 27; i++) {
            ItemStack invSlot = inventory[i];
            if (invSlot.regName.Equals(stack.regName)){
                bool result = invSlot.incrementStack(stack.count);
                inventory[i] = invSlot;
                return result;
            }
        }
        //Attempt to add to empty hotbar stack
        for(int i = 0; i < 9; i++){
            ItemStack hotbarSlot = hotbar[i];
            if (hotbarSlot.regName.Equals("empty")){
                hotbar[i] = stack;
                return true;
            }
        }
        //Attempt to add to empty inventory stack
        for(int i = 0; i < 27; i++){
            ItemStack invSlot = inventory[i];
            if (invSlot.regName.Equals("empty")){
                inventory[i] = stack;
                return true;
            }
        }
        return false;
    }
    public ItemStack getItem(int index){
        if (index < 0 || index >= 36){
            return ItemStack.empty();
        }
        if (index < 9){
            return hotbar[index];
        } else {
            return inventory[index - 9];
        }
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
