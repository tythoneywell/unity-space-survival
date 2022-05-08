using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Interactable which handles building / upgrading / using rooms
 */
public class UpgradeTerminal : InteractableObject
{
    public static RecipeRegistry activeUpgrades;

    public RecipeRegistry knownUpgrades;
    public ShipSystem sys;
    public string sysName;

    public override void OnInteract(PlayerInteraction presser)
    {
        activeUpgrades = knownUpgrades;
        ShipSystem.curr = sys;
        PlayerUIController.main.OpenUpgradeMenu();
    }
    public override string GetInteractPrompt()
    {
        return "[RMB] to upgrade " + sysName;
    }
}
