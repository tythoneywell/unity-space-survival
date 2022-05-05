using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Interactable which handles building / upgrading / using rooms
 */
public class RoomTerminal : InteractableObject
{
    RoomWrapper room;

    void Start()
    {
        room = transform.parent.GetComponent<RoomWrapper>();
    }

    public override void OnInteract(PlayerInteraction presser)
    {
        if (!room.built)
        {
            PlayerUIController.main.OpenBuildMenu(room);
        }
        else
        {
            room.Interact(presser);
        }
    }
}
