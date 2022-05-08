using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeRecipeSelectGrid : RecipeSelectGrid
{
    public override void ActivateRecipe(int index)
    {
        if (index == -1) index = selectedRecipeIndex;
        ShipSystem.curr.Upgrade((UpgradeRecipe)knownRecipes.recipeList[index]);
        PlayerUIController.main.HideInventory();
    }

    private void Update()
    {
        ShowRecipeProducts(0);
    }

    public override void ShowRecipeProducts(int page)
    {
        knownRecipes = UpgradeTerminal.activeUpgrades;
        Recipe[] products = knownRecipes.recipeList;

        for (int i = 0; i < gridSlots.Length; i++)
        {
            if (i < products.Length) gridSlots[i].ShowSpriteWithTooltip(products[i].icon, products[i].recipeName, products[i].recipeDescription);
            else gridSlots[i].ShowSprite(null);
        }
    }
}
