using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Produces crops 
 */
public class FarmRoom : RoomBackend
{
    ProcessingInventory farmInv;

    const float itemProductionDelay = 10f;
    const float fuelConsumptionDelay = 15f;
    const float powerDraw = 1f;

    float itemProductionProgress = 0;
    float fuelConsumptionProgress = fuelConsumptionDelay;

    public override void Build()
    {
        farmInv = new ProcessingInventory(0, 4, 1);
    }
    public override void Update()
    {
        float powerSatisfaction = ShipSystemController.main.powerSatisfaction;
        if (farmInv.recipe != null && farmInv.fuel.GetItem(0).item.itemName == farmInv.recipe.fuel.item.itemName)
        {
            wrapper.working = true;
            wrapper.powerConsumption = powerDraw;

            itemProductionProgress += Time.deltaTime * powerSatisfaction;
            fuelConsumptionProgress += Time.deltaTime * powerSatisfaction;
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
        else
        {
            wrapper.working = false;
            wrapper.powerConsumption = 0;
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
