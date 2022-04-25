using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSystem : MonoBehaviour
{
    RoomWrapper[] rooms;
    
    void Start()
    {
        rooms = gameObject.GetComponentsInChildren<RoomWrapper>();
    }

    void Update()
    {
        
    }
}
