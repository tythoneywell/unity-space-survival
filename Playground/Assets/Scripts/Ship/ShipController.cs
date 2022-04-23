using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShipController : GravityWell
{
    [SerializeField]
    public float baseSpeed = 800;
    [SerializeField]
    public float baseFriction = 4;
    [SerializeField]
    float turnSpeed = 30;

    AsteroidField asteroidField;

    float targetSpeed;
    Vector3 shipTurnRate;

    Vector3 speedVec;

    void Start()
    {
        asteroidField = GameObject.Find("AsteroidField").GetComponent<AsteroidField>();
    }

    protected override void ManualFixedUpdate()
    {
        transform.Rotate(turnSpeed * shipTurnRate * Time.fixedDeltaTime);

        speedVec += targetSpeed * transform.forward * Time.fixedDeltaTime;
        speedVec *= Mathf.Pow(1 / (1 + baseFriction), Time.fixedDeltaTime);

        asteroidField.PanField(speedVec);
    }

    public void UpdateThrust(InputAction.CallbackContext context)
    {
        if (context.started)
            targetSpeed = baseSpeed;
        if (context.canceled)
            targetSpeed = 0;
    }
    public void UpdateRotationSpeed(InputAction.CallbackContext context)
    {
        if (context.performed || context.canceled)
        {
            Vector3 dir = context.ReadValue<Vector3>();
            shipTurnRate = new Vector3(-dir.y, -dir.z, -dir.x);
        }
    }
}
