using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipStatusDisplay : MonoBehaviour
{
    public GameObject powerMeter;
    public GameObject oxygenMeter;

    Vector3 meterStartScale;

    void Start()
    {
        meterStartScale = powerMeter.transform.localScale;
    }


    void Update()
    {
        powerMeter.transform.localScale = Vector3.Scale(meterStartScale, new Vector3(ShipSystemController.main.powerSatisfaction, 1, 1));
        oxygenMeter.transform.localScale = Vector3.Scale(meterStartScale, new Vector3(ShipSystemController.main.oxygenAmount, 1, 1));
    }
}
