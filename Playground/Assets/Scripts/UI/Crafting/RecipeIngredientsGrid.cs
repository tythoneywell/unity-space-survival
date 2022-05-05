using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeIngredientsGrid : UISlotGrid
{
    ItemStack[] shownIngredients = new ItemStack[0];

    new void Start()
    {
        base.Start();
        ShowIngredients(shownIngredients);
    }

    public void ShowIngredients(ItemStack[] ingredients)
    {
        shownIngredients = ingredients;
        for (int i = 0; i < gridSlots.Length; i++)
        {
            if (i < ingredients.Length) gridSlots[i].ShowStack(ingredients[i]);
            else gridSlots[i].ShowSprite(null);
        }
    }
}
