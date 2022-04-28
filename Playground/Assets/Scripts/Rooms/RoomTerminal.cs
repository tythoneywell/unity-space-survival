using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTerminal : InteractableObject
{
    RoomWrapper room;
    bool built = false;

    void Start()
    {
        room = transform.parent.GetComponent<RoomWrapper>();
    }

    public override void OnInteract(PlayerInteraction presser)
    {
        if (!built)
        {
            PlayerUIController.main.OpenBuildMenu(room);
        }
        else
        {
            room.Interact(presser);
        }
        Debug.Log("room terminal used");
    }
}
