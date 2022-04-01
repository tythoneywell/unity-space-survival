using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlasterItem : InteractableObject
{
    // Start is called before the first frame update
    public ItemStack itemStack;
    public BlasterItem(ItemStack itemStack) {
        this.itemStack = itemStack;
    }
    public override void Interact(PlayerInteraction presser)
    {
        bool pickedUp = presser.AddToInventory(itemStack);
        if (pickedUp) {
            Debug.Log("Picked up " + itemStack.count + " " + itemStack.item.itemName);
            Destroy(gameObject);
        }
        else Debug.Log("" + itemStack.count + " " + itemStack.item.itemName + " did not fit in inventory");
    }
}
