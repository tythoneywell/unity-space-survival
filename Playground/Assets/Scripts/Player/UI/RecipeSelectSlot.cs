using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeSelectSlot : UISlot
{
    public override void PrimaryActivate()
    {
        ((RecipeSelectGrid)parentGrid).SelectRecipe(index);
    }
    public override void TertiaryActivate()
    {
        ((RecipeSelectGrid)parentGrid).BuildRecipe(index);
    }
}
