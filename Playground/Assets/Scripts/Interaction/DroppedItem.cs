using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedItem : InteractableObject
{
    public ItemStack itemStack;
    public DroppedItem(ItemStack itemStack) {
        this.itemStack = new ItemStack(itemStack);
    }

    public override void OnInteract(PlayerInteraction presser) {
        bool pickedUp = presser.AddToInventory(itemStack);
        if (pickedUp) {
            Debug.Log("Picked up " + itemStack.count + " " + itemStack.item.itemName);
            Destroy(gameObject);
        }
        else Debug.Log("" + itemStack.count + " " + itemStack.item.itemName + " did not fit in inventory");
    }
    public override string GetInteractPrompt()
    {
        return "[RMB] to pick up " + itemStack.count + " " + itemStack.item.displayName;
    }
}
