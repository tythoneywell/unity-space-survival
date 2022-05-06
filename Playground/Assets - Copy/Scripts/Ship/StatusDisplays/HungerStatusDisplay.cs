using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HungerStatusDisplay : MonoBehaviour
{
    public GameObject hungerMeter;

    Vector3 meterStartScale;

    void Start()
    {
        meterStartScale = hungerMeter.transform.localScale;
    }


    void Update()
    {
        hungerMeter.transform.localScale = Vector3.Scale(meterStartScale, new Vector3(PlayerSurvival.main.hunger, 1, 1));
    }
}
