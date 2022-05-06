using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShakyCam : MonoBehaviour
{
    Vector3 rootPos;
    float shakeTimeLeft;

    void Start()
    {
        rootPos = transform.localPosition;
        ShipSystemController.main.onDamage.AddListener(StartShake);
    }

    void StartShake()
    {
        StartCoroutine(Shake());
    }
    IEnumerator Shake()
    {
        if (shakeTimeLeft > 0)
        {
            shakeTimeLeft = 1f;
            yield break;
        }
        shakeTimeLeft = 1f;
        while (shakeTimeLeft > 0)
        {
            transform.localPosition = rootPos + Random.insideUnitSphere * shakeTimeLeft;
            shakeTimeLeft -= Time.deltaTime;
            yield return null;
        }
        shakeTimeLeft = 0f;
        transform.localPosition = rootPos;
    }
    private void OnEnable()
    {
        shakeTimeLeft = 0f;
    }
}
