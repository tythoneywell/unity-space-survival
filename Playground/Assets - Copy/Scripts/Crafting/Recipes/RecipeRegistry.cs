using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RecipeRegistry", menuName = "ScriptableObjects/Single/RecipeRegistry", order = 1)]
public class RecipeRegistry : ScriptableObject
{
    public Recipe[] recipeList;

    public Sprite[] GetProducts()
    {
        List<Sprite> products = new List<Sprite>();
        foreach (Recipe recipe in recipeList)
        {
            products.Add(recipe.icon);
        }

        return products.ToArray();
    }
}
