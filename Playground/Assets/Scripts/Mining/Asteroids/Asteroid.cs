using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MineableObject
{
    public int collideDamage;
    public bool danger;

    public static float shrinkStartDist;
    public static float shrinkDist;

    Vector3 baseScale;

    new void Start()
    {
        base.Start();
        transform.rotation = Random.rotation;
        transform.localScale *= (Random.value + 1) / 2;
        baseScale = transform.localScale;
        CalculateShrink();
    }

    private void Update()
    {
        CalculateShrink();
    }

    void CalculateShrink()
    {
        float shrinkFactor = transform.position.sqrMagnitude - shrinkStartDist * shrinkStartDist;
        if (shrinkFactor > 0)
        {
            transform.localScale = baseScale * Mathf.Clamp01(1 - shrinkFactor / (shrinkDist * shrinkDist));
        }
        else
        {
            transform.localScale = baseScale;
        }
    }
}
