using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MineableObject
{
    public int collideDamage;

    new void Start()
    {
        base.Start();
        transform.rotation = Random.rotation;
        transform.localScale *= (Random.value + 1) / 2;
    }
}
