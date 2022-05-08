using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSystem : ShipSystem
{
    public float bonusDamage = 0;
    public float bonusRange = 0;
    public float powerMul = 1;

    private void Start()
    {
        installedUpgrades = new bool[3];
        for (int i = 0; i < 3; i++)
        {
            installedUpgrades[i] = false;
        }
    }

    public override void Upgrade(UpgradeRecipe upgrade)
    {
        switch (upgrade.upgradeType)
        {
            case UpgradeRecipe.UpgradeType.EFFICIENCY:
                if (!installedUpgrades[0] && PlayerInventory.main.HasRecipeIngredients(upgrade))
                {
                    powerMul = 0.25f;
                    installedUpgrades[0] = true;
                    PlayerInventory.main.ConsumeRecipeIngredients(upgrade);
                }
                break;
            case UpgradeRecipe.UpgradeType.POWER:
                if (!installedUpgrades[1] && PlayerInventory.main.HasRecipeIngredients(upgrade))
                {
                    bonusDamage = 5f;
                    installedUpgrades[1] = true;
                    PlayerInventory.main.ConsumeRecipeIngredients(upgrade);
                }
                break;
            case UpgradeRecipe.UpgradeType.RANGE:
                if (!installedUpgrades[2] && PlayerInventory.main.HasRecipeIngredients(upgrade))
                {
                    bonusRange = 400f;
                    installedUpgrades[2] = true;
                    PlayerInventory.main.ConsumeRecipeIngredients(upgrade);
                }
                break;
        }
    }
}
