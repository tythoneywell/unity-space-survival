using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class TurretController : MonoBehaviour
{

    // Tweakable params
    [SerializeField]
    Vector2 lookSensitivity;
    
    // Private refs
    Camera playerCamera;

    float vertLookAngle = 0f;


    // Start is called before the first frame update
    void Start()
    {
        playerCamera = gameObject.GetComponentInChildren<Camera>();

        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /* Input */
    public void Look(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Vector2 direction = context.ReadValue<Vector2>();
            gameObject.transform.Rotate(new Vector3(0, direction.x * lookSensitivity.x, 0));

            vertLookAngle += lookSensitivity.y * -direction.y;
            vertLookAngle = Mathf.Clamp(vertLookAngle, -90, 90);
            playerCamera.transform.localRotation = Quaternion.Euler(vertLookAngle, 0, 0);
        }
    }
}
