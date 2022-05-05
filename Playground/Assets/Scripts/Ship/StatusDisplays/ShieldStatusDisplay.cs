using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldStatusDisplay : MonoBehaviour
{
    public GameObject shieldMeter;

    Vector3 meterStartScale;

    void Start()
    {
        meterStartScale = shieldMeter.transform.localScale;
    }


    void Update()
    {
        if (ShipSystemController.main.shieldCapacity > 0)
        {
            shieldMeter.transform.localScale = Vector3.Scale(meterStartScale, new Vector3(ShipSystemController.main.currShields / ShipSystemController.main.shieldCapacity, 1, 1));
        }
        else
        {
            shieldMeter.transform.localScale = Vector3.Scale(meterStartScale, new Vector3(0, 1, 1));
        }
    }
}
