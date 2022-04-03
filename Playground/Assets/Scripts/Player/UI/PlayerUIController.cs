using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerUIController : MonoBehaviour
{
    public static PlayerUIController main;

    public GameObject playerHotbarObject;
    public GameObject playerInventoryObject;
    public GameObject externalInventoryObject;

    InventoryGridManager playerHotbarSlots;
    InventoryGridManager playerInventorySlots;
    InventoryGridManager externalInventorySlots;

    public static GameObject emptySprite;
    // Hack to be able to set this in inspector
    public GameObject emptySpriteInspector;

    int hoveredSlotIdx;

    RectTransform activeSlotPosition;
    bool invShown;

    void Awake()
    {
        main = this;
        emptySprite = emptySpriteInspector;

        playerHotbarSlots = playerHotbarObject.GetComponentInChildren<InventoryGridManager>();
        playerInventorySlots = playerInventoryObject.GetComponentInChildren<InventoryGridManager>();
        externalInventorySlots = externalInventoryObject.GetComponentInChildren<InventoryGridManager>();
    }

    void Start()
    {
        activeSlotPosition = GameObject.Find("ActiveSlot").GetComponent<RectTransform>();
        StartCoroutine(DelayedUpdateInventory());
    }

    public void Click(InputAction.CallbackContext context)
    {
        if (context.performed && context.ReadValue<float>() == 1)
        {
            PlayerInteraction.main.inventory.CursorInteractWith(hoveredSlotIdx);
            Debug.Log(PlayerInteraction.main.inventory.cursor.item.itemName);
        }
        if (context.performed && context.ReadValue<float>() == 0)
        {
            // Something on mouse up
        }


        UpdateInventory();
    }
    public void UpdateHoveredSlot(int index, InventoryGridManager.InventoryType type)
    {
        hoveredSlotIdx = index;
        hoveredSlotIdx += type == InventoryGridManager.InventoryType.Inventory ? 9 : 0;
    }

    public void ToggleShowInventory()
    {
        if (invShown)
        {
            ShowInventory();
            invShown = false;
        }
        else
        {
            HideInventory();
            invShown = true;
        }
    }
    public void ShowInventory()
    {
        playerInventoryObject.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        UpdateInventory();
    }
    public void HideInventory()
    {
        playerInventoryObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void SetHotbarActiveSlot(int index)
    {
        Vector3 slotPos = activeSlotPosition.localPosition;
        slotPos.x = -220 + 55 * ((int)index - 1);
        activeSlotPosition.localPosition = slotPos;

    }

    public void UpdateInventory()
    {
        // Update hotbar
        for (int i = 0; i < 9; i++)
        {
            ItemStack stack = PlayerInteraction.main.inventory.GetItem(i);
            if (stack.item.itemName != null)
            {
                playerHotbarSlots.gridSprites[i].sprite = stack.item.sprite;
            }
            else
            {
                playerHotbarSlots.gridSprites[i].sprite = emptySprite.GetComponent<Image>().sprite;
            }
        }
        // Update inventory
        for (int i = 0; i < 27; i++)
        {
            ItemStack stack = PlayerInteraction.main.inventory.GetItem(i + 9);
            if (stack.item.itemName != null)
            {
                playerInventorySlots.gridSprites[i].sprite = stack.item.sprite;
            }
            else
            {
                playerInventorySlots.gridSprites[i].sprite = emptySprite.GetComponent<Image>().sprite;
            }
        }
    }

    IEnumerator DelayedUpdateInventory()
    {
        yield return null;
        UpdateInventory();
    }
}
