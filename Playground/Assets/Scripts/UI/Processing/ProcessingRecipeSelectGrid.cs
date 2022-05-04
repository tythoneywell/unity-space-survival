using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcessingRecipeSelectGrid : RecipeSelectGrid
{
    public override void ActivateRecipe(int index)
    {
        ProcessingInventory.curr.recipe = (ProcessingRecipe)knownRecipes.recipeList[index];
        PlayerUIController.main.HideInventory();
    }
}
