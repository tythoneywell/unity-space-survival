using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    public Material litMat;
    public Material deadMat;

    Light light;
    MeshRenderer lightMesh;

    void Start()
    {
        light = gameObject.GetComponent<Light>();
        lightMesh = gameObject.GetComponentInChildren<MeshRenderer>();
        StartCoroutine(FlickerTimer());
    }

    IEnumerator FlickerTimer()
    {
        while(true)
        {
            if (Random.value > ShipSystemController.main.powerSatisfaction)
            {
                light.enabled = false;
                lightMesh.material = deadMat;
            }
            else
            {
                light.enabled = true;
                lightMesh.material = litMat;
            }
            yield return new WaitForSeconds(2f + Random.value);
        }
    }
}
