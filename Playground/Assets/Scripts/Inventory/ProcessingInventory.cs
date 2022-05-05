using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcessingInventory : Inventory
{
    public static ProcessingInventory curr;

    public ProcessingRecipe recipe;
    public Inventory rawResources;
    public Inventory fuel;

    float itemProductionProgress = 0;
    float fuelConsumptionProgress = 0;
    float itemProductionDelay = 0;
    float fuelConsumptionDelay = 0;

    public ProcessingInventory(int inputSlots, int outputSlots, int fuelSlots) : base(outputSlots)
    {
        rawResources = new Inventory(inputSlots);
        fuel = new Inventory(fuelSlots);
    }

    public void SetRecipe(ProcessingRecipe recipe)
    {
        this.recipe = recipe;
        itemProductionProgress = 0;
        fuelConsumptionProgress = recipe.fuelConsumeTime;
        itemProductionDelay = recipe.productionTime;
        fuelConsumptionDelay = recipe.fuelConsumeTime;
    }
    // Processes the recipe for scaled time time, returning whether it cannot process
    public bool ProcessTime(float time)
    {
        if (fuel.HasRecipeIngredients(new ItemStack[] { recipe.fuel }))
        {
            itemProductionProgress += time;
            fuelConsumptionProgress += time;
            if (itemProductionProgress > itemProductionDelay)
            {
                AddItemNoIndex(new ItemStack(recipe.result));
                itemProductionProgress -= itemProductionDelay;
            }
            if (fuelConsumptionProgress > fuelConsumptionDelay)
            {
                fuel.ConsumeRecipeIngredients(new ItemStack[] { recipe.fuel });
                fuelConsumptionProgress -= fuelConsumptionDelay;
            }
            return true;
        }
        return false;
    }
}
