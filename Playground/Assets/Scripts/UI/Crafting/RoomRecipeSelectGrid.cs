using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomRecipeSelectGrid : RecipeSelectGrid
{
    public RoomWrapper targetRoom;

    public override void ActivateRecipe(int index)
    {
        targetRoom.Build((RoomRecipe)knownRecipes.recipeList[index]);
        PlayerUIController.main.HideInventory();
    }
}
