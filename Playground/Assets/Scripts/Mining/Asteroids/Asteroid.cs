using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MineableObject
{
    new void Start()
    {
        base.Start();
        transform.rotation = Random.rotation;
        transform.localScale *= (Random.value + 1) / 2;
    }
}
