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

    public bool controllingShip = false;

    const float deadzone = .35f;

    AsteroidField asteroidField;
    public GameObject[] asteroidFieldList;
    int currZone = 0;

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
        controllingShip = true;
        Cursor.lockState = CursorLockMode.Confined;
        rocketCam.SetActive(true);
        hideableShipBody.SetActive(true);
        PlayerInteraction.main.GetComponent<PlayerInput>().SwitchCurrentActionMap("Ship");
    }
    public void LeaveShipMode(InputAction.CallbackContext context)
    {
        controllingShip = false;
        Cursor.lockState = CursorLockMode.Locked;
        rocketCam.SetActive(false);
        hideableShipBody.SetActive(false);
        PlayerInteraction.main.GetComponent<PlayerInput>().SwitchCurrentActionMap("Player");
        shipTurnRate = Vector3.zero;
    }

    public void Warp()
    {
        currZone++;
        Destroy(asteroidField.gameObject);
        asteroidField = Instantiate(asteroidFieldList[currZone], Vector3.zero, Quaternion.identity).GetComponent<AsteroidField>();
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
            Vector2 dir = context.ReadValue<Vector2>();
            Vector2 camDimensions = new Vector2(Camera.main.pixelWidth, Camera.main.pixelHeight);
            dir -= camDimensions / 2; // Center mouse inpt
            dir = new Vector2(2 * dir.x / camDimensions.x, 2 * dir.y / camDimensions.y); // Scale mouse input
            dir = new Vector2(Mathf.Sign(dir.x) * Mathf.Clamp((Mathf.Abs(dir.x) - deadzone) / (1 - deadzone), 0, 1),
                -Mathf.Sign(dir.y) * Mathf.Clamp((Mathf.Abs(dir.y) - deadzone) / (1 - deadzone), 0, 1)); // Apply deadzones
            shipTurnRate.y = dir.x;
            shipTurnRate.x = dir.y;
        }
    }
}
