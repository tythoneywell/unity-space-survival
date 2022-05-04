using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcessingInventory : Inventory
{
    public static ProcessingInventory curr;

    public ProcessingRecipe recipe;
    public Inventory rawResources;
    public Inventory fuel;

    public ProcessingInventory(int inputSlots, int outputSlots, int fuelSlots) : base(outputSlots)
    {
        rawResources = new Inventory(inputSlots);
        fuel = new Inventory(fuelSlots);
    }

    public void DepleteFuel(int amt)
    {

    }
}
