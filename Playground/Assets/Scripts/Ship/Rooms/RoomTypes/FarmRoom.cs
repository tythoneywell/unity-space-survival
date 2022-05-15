using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Produces crops 
 */
public class FarmRoom : RoomBackend
{
    ProcessingInventory farmInv;

    const float powerDraw = 1f;

    public override void Build()
    {
        farmInv = new ProcessingInventory(0, 4, 1);
    }
    public override void Update()
    {
        float powerSatisfaction = ShipSystemController.main.powerSatisfaction;
        if (farmInv.recipe != null && farmInv.ProcessTime(Time.deltaTime * powerSatisfaction, ShipController.main.currZone == 1 ? 0.5f : 1))
        {
            wrapper.working = true;
            wrapper.powerConsumption = powerDraw;
        }
        else
        {
            wrapper.working = false;
            wrapper.powerConsumption = 0;
        }
    }
    public override void Deconstruct()
    {
        wrapper.powerConsumption = 0;
    }
    public override void Interact(PlayerInteraction presser)
    {
        ProcessingInventory.curr = farmInv;
        PlayerUIController.main.OpenFarmMenu();
    }
}
