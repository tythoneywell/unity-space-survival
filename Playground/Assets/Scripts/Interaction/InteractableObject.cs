using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class InteractableObject : MonoBehaviour
{
    abstract public void OnInteract(PlayerInteraction presser);
    public virtual string GetInteractPrompt() { return ""; }
}
