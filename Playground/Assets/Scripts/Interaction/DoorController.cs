using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : InteractableObject
{
    public bool operational;
    public Recipe repairCost;

    public override void OnInteract(PlayerInteraction presser){
        if (operational)
        {
            Door doorPlate = gameObject.GetComponentInChildren<Door>();
            doorPlate.Open();
        }
        else
        {
            // Open repair menu
        }
    }
    
}
