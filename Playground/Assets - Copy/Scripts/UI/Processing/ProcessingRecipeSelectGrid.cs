using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcessingRecipeSelectGrid : RecipeSelectGrid
{
    public override void ActivateRecipe(int index)
    {
        if (index == -1) index = selectedRecipeIndex;
        ProcessingInventory.curr.SetRecipe((ProcessingRecipe)knownRecipes.recipeList[index]);
        PlayerUIController.main.HideInventory();
    }
}
