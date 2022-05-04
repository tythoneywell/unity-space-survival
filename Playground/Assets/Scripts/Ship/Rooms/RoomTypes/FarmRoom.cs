using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Produces crops 
 */
public class FarmRoom : RoomBackend
{
    ProcessingInventory farmInv;

    const float itemProductionDelay = 2f;
    const float fuelConsumptionDelay = 4f;

    float itemProductionProgress = 0;
    float fuelConsumptionProgress = fuelConsumptionDelay;

    public override void Build()
    {
        farmInv = new ProcessingInventory(0, 4, 1);
    }
    public override void Update()
    {
        if (farmInv.recipe != null && farmInv.fuel.GetItem(0).item.itemName == farmInv.recipe.fuel.item.itemName)
        {
            itemProductionProgress += Time.deltaTime;
            fuelConsumptionProgress += Time.deltaTime;
            if (itemProductionProgress > itemProductionDelay)
            {
                farmInv.AddItemNoIndex(new ItemStack(farmInv.recipe.result));
                itemProductionProgress -= itemProductionDelay;
            }
            if (fuelConsumptionProgress > fuelConsumptionDelay)
            {
                farmInv.fuel.RemoveItem(0, 1);
                fuelConsumptionProgress -= fuelConsumptionDelay;
            }
        }
    }
    public override void Deconstruct()
    {

    }
    public override void Interact(PlayerInteraction presser)
    {
        ProcessingInventory.curr = farmInv;
        PlayerUIController.main.OpenFarmMenu();
    }
}
