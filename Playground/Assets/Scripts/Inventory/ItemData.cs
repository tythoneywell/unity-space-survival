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

    public int hungerRestoreAmount;

    public virtual void Use(PlayerInteraction presser)
    {
        if (hungerRestoreAmount > 0)
        {
            presser.RemoveCurrentFromInv(1);
            Debug.Log("ate " + displayName);
        }
    }

    public GameObject MakeDroppedInstance(int count, PlayerInteraction presser)
    {
        GameObject droppedInstance = Instantiate(model);
        DroppedItem drop = droppedInstance.GetComponent<DroppedItem>();
        if (drop == null) drop = droppedInstance.AddComponent<DroppedItem>();
        drop.itemStack = new ItemStack(this, count);
        if (presser != null){
            Vector3 dir = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.75f, 0f)).direction;
            Vector3 pos = presser.transform.position + dir*1;
            droppedInstance.transform.position = pos;
            Rigidbody rb = drop.GetComponent<Rigidbody>();
            rb.velocity = dir * 10;
            presser.RemoveCurrentFromInv(count);
        }

        return droppedInstance;
    }
}
