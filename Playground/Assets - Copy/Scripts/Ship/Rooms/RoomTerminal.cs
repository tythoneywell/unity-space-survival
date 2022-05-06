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
        RoomWrapper.curr = room;
        if (!room.built)
        {
            PlayerUIController.main.OpenBuildMenu();
        }
        else
        {
            room.Interact(presser);
        }
    }
    public bool Repair(int amount)
    {
        if (!room.built)
        {
            return false;
        }
        else
        {
            return room.Repair(amount);
        }
    }
}
