using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class InteractableObject : MonoBehaviour
{
    abstract public void Interact(PlayerInteraction presser);
}
