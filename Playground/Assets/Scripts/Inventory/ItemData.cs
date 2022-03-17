using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/ItemData", order = 1)]
public class ItemData: ScriptableObject
{
    public string itemName;
    public string displayName;
    public int maxStackSize;

    public GameObject model;
    public Sprite sprite;

    public void Use()
    {
        Instantiate(MakeDroppedInstance(1));
    }

    public GameObject MakeDroppedInstance(int count)
    {
        GameObject droppedInstance = Instantiate(model);
        DroppedItem drop = droppedInstance.GetComponent<DroppedItem>();
        if (drop == null) drop = droppedInstance.AddComponent<DroppedItem>();
        drop.itemStack = new ItemStack(this, count);

        return droppedInstance;
    }
}
