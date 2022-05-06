using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : RepairableObject
{
    public override void OnInteract(PlayerInteraction presser)
    {
        if (operational)
        {
            Door doorPlate = gameObject.GetComponentInChildren<Door>();
            doorPlate.Open();
        }
        else
        {
            base.OnInteract(presser);
        }
    }
    public override void Repair()
    {
        base.Repair();
        Door doorPlate = gameObject.GetComponentInChildren<Door>();
        doorPlate.Open();
    }
    
}
