using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerUIController : MonoBehaviour
{
    public static PlayerUIController main;

    public static bool invShown;
    public static bool saveShown;

    public GameObject playerHotbarObject;
    public GameObject playerInventoryObject;
    public GameObject externalInventoryObject;

    public GameObject playerSaveMenu;

    public GameObject craftingMenuObject;
    public GameObject buildMenuObject;
    public GameObject farmMenuObject;
    public GameObject forgeMenuObject;
    public GameObject generatorMenuObject;
    public GameObject repairMenuObject;

    public GameObject tooltipObject;
    public GameObject tabsObject;

    InventorySlotGrid playerHotbarSlots;
    InventorySlotGrid playerInventorySlots;

    public static GameObject emptySprite;
    // Hack to be able to set this in inspector
    public GameObject emptySpriteInspector;

    UISlot hoveredSlot;

    RectTransform activeSlotRect;
    UISlot invCursorStack;
    RectTransform invCursorRect;
    Text tooltipText;
    RectTransform tooltipRect;

    void Awake()
    {
        main = this;
        emptySprite = emptySpriteInspector;

        playerHotbarSlots = playerHotbarObject.GetComponentInChildren<InventorySlotGrid>();
        playerInventorySlots = playerInventoryObject.GetComponentInChildren<InventorySlotGrid>();

        tooltipText = tooltipObject.GetComponent<Text>();
        tooltipRect = tooltipObject.GetComponent<RectTransform>();
    }

    void Start()
    {
        activeSlotRect = GameObject.Find("ActiveSlot").GetComponent<RectTransform>();

        invCursorStack = Instantiate(emptySprite, transform).AddComponent<UISlot>();
        invCursorRect = invCursorStack.gameObject.GetComponent<RectTransform>();
        invCursorRect.pivot = new Vector2(-0.08f, 1.08f);
        invCursorRect.sizeDelta = new Vector2(40, 40);
        invCursorStack.gameObject.GetComponent<Image>().raycastTarget = false;

        StartCoroutine(DelayedUpdateInventory());
    }
    public void ToggleShowInventory()
    {
        if (invShown)
        {
            HideInventory();
        }
        else
        {
            ShowInventory();
        }
    }
    public void OpenCraftingMenu()
    {
        craftingMenuObject.SetActive(true);
        ShowInventory();
    }
    public void OpenBuildMenu()
    {
        HideMenus();
        buildMenuObject.SetActive(true);
        ShowInventory();
    }
    public void OpenFarmMenu()
    {
        HideMenus();
        farmMenuObject.SetActive(true);
        tabsObject.SetActive(true);
        ShowInventory();
    }
    public void OpenForgeMenu()
    {
        HideMenus();
        forgeMenuObject.SetActive(true);
        tabsObject.SetActive(true);
        ShowInventory();
    }
    public void OpenGeneratorMenu()
    {
        HideMenus();
        generatorMenuObject.SetActive(true);
        tabsObject.SetActive(true);
        ShowInventory();
    }
    public void OpenRepairMenu()
    {
        repairMenuObject.SetActive(true);
        repairMenuObject.GetComponentInChildren<RecipeIngredientsGrid>().ShowIngredients(RepairableObject.curr.repairCost.ingredients);
        repairMenuObject.GetComponentInChildren<Text>().text = RepairableObject.curr.repairCost.recipeName;
        ShowInventory();
    }
    public void HideMenus()
    {
        craftingMenuObject.SetActive(false);
        buildMenuObject.SetActive(false);
        farmMenuObject.SetActive(false);
        forgeMenuObject.SetActive(false);
        repairMenuObject.SetActive(false);
        generatorMenuObject.SetActive(false);
    }

    public void Click(InputAction.CallbackContext context)
    {
        if (hoveredSlot == null) return;

        if (context.performed && context.ReadValue<float>() == 1)
        {
            hoveredSlot.PrimaryActivate();
        }
        if (context.performed && context.ReadValue<float>() == 0)
        {
            // Something on mouse up
        }

        UpdateInventory();
    }
    public void RightClick(InputAction.CallbackContext context)
    {
        if (hoveredSlot == null) return;

        if (context.performed && context.ReadValue<float>() == 1)
        {
            hoveredSlot.SecondaryActivate();
        }
        if (context.performed && context.ReadValue<float>() == 0)
        {
            // Something on mouse up
        }

        UpdateInventory();
    }
    public void MiddleClick(InputAction.CallbackContext context)
    {
        if (hoveredSlot == null) return;

        if (context.performed && context.ReadValue<float>() == 1)
        {
            hoveredSlot.TertiaryActivate();
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
        tooltipRect.position = context.ReadValue<Vector2>();
    }

    public void UpdateHoveredSlot(UISlot slot)
    {
        hoveredSlot = slot;
        tooltipText.text = hoveredSlot.descriptionText;
        //Debug.Log("hovered " + slot.index);
    }
    public void ClearHoveredSlot(UISlot slot)
    {
        if (hoveredSlot == slot)
        {
            hoveredSlot = null;
            tooltipText.text = "";
        }
        //Debug.Log("unhovered " + slot.index);
    }

    public void ToggleShowSaveMenu()
    {
        if (saveShown)
        {
            HideSaveMenu();
            saveShown = false;
        }
        else
        {
            ShowSaveMenu();
            saveShown = true;
        }
    }
    public void ShowSaveMenu()
    {
        playerSaveMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;

    }
    public void HideSaveMenu()
    {
        playerSaveMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;

    }

    public void ShowInventory()
    {
        playerInventoryObject.SetActive(true);
        invCursorStack.gameObject.SetActive(true);
        tooltipObject.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        invShown = true;
        UpdateInventory();
    }
    public void HideInventory()
    {
        playerInventoryObject.SetActive(false);
        invCursorStack.gameObject.SetActive(false);
        craftingMenuObject.SetActive(false);
        buildMenuObject.SetActive(false);
        tooltipObject.SetActive(false);
        farmMenuObject.SetActive(false);
        forgeMenuObject.SetActive(false);
        generatorMenuObject.SetActive(false);
        repairMenuObject.SetActive(false);
        tabsObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        invShown = false;
    }

    public void SetHotbarActiveSlot(int index)
    {
        RectTransform hotbarRect = playerHotbarObject.GetComponent<RectTransform>();
        Vector3 slotPos = activeSlotRect.localPosition;
        slotPos.x = hotbarRect.rect.xMin + (hotbarRect.sizeDelta.x / 9) * ((float)index - 0.5f);
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
