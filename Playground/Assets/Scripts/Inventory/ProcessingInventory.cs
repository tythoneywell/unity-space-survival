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

    bool fuelConsumed;
    bool resourcesConsumed;

    public ProcessingInventory(int inputSlots, int outputSlots, int fuelSlots) : base(outputSlots)
    {
        rawResources = new Inventory(inputSlots);
        fuel = new Inventory(fuelSlots);
    }

    public void SetRecipe(ProcessingRecipe recipe)
    {
        this.recipe = recipe;
        itemProductionProgress = 0;
        fuelConsumptionProgress = 0;
        itemProductionDelay = recipe.productionTime;
        fuelConsumptionDelay = recipe.fuelConsumeTime;
        fuelConsumed = false;
        resourcesConsumed = false;
    }
    // Processes the recipe for scaled time time, returning whether it cannot process
    public bool ProcessTime(float time)
    {
        if (!fuelConsumed)
        {
            if (fuel.inventory.Length == 0 || fuel.HasRecipeIngredients(new ItemStack[] { recipe.fuel }))
            {
                fuel.ConsumeRecipeIngredients(new ItemStack[] { recipe.fuel });
                fuelConsumed = true;
            }
        }
        if (!resourcesConsumed)
        {
            if (rawResources.inventory.Length == 0 || rawResources.HasRecipeIngredients(recipe.ingredients))
            {
                rawResources.ConsumeRecipeIngredients(recipe);
                resourcesConsumed = true;
            }
        }
        if (fuelConsumed && resourcesConsumed)
        {
            if (itemProductionProgress > itemProductionDelay)
            {
                AddItemNoIndex(new ItemStack(recipe.result));
                itemProductionProgress -= itemProductionDelay;
                resourcesConsumed = false;
            }
            if (fuelConsumptionProgress > fuelConsumptionDelay)
            {
                fuel.ConsumeRecipeIngredients(new ItemStack[] { recipe.fuel });
                fuelConsumptionProgress -= fuelConsumptionDelay;
                fuelConsumed = false;
            }
            itemProductionProgress += time;
            fuelConsumptionProgress += time;
            ShipSystemController.main.oxygenAmount -= recipe.oxygenConsumeRate * time;
            return true;
        }
        return false;
    }
}
