using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShipController : GravityWell
{
    public static ShipController main;

    [SerializeField]
    public float baseSpeed = 800;
    [SerializeField]
    public float baseFriction = 4;
    [SerializeField]
    float turnSpeed = 30;

    public GameObject rocketCam;
    public GameObject hideableShipBody;

    AsteroidField asteroidField;

    float targetSpeed;
    Vector3 shipTurnRate;

    Vector3 speedVec;

    private void Awake()
    {
        main = this;
    }

    new void Start()
    {
        base.Start();
        asteroidField = GameObject.Find("AsteroidField").GetComponent<AsteroidField>();
    }

    protected override void ManualFixedUpdate()
    {
        gameObject.GetComponent<Rigidbody>().MoveRotation(transform.rotation * Quaternion.Euler(turnSpeed * shipTurnRate * Time.fixedDeltaTime));

        speedVec += targetSpeed * transform.forward * Time.fixedDeltaTime;
        speedVec *= Mathf.Pow(1 / (1 + baseFriction), Time.fixedDeltaTime);

        asteroidField.PanField(speedVec);
    }

    public void EnterShipMode()
    {
        Cursor.lockState = CursorLockMode.Confined;
        rocketCam.SetActive(true);
        hideableShipBody.SetActive(true);
        PlayerInteraction.main.GetComponent<PlayerInput>().SwitchCurrentActionMap("Ship");
    }
    public void LeaveShipMode(InputAction.CallbackContext context)
    {
        Cursor.lockState = CursorLockMode.Locked;
        rocketCam.SetActive(false);
        hideableShipBody.SetActive(false);
        PlayerInteraction.main.GetComponent<PlayerInput>().SwitchCurrentActionMap("Player");
        shipTurnRate = Vector3.zero;
    }

    public void UpdateThrust(InputAction.CallbackContext context)
    {
        if (context.started)
            targetSpeed = baseSpeed;
        if (context.canceled)
            targetSpeed = 0;
    }
    public void UpdateMovementKeyboard(InputAction.CallbackContext context)
    {
        if (context.performed || context.canceled)
        {
            Vector3 dir = context.ReadValue<Vector3>();
            shipTurnRate.z = -dir.x;

            targetSpeed = dir.y * baseSpeed;
        }
    }
    public void UpdateMovementMouse(InputAction.CallbackContext context)
    {
        if (context.performed || context.canceled)
        {
            Vector3 dir = context.ReadValue<Vector2>();
            dir -= new Vector3(Camera.main.pixelWidth / 2, Camera.main.pixelHeight / 2);
            dir /= Camera.main.pixelWidth / 2;
            shipTurnRate.x = -Mathf.Sign(dir.y) * Mathf.Clamp(Mathf.Abs(dir.y) - 0.35f, 0, 1);
            shipTurnRate.y = Mathf.Sign(dir.x) * Mathf.Clamp(Mathf.Abs(dir.x) - 0.35f, 0, 1);
        }
    }
}
