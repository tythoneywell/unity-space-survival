using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomButton : MonoBehaviour
{
    public void Press()
    {
        RoomWrapper.curr.Interact(PlayerInteraction.main);
    }
}
