using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomStatusDisplay : MonoBehaviour
{
    public RoomWrapper room;

    public GameObject integrityMeter;

    Vector3 meterStartScale;

    void Start()
    {
        meterStartScale = integrityMeter.transform.localScale;
    }


    void Update()
    {
        integrityMeter.transform.localScale = Vector3.Scale(meterStartScale, new Vector3((float)room.health / 10, 1, 1));
    }
}
