using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeSelectGrid : UISlotGrid
{
    public RoomRecipeRegistry knownRecipes;

    public GameObject activeRecipeCursor;
    public RecipeIngredientsGrid ingredientsGrid;

    public RoomWrapper targetRoom;

    new void Start()
    {
        base.Start();
        ShowRecipeProducts(0);
    }

    public void ShowRecipeProducts(int page)
    {
        RoomRecipe[] products = knownRecipes.recipeList;

        for (int i = 0; i < gridSlots.Length; i++)
        {
            if (i < products.Length) gridSlots[i].ShowSpriteWithTooltip(products[i].icon, products[i].roomName);
            else gridSlots[i].ShowSprite(null);
        }
    }
    public void SelectRecipe(int index)
    {
        ShowRecipeIngredients(index);
        activeRecipeCursor.transform.position = gridSlots[index].transform.position;
    }
    public void BuildRecipe(int index)
    {
        targetRoom.Build(knownRecipes.recipeList[index]);
        PlayerUIController.main.HideBuildMenu();
    }
    void ShowRecipeIngredients(int index)
    {
        if (index >= knownRecipes.recipeList.Length)
        {
            ingredientsGrid.ShowIngredients(new ItemStack[0]);
        }
        else
        {
            ingredientsGrid.ShowIngredients(knownRecipes.recipeList[index].recipe);
        }
    }

    protected override GameObject MakeSlot()
    {
        GameObject newSlot = Instantiate(PlayerUIController.emptySprite, rTransform);
        newSlot.AddComponent<RecipeSelectSlot>();

        return newSlot;
    }
}
