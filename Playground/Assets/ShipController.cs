using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShipController : MonoBehaviour
{

    public float baseSpeed;
    public float baseFriction;

    AsteroidField asteroidField;

    float targetSpeed;
    Vector3 shipTurnRate;

    Vector3 speedVec;

    void Start()
    {
        asteroidField = GameObject.Find("AsteroidField").GetComponent<AsteroidField>();
    }

    void Update()
    {
        transform.Rotate(shipTurnRate);

        speedVec += targetSpeed * transform.forward * Time.deltaTime;
        speedVec *= Mathf.Pow(1 / (1 + baseFriction), Time.deltaTime);

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
            Vector2 dir = context.ReadValue<Vector2>();
            shipTurnRate = new Vector3(dir.y, 0, -dir.x);
        }
    }
}
