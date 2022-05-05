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
}
