using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    public static PlayerInteraction main;

    public Inventory inventory;
    int currentSlotIdx = 1;

    GameObject rightHand;
    Quaternion rightHandRot;
    ItemData heldItem;
    GameObject heldItemModel;
    ItemRegistry registry;

    PlayerUIController UI;

    void Awake()
    {
        main = this;
    }

    void Start()
    {
        rightHand = GameObject.Find("RightArm");
        registry = GameObject.Find("ItemRegistry").GetComponent<ItemRegistry>();
        UI = GameObject.Find("UI").GetComponent<PlayerUIController>();

        rightHandRot = transform.rotation;

        inventory = new Inventory();
        heldItem = inventory.GetItem(0).item;
    }


    public bool AddToInventory(ItemStack stack)
    {
        return inventory.AddItemNoIndex(stack);
    }
    public void RemoveCurrentFromInv(int number = 1){
        inventory.RemoveItem(currentSlotIdx - 1, number);
        UpdateCurrentItem();
    }
    public ItemStack getCurrentItem(){
        return inventory.GetItem(currentSlotIdx - 1);
    }
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
                // Check if it is InteractableObject
                InteractableObject obj = hitObject.GetComponent<InteractableObject>();
                if (obj != null)
                {
                    obj.Interact(this);
                }
                else
                {
                    Debug.Log("No Interactable Object Found");
                }
            }
        }

        UpdateCurrentItem();
    }

    public void Use(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (heldItem.itemName == null)
            {
                //Empty hand action
            }
            else
            {
                heldItem.Use(this);
            }
        }
    }
    public void SelectSlot(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            currentSlotIdx = (int)context.ReadValue<System.Single>();
        }

        UpdateCurrentItem();
    }
    public void Scroll(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            currentSlotIdx += context.ReadValue<Vector2>().y > 0 ? 1 : -1;
            if (currentSlotIdx < 1)
            {
                currentSlotIdx = 1;
            }
            else if (currentSlotIdx > 9)
            {
                currentSlotIdx = 9;
            }
        }

        UpdateCurrentItem();
    }
    public void SetCurrentItemRotation(Vector3 eulers){
        if (heldItemModel != null){
            heldItemModel.transform.localRotation = Quaternion.Euler(eulers);
        }

    }

    public void ToggleInventory(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            UI.ToggleShowInventory();
            UpdateCurrentItem();
        }
    }
    public void UpdateCurrentItem()
    {
        // Update UI
        UI.SetHotbarActiveSlot(currentSlotIdx);
        UI.UpdateInventory();

        // Remove held item model
        Destroy(heldItemModel);

        // Show new held item
        ItemData itemData = inventory.GetItem(currentSlotIdx - 1).item;
        if (itemData.itemName == null){
            heldItem = itemData;
            return;
        }

        heldItem = itemData;

        GameObject itemPrefab = itemData.model;
        heldItemModel = Instantiate(itemPrefab, rightHand.transform.parent);
        //heldItemModel.transform.Translate(0, 1, 1);
        if (heldItemModel.GetComponent<Rigidbody>() != null)
            Destroy(heldItemModel.GetComponent<Rigidbody>());
        if (heldItemModel.GetComponent<Collider>() != null)
            heldItemModel.GetComponent<Collider>().enabled = false;
        if (heldItemModel.GetComponent<DroppedItem>() != null)
            heldItemModel.GetComponent<DroppedItem>().enabled = false;


        heldItemModel.transform.position = rightHand.transform.position;
        //Quaternion rotModifier = Quaternion.Euler(0,0,0);
        //SetCurrentItemRotation(new Vector3(gameObject.GetComponent<PlayerMovement>().vertLookAngle, 0, 0));
    }
}
