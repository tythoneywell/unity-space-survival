using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeRecipe", menuName = "ScriptableObjects/UpgradeRecipe", order = 1)]
public class UpgradeRecipe : Recipe
{
    public enum UpgradeType
    {
        POWER,
        EFFICIENCY,
        RANGE,
    }
    public UpgradeType upgradeType;
}
