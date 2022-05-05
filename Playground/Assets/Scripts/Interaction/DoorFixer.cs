using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorFixer : MonoBehaviour
{
    public void TryFixDoor()
    {
        if (PlayerInventory.main.HasRecipeIngredients(DoorController.currentDoor.repairCost))
        {
            PlayerInventory.main.ConsumeRecipeIngredients(DoorController.currentDoor.repairCost);
            DoorController.currentDoor.FixDoor();
            PlayerUIController.main.UpdateInventory();
            PlayerUIController.main.HideInventory();
        }
    }
}
