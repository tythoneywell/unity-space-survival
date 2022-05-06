using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Contains modifiable functionality of a room
 */
public abstract class RoomBackend
{
    public RoomWrapper wrapper;

    public abstract void Build();
    public abstract void Update();
    public abstract void Deconstruct();
    public virtual void Interact(PlayerInteraction presser)
    {
        RoomWrapper.curr = wrapper;
        PlayerUIController.main.OpenBuildMenu();
    }
}
