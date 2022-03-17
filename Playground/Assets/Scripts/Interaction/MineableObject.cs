using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineableObject : MonoBehaviour
{
    float health;
    private bool isBroken;

    // Start is called before the first frame update
    void Start()
    {
        health = 100;
        isBroken = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void damageHealth(float laserStrength) 
    {
        if (health > 0)
        {
            health = health - laserStrength;
        }
        else 
        {
            isBroken = true;
        }
    }

    bool getStatus() 
    {
        return isBroken;
    }
}
