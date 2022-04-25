using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RoomRecipeRegistry", menuName = "ScriptableObjects/Single/RoomRecipeRegistry", order = 1)]
public class RoomRecipeRegistry : ScriptableObject
{
    public RoomRecipe[] recipeList;

    public Sprite[] GetProducts()
    {
        List<Sprite> products = new List<Sprite>();
        foreach (RoomRecipe recipe in recipeList)
        {
            products.Add(recipe.icon);
        }

        return products.ToArray();
    }
}
