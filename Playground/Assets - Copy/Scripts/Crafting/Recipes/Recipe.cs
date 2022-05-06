using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recipe : ScriptableObject
{
    public ItemStack[] ingredients;

    public string recipeName;
    public Sprite icon;
}
