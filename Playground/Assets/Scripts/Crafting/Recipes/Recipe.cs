using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Recipe : ScriptableObject
{
    public ItemStack[] ingredients;

    public string recipeName;
    public Sprite icon;
}
