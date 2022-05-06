using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSurvival : MonoBehaviour
{
    public static PlayerSurvival main;

    public float hunger;
    public float foodQuality;

    void Awake()
    {
        main = this;
    }

    void Start()
    {
        hunger = 0.8f;
    }

    void Update()
    {
        hunger -= Time.deltaTime / 300;
    }

    public void Eat(ItemData item)
    {
        hunger += (float)item.hungerRestoreAmount / 10;
    }
}
