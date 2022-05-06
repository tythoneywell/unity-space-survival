using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class RepairableObject : InteractableObject
{
    public static RepairableObject curr;

    public bool operational;
    public Recipe repairCost;

    public override void OnInteract(PlayerInteraction presser)
    {
        curr = this;
        PlayerUIController.main.OpenRepairMenu();
    }
    public virtual void Repair()
    {
        operational = true;
    }
}
