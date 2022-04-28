using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShipControlSwapper : InteractableObject
{
    
    private PlayerInput playerInput;
    private bool controllingShip;

    void Start(){
        controllingShip = false;
    }

    public override void OnInteract(PlayerInteraction presser) {
        ShipController.main.EnterShipMode();
    }
}
