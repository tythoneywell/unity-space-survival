using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRecipeSelectGrid : RecipeSelectGrid
{
    public override void ActivateRecipe(int index)
    {
        if (index == -1) index = selectedRecipeIndex;
        CraftingStation.curr.Craft((ItemRecipe)knownRecipes.recipeList[index]);
    }
}
