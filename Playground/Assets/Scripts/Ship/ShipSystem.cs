using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Class for any component of the ship
 */
public class ShipSystem: MonoBehaviour
{
    public bool operational;
    public bool working;
    public int health;
    public float powerProduction;
    public float powerConsumption;
    public float shieldCapacity;
    public float shieldChargeRate;
}
