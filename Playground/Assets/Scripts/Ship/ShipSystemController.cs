using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Ship system which handles the combined functionality of each room
 */
public class ShipSystemController : MonoBehaviour
{
    public static ShipSystemController main;

    // Tweakable params
    public float basePowerProduction;
    public float baseShieldCapacity;
    public float baseShieldChargeRate;

    // Public values for other scripts
    public float powerSatisfaction;
    public float currShields;
    public float oxygenAmount;

    // Private ship stats
    float powerProduction;
    float powerConsumption;
    float shieldCapacity;
    float shieldChargeRate;

    ShipSystem[] shipSystems;

    void Awake()
    {
        main = this;
    }

    void Start()
    {
        shipSystems = gameObject.GetComponentsInChildren<ShipSystem>();
    }

    void Update()
    {
        UpdateStats();
        currShields = Mathf.Clamp(currShields + shieldChargeRate * Time.deltaTime, 0, shieldCapacity);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.impulse.magnitude > 3000)
        {
            //Debug.Log("collided with " + collision.collider.name);
            //Debug.Log("force " + collision.impulse.magnitude);
            //Debug.DrawLine(collision.transform.position, Vector3.Scale(collision.transform.position, collision.impulse / 1000), Color.red, 10f);
            DamageSystems(1);
        }
    }

    void UpdateStats()
    {
        powerProduction = basePowerProduction;
        powerConsumption = 0f;
        shieldCapacity = baseShieldCapacity;
        shieldChargeRate = baseShieldChargeRate;
        foreach (ShipSystem sys in shipSystems)
        {
            powerProduction += sys.powerProduction;
            powerConsumption += sys.powerConsumption;
            shieldCapacity += sys.shieldCapacity;
            shieldChargeRate += sys.shieldChargeRate;
        }
        powerSatisfaction = powerProduction > powerConsumption ? 1 : powerProduction / powerConsumption;
    }

    void DamageSystems(int damage)
    {
        if (currShields > 1)
        {
            currShields -= damage;
            return;
        }

        List<ShipSystem> possibleTargets = new List<ShipSystem>();
        foreach (ShipSystem sys in shipSystems)
        {
            if (sys.operational) possibleTargets.Add(sys);
        }
        if (possibleTargets.Count > 0)
        {
            ShipSystem targetSys = possibleTargets[(int)Random.Range(0, (float)possibleTargets.Count - 0.001f)];
            targetSys.health -= 1;
        }
        else
        {
            print("ship is completely trashed");
        }
    }
}
