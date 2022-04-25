using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeIngredientsGrid : UISlotGrid
{
    new void Start()
    {
        base.Start();
        ShowIngredients(new ItemStack[0]);
    }

    public void ShowIngredients(ItemStack[] recipe)
    {
        for (int i = 0; i < gridSlots.Length; i++)
        {
            if (i < recipe.Length) gridSlots[i].ShowStack(recipe[i]);
            else gridSlots[i].ShowSprite(null);
        }
    }
}
