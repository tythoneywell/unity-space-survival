using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutputItemGrid : UISlotGrid
{

    protected override GameObject MakeSlot()
    {
        GameObject newSlot = Instantiate(PlayerUIController.emptySprite, rTransform);
        ProcessingInventorySlot invSlot = newSlot.AddComponent<ProcessingInventorySlot>();
        invSlot.isFuel = false;

        return newSlot;
    }
}
