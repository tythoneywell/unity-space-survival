using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomRecipeSelectGrid : RecipeSelectGrid
{
    public override void ActivateRecipe(int index)
    {
        if (index == -1) index = selectedRecipeIndex;
        RoomWrapper.curr.Build((RoomRecipe)knownRecipes.recipeList[index]);
        PlayerUIController.main.HideInventory();
    }
}
