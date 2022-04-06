using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerUIController : MonoBehaviour
{
    public static PlayerUIController main;

    public static bool invShown;

    public GameObject playerHotbarObject;
    public GameObject playerInventoryObject;
    public GameObject externalInventoryObject;

    InventorySlotGrid playerHotbarSlots;
    InventorySlotGrid playerInventorySlots;
    InventorySlotGrid externalInventorySlots;

    public static GameObject emptySprite;
    // Hack to be able to set this in inspector
    public GameObject emptySpriteInspector;

    int hoveredSlotIdx = -1;

    RectTransform activeSlotRect;
    InventorySlotSingle invCursorStack;
    RectTransform invCursorRect;

    void Awake()
    {
        main = this;
        emptySprite = emptySpriteInspector;

        playerHotbarSlots = playerHotbarObject.GetComponentInChildren<InventorySlotGrid>();
        playerInventorySlots = playerInventoryObject.GetComponentInChildren<InventorySlotGrid>();
        externalInventorySlots = externalInventoryObject.GetComponentInChildren<InventorySlotGrid>();
    }

    void Start()
    {
        activeSlotRect = GameObject.Find("ActiveSlot").GetComponent<RectTransform>();

        invCursorStack = Instantiate(emptySprite, transform).GetComponent<InventorySlotSingle>();
        invCursorRect = invCursorStack.gameObject.GetComponent<RectTransform>();
        invCursorRect.pivot = new Vector2(-0.08f, 1.08f);
        invCursorRect.sizeDelta = new Vector2(60, 60);
        invCursorStack.gameObject.GetComponent<Image>().raycastTarget = false;

        StartCoroutine(DelayedUpdateInventory());
    }

    public void Click(InputAction.CallbackContext context)
    {
        if (hoveredSlotIdx == -1) return;

        if (context.performed && context.ReadValue<float>() == 1)
        {
            PlayerInteraction.main.inventory.CursorInteractWith(hoveredSlotIdx);
        }
        if (context.performed && context.ReadValue<float>() == 0)
        {
            // Something on mouse up
        }

        UpdateInventory();
    }
    public void MiddleClick(InputAction.CallbackContext context)
    {
        if (hoveredSlotIdx == -1) return;

        if (context.performed && context.ReadValue<float>() == 1)
        {
            ItemStack toDuplicate = PlayerInteraction.main.inventory.GetItem(hoveredSlotIdx);
            PlayerInteraction.main.inventory.cursorStack = new ItemStack(toDuplicate);
        }
        if (context.performed && context.ReadValue<float>() == 0)
        {
            // Something on mouse up
        }

        UpdateInventory();
    }
    public void RightClick(InputAction.CallbackContext context)
    {
        if (hoveredSlotIdx == -1) return;

        if (context.performed && context.ReadValue<float>() == 1)
        {
            PlayerInteraction.main.inventory.CursorAlternateInteractWith(hoveredSlotIdx);
        }
        if (context.performed && context.ReadValue<float>() == 0)
        {
            // Something on mouse up
        }

        UpdateInventory();
    }
    public void MouseMove(InputAction.CallbackContext context)
    {
        invCursorRect.position = context.ReadValue<Vector2>();
    }

    public void UpdateHoveredSlot(int index, InventorySlotGrid.InventoryType type)
    {
        hoveredSlotIdx = index;
        hoveredSlotIdx += type == InventorySlotGrid.InventoryType.Inventory ? 9 : 0;
        //Debug.Log("hovered " + hoveredSlotIdx);
    }
    public void ClearHoveredSlot(int index, InventorySlotGrid.InventoryType type)
    {
        int convertedIdx = index + (type == InventorySlotGrid.InventoryType.Inventory ? 9 : 0);
        if (convertedIdx == hoveredSlotIdx) hoveredSlotIdx = -1;
        //Debug.Log("unhovered " + convertedIdx);
    }

    public void ToggleShowInventory()
    {
        if (invShown)
        {
            HideInventory();
            invShown = false;
        }
        else
        {
            ShowInventory();
            invShown = true;
        }
    }
    public void ShowInventory()
    {
        playerInventoryObject.SetActive(true);
        invCursorStack.gameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        UpdateInventory();
    }
    public void HideInventory()
    {
        playerInventoryObject.SetActive(false);
        invCursorStack.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void SetHotbarActiveSlot(int index)
    {
        Vector3 slotPos = activeSlotRect.localPosition;
        slotPos.x = -220 + 55 * ((int)index - 1);
        activeSlotRect.localPosition = slotPos;

    }

    public void UpdateInventory()
    {
        // Update hotbar
        for (int i = 0; i < 9; i++)
        {
            ItemStack stack = PlayerInteraction.main.inventory.GetItem(i);
            playerHotbarSlots.gridSlots[i].ShowStack(stack);
        }
        // Update inventory
        for (int i = 0; i < 27; i++)
        {
            ItemStack stack = PlayerInteraction.main.inventory.GetItem(i + 9);
            playerInventorySlots.gridSlots[i].ShowStack(stack);
        }
        // Update cursor
        invCursorStack.ShowStack(PlayerInteraction.main.inventory.cursorStack);
    }

    IEnumerator DelayedUpdateInventory()
    {
        yield return null;
        UpdateInventory();
        HideInventory();
    }
}
