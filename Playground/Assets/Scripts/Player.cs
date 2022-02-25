﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class Player : GravityWellObject
{
    // Tweakable params
    [SerializeField]
    Vector2 lookSensitivity;
    [SerializeField]
    float walkingSpeed = 5f;
    [SerializeField]
    float jumpSpeed = 2.5f;
    // Overrode this to use the physics system's built-in gravity (can be tweaked in-editor)
    //[SerializeField]
    float gravity;
    [SerializeField]
    bool debugJump = false;

    // Private constants (the same as tweakable params, only I don't want them to show up in-editor.)
    const float collisionRadius = 0.8f;

    // Private refs
    Camera playerCamera;
    Inventory inventory;

    // Private vars
    bool isWalking = false;
    bool grounded = true;

    float vertLookAngle = 0f;

    Vector2 hspeed = Vector2.zero;
    float vspeed = 0f;

    
    void Start()
    {
        gravity = -Physics.gravity.y;
        playerCamera = gameObject.GetComponentInChildren<Camera>();
        this.inventory = new Inventory();
        Cursor.lockState = CursorLockMode.Locked;
    }

    public override void ManualFixedUpdate()
    {
        // If in the air, move through the air and check if we've landed
        if (!grounded)
        {
            vspeed = Mathf.Max(-20, vspeed - gravity * Time.fixedDeltaTime);
            Vector3 moveDir = new Vector3(hspeed.x, vspeed, hspeed.y) * Time.fixedDeltaTime;
            TryJumpDirection(moveDir); // grounded flag updated if landed
        }
        if (grounded) // DO NOT MAKE ELSE
        {
            vspeed = 0;
            Vector3 moveDir = new Vector3(hspeed.x, 0, hspeed.y) * Time.fixedDeltaTime;
            TryWalkDirection(moveDir);
        }
    }

    /* Input */
    public void Interact(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            float rayDistance = 1000.0f;
            // Ray from the center of the viewport.
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
            RaycastHit rayHit;
            // Check if we hit something.
            if (Physics.Raycast(ray, out rayHit, rayDistance))
            {
                // Get the object that was hit.
                GameObject hitObject = rayHit.collider.gameObject;
                // Check if it is storable
                if (hitObject.GetComponent("StorableObject") != null)
                {
                    BaseObject obj = (BaseObject)hitObject.GetComponent("StorableObject");
                    obj.onInteract(gameObject);
                    Destroy(hitObject);
                }
                else
                {
                    Debug.Log("No Interactable Object Found");
                }
            }
        }
    }
    
    public void Move(InputAction.CallbackContext context)
    {
        Vector2 dir = context.ReadValue<Vector2>();
        if (isWalking == false)
        {
            isWalking = true;
            hspeed = walkingSpeed * dir;
        }
        else
        {
            if (Vector3.Distance(dir, Vector3.zero) > 0)
            {
                hspeed = walkingSpeed * dir;
            }
            else
            {
                isWalking = false;
                hspeed = Vector2.zero;
            }
        }
    }
    
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
    
    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (grounded || debugJump)
            {
                grounded = false;
                vspeed = jumpSpeed;
            }
        }

    }

    /* Movement */
    void TryWalkDirection(Vector3 dir)
    {
        Vector3 destination = transform.position + transform.rotation * dir;

        RaycastHit hit;
        if (Physics.SphereCast(destination, 0.5f, -transform.up, out hit, 1.3f))
        {
            Vector3 prevPos = transform.position;
            transform.Translate(dir + Vector3.up * (0.5f - hit.distance));
            WallCollide(prevPos);
        }
        else
        {
            //Debug.Log("fallen");
            grounded = false;
        }
    }
    void TryJumpDirection(Vector3 dir)
    {
        Vector3 destination = transform.position + transform.rotation * dir;

        RaycastHit hit;
        if (Physics.SphereCast(destination + transform.up, 0.5f, -transform.up, out hit, 1.5f))
        {
            //Debug.Log("landed");
            grounded = true;
        }
        else
        {
            Vector3 prevPos = transform.position;
            transform.Translate(dir);
            WallCollide(prevPos);
        }
    }
    void WallCollide(Vector3 prevPos)
    {
        RaycastHit hit;
        Vector3 revisedPos = transform.position;
        int infiniteBreak = 0;
        while (Physics.SphereCast(prevPos, collisionRadius, revisedPos - prevPos, out hit, Vector3.Distance(revisedPos, prevPos)))
        {
            infiniteBreak += 1;
            if (infiniteBreak > 200)
            {
                Debug.Log("Player is being crushed.");
                break;
            }
            revisedPos += Vector3.Project((hit.point + (collisionRadius + 0.01f) * hit.normal) - revisedPos, hit.normal);
        }
        while (Physics.SphereCast(prevPos, collisionRadius - 0.1f, revisedPos - prevPos, out hit, Vector3.Distance(revisedPos, prevPos) + 0.1f))
        {
            infiniteBreak += 1;
            if (infiniteBreak > 200)
            {
                Debug.Log("Player is being crushed.");
                break;
            }
            revisedPos += Vector3.Project((hit.point + (collisionRadius + 0.01f) * hit.normal) - revisedPos, hit.normal);
        }

        transform.Translate(revisedPos - transform.position, Space.World);
    }

    /* Inventory System */
    public bool AddToInventory(ItemStack stack)
    {
        Inventory tempInventory = this.inventory;
        bool result = tempInventory.addItem(stack);
        this.inventory = tempInventory;
        return result;
    }

}
