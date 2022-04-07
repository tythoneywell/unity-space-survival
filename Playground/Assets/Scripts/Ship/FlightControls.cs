using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FlightControls : InteractableObject
{
   private InputAction movement;
   //[SerializeField]
   //private float speed = 10f;
   //[SerializeField]
   //private float mouseSensitivity = 1f;
   public override void Interact(PlayerInteraction presser) {
      presser.transform.position = this.transform.position;
   }
}
