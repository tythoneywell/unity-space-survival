using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrusterSystem : ShipSystem
{
    public float bonusSpeed = 0;
    public float bonusTurnSpeed = 0;

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
                    bonusSpeed += 1000f;
                    installedUpgrades[0] = true;
                    PlayerInventory.main.ConsumeRecipeIngredients(upgrade);
                }
                break;
            case UpgradeRecipe.UpgradeType.POWER:
                if (!installedUpgrades[1] && PlayerInventory.main.HasRecipeIngredients(upgrade))
                {
                    bonusSpeed += 2000f;
                    installedUpgrades[1] = true;
                    PlayerInventory.main.ConsumeRecipeIngredients(upgrade);
                }
                break;
            case UpgradeRecipe.UpgradeType.RANGE:
                if (!installedUpgrades[2] && PlayerInventory.main.HasRecipeIngredients(upgrade))
                {
                    bonusTurnSpeed += 20f;
                    installedUpgrades[2] = true;
                    PlayerInventory.main.ConsumeRecipeIngredients(upgrade);
                }
                break;
        }
    }
}
