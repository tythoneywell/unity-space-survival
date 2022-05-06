using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputItemGrid : UISlotGrid
{
    public bool isFuel;
    public bool isIngredient;

    protected override GameObject MakeSlot()
    {
        GameObject newSlot = Instantiate(PlayerUIController.emptySprite, rTransform);
        ProcessingInventorySlot invSlot = newSlot.AddComponent<ProcessingInventorySlot>();
        invSlot.isFuel = isFuel;
        invSlot.isInput = isIngredient;

        return newSlot;
    }
}
