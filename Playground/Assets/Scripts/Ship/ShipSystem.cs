using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Class for any component of the ship
 */
public class ShipSystem : MonoBehaviour
{
    public static ShipSystem curr;

    public bool operational;
    public bool working;
    public int health;
    public float powerProduction;
    public float powerConsumption;
    public float shieldCapacity;
    public float shieldChargeRate;

    protected bool[] installedUpgrades;

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health < 0)
        {
            operational = false;
            health = 0;
        }
    }
    public void Repair()
    {
        health = 10;
        operational = true;
    }
    public virtual void Upgrade(UpgradeRecipe upgrade)
    {
        Debug.Log("UPGRADE!");
    }
}
