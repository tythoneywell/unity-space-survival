using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemStack
{
    Item item;
    public int count;
    int maxCount;
    public string regName;
    public ItemStack(Item item, int count){
        this.item = item;
        this.count = count;
        this.maxCount = item.maxStackSize;
        this.regName = item.regName;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public bool incrementStack(int num){
        if (this.count + num >= maxCount || this.count + num < 0){
            return false;
        } else {
            this.count += num;
            return true;
        }
    }
    public bool setStackSize(int num){
     if (num >= maxCount || num < 0){
            return false;
        } else {
            this.count = num;
            return true;
        }
    }
    public static ItemStack empty(){
        Item empty = Item.empty();
        ItemStack curr_stack = new ItemStack(empty, 0);
        curr_stack.regName = "empty";
        return curr_stack;
    }
}
