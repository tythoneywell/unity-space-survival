using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ProcessingInventorySlot : UISlot
{
    public bool isFuel = false;
    public bool isInput = false;

    private void Start()
    {
        ShowStack(ItemStack.GetEmpty());
    }
    private void Update()
    {
        ShowStack(GetAttachedInventory().GetItem(index));
    }
    public override void PrimaryActivate()
    {
        PlayerInventory.main.CursorInteractWith(index, GetAttachedInventory());
    }
    public override void SecondaryActivate()
    {
        PlayerInventory.main.CursorAlternateInteractWith(index, GetAttachedInventory());
    }
    public override void TertiaryActivate()
    {

    }

    Inventory GetAttachedInventory()
    {
        if (isFuel) return ProcessingInventory.curr.fuel;
        else if (isInput) return ProcessingInventory.curr.rawResources;
        else return ProcessingInventory.curr;
    }
}
