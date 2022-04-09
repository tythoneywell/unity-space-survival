using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FlightControls : InteractableObject
{
   private InputAction movement;
   public GameObject rocketCam;
   private PlayerInput playerInput;
   private bool controllingShip;

   //[SerializeField]
   //private float speed = 10f;
   //[SerializeField]
   //private float mouseSensitivity = 1f;
public void Start(){
   controllingShip = false;
}
   public override void Interact(PlayerInteraction presser) {
      playerInput = presser.GetComponent<PlayerInput>();
      presser.transform.rotation = this.transform.rotation;
      presser.transform.position = this.transform.position;
      rocketCam.SetActive(true);
      playerInput.SwitchCurrentActionMap("Ship");
   }

   public void LeaveCockpit(InputAction.CallbackContext context){
      rocketCam.SetActive(false);
      playerInput.SwitchCurrentActionMap("Player");
   }
}
