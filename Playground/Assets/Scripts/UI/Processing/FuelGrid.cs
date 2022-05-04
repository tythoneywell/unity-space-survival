using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelGrid : UISlotGrid
{

    protected override GameObject MakeSlot()
    {
        GameObject newSlot = Instantiate(PlayerUIController.emptySprite, rTransform);
        ProcessingInventorySlot invSlot = newSlot.AddComponent<ProcessingInventorySlot>();
        invSlot.isFuel = true;

        return newSlot;
    }
}
