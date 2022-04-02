using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    Inventory inventory;
    int currentSlot = 1;

    GameObject rightHand;
    Quaternion rightHandRot;
    ItemData heldItem;
    GameObject heldItemModel;
    ItemRegistry registry;

    RectTransform activeSlot;
    GameObject UI;

    // Start is called before the first frame update
    void Start()
    {
        rightHand = GameObject.Find("RightArm");
        registry = GameObject.Find("ItemRegistry").GetComponent<ItemRegistry>();
        activeSlot = GameObject.Find("ActiveSlot").GetComponent<RectTransform>();
        UI = GameObject.Find("UI");

        rightHandRot = transform.rotation;

        inventory = new Inventory();
        heldItem = inventory.GetItem(0).item;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public bool AddToInventory(ItemStack stack)
    {
        return inventory.AddItemNoIndex(stack);
    }
    public void RemoveCurrentFromInv(int number = 1){
        inventory.RemoveItem(currentSlot - 1, number);
        UpdateCurrentItem();
    }
    public ItemStack getCurrentItem(){
        return inventory.GetItem(currentSlot - 1);
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
            currentSlot = (int)context.ReadValue<System.Single>();
        }

        UpdateCurrentItem();
    }
    public void Scroll(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            currentSlot += (int)(context.ReadValue<Vector2>().y * -0.005f);
            if (currentSlot < 1)
            {
                currentSlot = 1;
            }
            else if (currentSlot > 9)
            {
                currentSlot = 9;
            }
        }

        UpdateCurrentItem();
    }
    void UpdateCurrentItem()
    {
        // Update UI
        Vector3 slotPos = activeSlot.localPosition;
        slotPos.x = -220 + 55 * ((int)currentSlot - 1);
        activeSlot.localPosition = slotPos;

        // Remove held item model
        Destroy(heldItemModel);

        // Show new held item
        ItemData itemData = inventory.GetItem(currentSlot - 1).item;
        if (itemData.itemName == null){
            heldItem = itemData;
            return; 
        } 

        heldItem = itemData;

        GameObject itemPrefab = itemData.model;
        heldItemModel = Instantiate(itemPrefab, transform);
        heldItemModel.transform.Translate(0, 1, 1);
        if (heldItemModel.GetComponent<Rigidbody>() != null)
            Destroy(heldItemModel.GetComponent<Rigidbody>());
        if (heldItemModel.GetComponent<Collider>() != null)
            heldItemModel.GetComponent<Collider>().enabled = false;
        if (heldItemModel.GetComponent<DroppedItem>() != null)
            heldItemModel.GetComponent<DroppedItem>().enabled = false;

        heldItemModel.transform.position = rightHand.transform.position;
        heldItemModel.transform.rotation = transform.rotation;

        }
}
