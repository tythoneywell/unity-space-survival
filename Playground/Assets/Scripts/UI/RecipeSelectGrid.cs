using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RecipeSelectGrid : UISlotGrid
{
    public RecipeRegistry knownRecipes;

    public GameObject activeRecipeCursor;
    public RecipeIngredientsGrid ingredientsGrid;

    new void Start()
    {
        base.Start();
        ShowRecipeProducts(0);
    }

    public void ShowRecipeProducts(int page)
    {
        Recipe[] products = knownRecipes.recipeList;

        for (int i = 0; i < gridSlots.Length; i++)
        {
            if (i < products.Length) gridSlots[i].ShowSpriteWithTooltip(products[i].icon, products[i].recipeName);
            else gridSlots[i].ShowSprite(null);
        }
    }
    public void SelectRecipe(int index)
    {
        ShowRecipeIngredients(index);
        activeRecipeCursor.transform.position = gridSlots[index].transform.position;
    }
    void ShowRecipeIngredients(int index)
    {
        if (ingredientsGrid == null) return;
        if (index >= knownRecipes.recipeList.Length)
        {
            ingredientsGrid.ShowIngredients(new ItemStack[0]);
        }
        else
        {
            ingredientsGrid.ShowIngredients(knownRecipes.recipeList[index].ingredients);
        }
    }
    public abstract void ActivateRecipe(int index);

    protected override GameObject MakeSlot()
    {
        GameObject newSlot = Instantiate(PlayerUIController.emptySprite, rTransform);
        newSlot.AddComponent<RecipeSelectSlot>();

        return newSlot;
    }
}
