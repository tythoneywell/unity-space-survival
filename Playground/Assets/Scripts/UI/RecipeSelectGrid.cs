using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RecipeSelectGrid : UISlotGrid
{
    public RecipeRegistry knownRecipes;

    public GameObject activeRecipeCursor;
    public RecipeIngredientsGrid ingredientsGrid;

    protected int selectedRecipeIndex = -1;

    new void Start()
    {
        base.Start();
        ShowRecipeProducts(0);
    }

    public void ShowRecipeProducts(int page)
    {
        if (knownRecipes == null)
        {
            ProcessingRecipe product = ProcessingInventory.curr.recipe;
            gridSlots[0].ShowSpriteWithTooltip(product.icon, product.recipeName, product.recipeDescription);
            ingredientsGrid.ShowIngredients(new ItemStack[] { product.fuel });
        }
        else
        {
            Recipe[] products = knownRecipes.recipeList;

            for (int i = 0; i < gridSlots.Length; i++)
            {
                if (i < products.Length) gridSlots[i].ShowSpriteWithTooltip(products[i].icon, products[i].recipeName, products[i].recipeDescription);
                else gridSlots[i].ShowSprite(null);
            }
        }
    }
    public void SelectRecipe(int index)
    {
        //if (index == selectedRecipeIndex) ActivateRecipe(index);
        ShowRecipeIngredients(index);
        selectedRecipeIndex = index;
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
