using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemStack
{
    public ItemData item;
    public int count;

    static ItemData nullItem = ScriptableObject.CreateInstance<ItemData>();

    public ItemStack(ItemData item, int count){
        this.item = item;
        this.count = count;
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

    public static ItemStack GetEmpty()
    {
        return new ItemStack(nullItem, 0);
    }
}
