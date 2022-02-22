using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class Player : MonoBehaviour
{

    [SerializeField]
    Rigidbody rigidBody;
    Inventory inventory;
    // Start is called before the first frame update
    bool isWalking = false;
    [SerializeField]
    Camera camera;
    bool canJump = true;
    public Vector2 sensitivity;
    int walking_speed = 5;
    Vector2 walking_dir = new Vector2(0,0);
    void Start()
    {
        this.inventory = new Inventory();
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void Interact(InputAction.CallbackContext context){
        if(context.performed){
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
                if(hitObject.GetComponent("StorableObject") != null){
                    BaseObject obj = (BaseObject)hitObject.GetComponent("StorableObject");
                    obj.onInteract(gameObject);
                    Destroy(hitObject);
                } else {
                    Debug.Log("No Ray");
                }
            }
        }
    }
    public void Move(InputAction.CallbackContext context)
    {
        if (context.performed || context.canceled){
            if (isWalking == false){
                isWalking = true;
                Vector2 dir = context.ReadValue<Vector2>();
                walking_dir = new Vector2(dir.x, dir.y);
            } else {
                Vector2 dir = context.ReadValue<Vector2>();
                if (!(dir - walking_dir == new Vector2(0,0)) && !dir.Equals(new Vector2(0,0))){
                    walking_dir = dir;
                } else {
                    float curr_y = rigidBody.velocity.y;
                    Vector3 velocity = new Vector3(0, curr_y, 0);
                    rigidBody.velocity = velocity;
                    isWalking = false;
                }
            }
            //transform.position += new Vector3(delta.x, 0, delta.y);
        }
    }
     public void Look(InputAction.CallbackContext context){
         if (context.performed){
            Vector2 direction = context.ReadValue<Vector2>();
            gameObject.transform.Rotate(new Vector3(0, direction.x*sensitivity.x, 0));
            camera.transform.Rotate(new Vector3(direction.y*-1*sensitivity.y, 0, 0));
         }
     }
     public bool addToInventory(ItemStack stack){
        Inventory tempInventory = this.inventory;
        bool result = tempInventory.addItem(stack);
        this.inventory = tempInventory;
        return result;
     }
     void OnCollisionEnter (Collision other) 
     {
         canJump = true;
     }
 
    void OnCollisionExit (Collision other) 
     {
         canJump = false;
     }
    public void Jump(InputAction.CallbackContext context) {
        if (context.performed){
            if (canJump){
                rigidBody.AddForce(new Vector3(0, 6, 0), ForceMode.Impulse);
            }
        }
        Debug.Log(this.inventory.getItem(0).regName);
        Debug.Log(this.inventory.getItem(0).count);

    }
    // Update is called once per frame
    void Update()
    {
        if(isWalking){
            float curr_y = rigidBody.velocity.y;
            Vector3 velocity = gameObject.transform.rotation * new Vector3(walking_speed*walking_dir.x, curr_y, walking_speed * walking_dir.y);
            rigidBody.velocity = velocity;
        }
    }
}
