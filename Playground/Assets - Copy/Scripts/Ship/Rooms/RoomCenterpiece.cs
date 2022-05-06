using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomCenterpiece : MonoBehaviour
{
    public bool working;
    public Transform spinningBit;

    void Update()
    {
        if (working)
            spinningBit.Rotate(0, 180 * Time.deltaTime * ShipSystemController.main.powerSatisfaction, 0);
    }
}
