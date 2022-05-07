using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShipControlSwapper : InteractableObject
{
    public override void OnInteract(PlayerInteraction presser)
    {
        ShipController.main.EnterShipMode();
    }
    public override string GetInteractPrompt()
    {
        if (!ShipController.main.controllingShip) return "[RMB] to Pilot Ship";
        else return "[E] to Exit Ship";
    }
}
