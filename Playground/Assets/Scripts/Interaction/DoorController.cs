using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : InteractableObject
{
    public static DoorController currentDoor;

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
            currentDoor = this;
            PlayerUIController.main.OpenRepairMenu();
        }
    }
    public void FixDoor()
    {
        operational = true;
        Door doorPlate = gameObject.GetComponentInChildren<Door>();
        doorPlate.Open();
    }
    
}
