using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ProcessingRecipe", menuName = "ScriptableObjects/ProcessingRecipe", order = 1)]
public class ProcessingRecipe : Recipe
{
    public ItemStack result;
    public ItemStack fuel;
}
