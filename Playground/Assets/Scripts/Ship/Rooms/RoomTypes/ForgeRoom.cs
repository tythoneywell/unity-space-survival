using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Produces crops 
 */
public class ForgeRoom : RoomBackend
{
    ProcessingInventory forgeInv;

    const float powerDraw = 2f;

    public override void Build()
    {
        forgeInv = new ProcessingInventory(4, 4, 0);
    }
    public override void Update()
    {
        float powerSatisfaction = ShipSystemController.main.powerSatisfaction;
        if (forgeInv.recipe != null && forgeInv.ProcessTime(Time.deltaTime * powerSatisfaction))
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

    }
    public override void Interact(PlayerInteraction presser)
    {
        ProcessingInventory.curr = forgeInv;
        PlayerUIController.main.OpenForgeMenu();
    }
}
