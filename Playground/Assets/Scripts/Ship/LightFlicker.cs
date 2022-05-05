using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    Light light;

    void Start()
    {
        light = gameObject.GetComponent<Light>();
        StartCoroutine(FlickerTimer());
    }

    IEnumerator FlickerTimer()
    {
        while(true)
        {
            if (Random.value > ShipSystemController.main.powerSatisfaction)
            {
                light.enabled = false;
            }
            else
            {
                light.enabled = true;
            }
            yield return new WaitForSeconds(2f + Random.value);
        }
    }
}
