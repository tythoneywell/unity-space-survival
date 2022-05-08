using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recipe : ScriptableObject
{
    public ItemStack[] ingredients;

    public string recipeName;
    [TextArea]
    public string recipeDescription;
    public Sprite icon;
}
