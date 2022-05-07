using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingStation : InteractableObject
{
    public static CraftingStation curr;

    public RecipeRegistry knownRecipes;

    public override void OnInteract(PlayerInteraction presser)
    {
        curr = this;
        PlayerUIController.main.OpenCraftingMenu();
    }
    public void Craft(Recipe recipe)
    { 
        if (PlayerInventory.main.HasRecipeIngredients(recipe))
        {
            PlayerInventory.main.ConsumeRecipeIngredients(recipe);
            PlayerInventory.main.AddItemNoIndex(new ItemStack(((ItemRecipe)recipe).result));
            PlayerUIController.main.UpdateInventory();
        }
    }
    public override string GetInteractPrompt()
    {
        return "[RMB] to open Crafting menu";
    }
}
