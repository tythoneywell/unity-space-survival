using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInShip : MonoBehaviour
{
    // Tweakable params
    [SerializeField]
    Vector2 lookSensitivity;
    [SerializeField]
    float walkingSpeed = 5f;
    [SerializeField]
    float jumpSpeed = 2.5f;
    [SerializeField]
    float gravity = 5f;

    // Private refs
    Camera camera;
    Inventory inventory;

    bool isWalking = false;
    bool grounded = true;
    Vector2 hspeed = Vector2.zero;
    float vspeed = 0f;
    
    void Start()
    {
        camera = Camera.main;
        this.inventory = new Inventory();
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    public void UpdateFromShip()
    {
        // If parent has changed, make sure we're aligned with them.
        if (transform.up != transform.parent.up)
        {
            transform.rotation = transform.parent.rotation;
        }

        // If in the air, move through the air and check if we've landed
        if (!grounded)
        {
            vspeed = Mathf.Max(-20, vspeed - gravity * Time.deltaTime);
            Vector3 moveDir = new Vector3(hspeed.x, vspeed, hspeed.y) * Time.deltaTime;
            TryJumpDirection(moveDir); // grounded updated if landed
        }
        if (grounded) // DO NOT MAKE ELSE
        {
            vspeed = 0;
            Vector3 moveDir = new Vector3(hspeed.x, 0, hspeed.y) * Time.deltaTime;
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
                //HighlightObject(hitObject);
                if (hitObject.GetComponent("StorableObject") != null)
                {
                    BaseObject obj = (BaseObject)hitObject.GetComponent("StorableObject");
                    obj.onInteract(gameObject);
                    Destroy(hitObject);
                }
                else
                {
                    Debug.Log("No Ray");
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
            camera.transform.Rotate(new Vector3(direction.y * -1 * lookSensitivity.y, 0, 0));
        }
    }
    
    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (grounded)
            {
                grounded = false;
                vspeed = jumpSpeed;
            }
        }
        /*
        Debug.Log(this.inventory.getItem(0).regName);
        Debug.Log(this.inventory.getItem(0).count);
        */

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
        float bodyThickness = 0.8f;

        RaycastHit hit;
        Vector3 revisedPos = transform.position;
        int infiniteBreak = 0;
        while (Physics.SphereCast(prevPos, bodyThickness, revisedPos - prevPos, out hit, Vector3.Distance(revisedPos, prevPos)))
        {
            infiniteBreak += 1;
            if (infiniteBreak > 200)
            {
                Debug.Log("Player is being crushed.");
                break;
            }
            revisedPos += Vector3.Project((hit.point + (bodyThickness + 0.04f) * hit.normal) - revisedPos, hit.normal);
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
