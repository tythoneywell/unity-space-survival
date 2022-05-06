using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorFixer : MonoBehaviour
{
    const bool debug = true;

    public void TryFixDoor()
    {
        if (PlayerInventory.main.HasRecipeIngredients(RepairableObject.curr.repairCost) || debug)
        {
            PlayerInventory.main.ConsumeRecipeIngredients(RepairableObject.curr.repairCost);
            RepairableObject.curr.Repair();
            PlayerUIController.main.UpdateInventory();
            PlayerUIController.main.HideInventory();
        }
    }
}
