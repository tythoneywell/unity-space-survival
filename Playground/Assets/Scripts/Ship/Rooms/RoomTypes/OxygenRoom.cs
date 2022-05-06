using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Produces power 
 */
public class OxygenRoom : RoomBackend
{
    ProcessingInventory oxyInv;

    const float powerDraw = 2f;

    public override void Build()
    {
        oxyInv = new ProcessingInventory(0, 0, 1);
        oxyInv.SetRecipe(wrapper.defaultOxyRecipe);
    }
    public override void Update()
    {
        float powerSatisfaction = ShipSystemController.main.powerSatisfaction;
        if (oxyInv.recipe != null && oxyInv.ProcessTime(Time.deltaTime * powerSatisfaction))
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
        ProcessingInventory.curr = oxyInv;
        PlayerUIController.main.OpenGeneratorMenu();
    }
    public void InitAsStartRoom()
    {
        oxyInv.fuel.AddItemNoIndex(new ItemStack(wrapper.defaultOxyRecipe.fuel.item, 20));
    }
}
