using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/*
 * Ship system which handles the combined functionality of each room
 */
public class ShipSystemController : MonoBehaviour
{
    public static ShipSystemController main;

    public UnityEvent onDamage;
    public UnityEvent onBlockDamage;

    // Tweakable params
    public float basePowerProduction;
    public float basePowerConsumption;
    public float baseShieldCapacity;
    public float baseShieldChargeRate;

    // Public values for other scripts
    public float powerSatisfaction;
    public float currShields;
    public float oxygenAmount;

    // Private ship stats
    float powerProduction;
    float powerConsumption;
    public float shieldCapacity;
    float shieldChargeRate;
    float damageInvulnTimer;

    ShipSystem[] shipSystems;

    void Awake()
    {
        main = this;
        onDamage = new UnityEvent();
        onBlockDamage = new UnityEvent();
    }

    void Start()
    {
        shipSystems = gameObject.GetComponentsInChildren<ShipSystem>();
        oxygenAmount = 1;
    }

    void Update()
    {
        UpdateStats();
        currShields = Mathf.Clamp(currShields + shieldChargeRate * Time.deltaTime, 0, shieldCapacity);
        oxygenAmount = Mathf.Clamp01(oxygenAmount - Time.deltaTime / 120);
        damageInvulnTimer = Mathf.Clamp(damageInvulnTimer - Time.deltaTime, 0, 1);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.impulse.magnitude > 1500)
        {
            //Debug.Log("collided with " + collision.collider.name);
            //Debug.Log("force " + collision.impulse.magnitude);
            //Debug.DrawLine(collision.transform.position, Vector3.Scale(collision.transform.position, collision.impulse / 1000), Color.red, 10f);
            Asteroid asteroid = collision.transform.GetComponent<Asteroid>();
            if (asteroid != null)
            {
                if (asteroid.collideDamage > 0) DamageSystems(asteroid.collideDamage);
                asteroid.BreakNoReward(collision.relativeVelocity);
            }
        }
    }

    void UpdateStats()
    {
        powerProduction = basePowerProduction;
        powerConsumption = basePowerConsumption;
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
            onBlockDamage.Invoke();
            return;
        }

        onDamage.Invoke();

        if (damageInvulnTimer > 0)
        {
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
            targetSys.TakeDamage(1);
        }
        else
        {
            print("ship is completely trashed");
        }
        damageInvulnTimer = 1f;
    }
}
