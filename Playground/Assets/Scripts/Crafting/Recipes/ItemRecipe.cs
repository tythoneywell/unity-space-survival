using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemRecipe", menuName = "ScriptableObjects/ItemRecipe", order = 1)]
public abstract class ItemRecipe : Recipe
{
    public ItemStack result;
}
