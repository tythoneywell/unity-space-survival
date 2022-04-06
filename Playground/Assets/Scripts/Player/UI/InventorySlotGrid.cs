using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlotGrid : MonoBehaviour
{
    public int slotCountX;
    public int slotCountY;

    public enum InventoryType
    {
        Hotbar,
        Inventory,
        ExternalInventory,
        CraftingStation,
        LootableCorpse,
    }
    public InventoryType invType;

    RectTransform rTransform;
    public InventorySlotSingle[] gridSlots;

    void Start()
    {
        rTransform = GetComponent<RectTransform>();
        gridSlots = new InventorySlotSingle[slotCountX * slotCountY];
        for (int i = 0; i < slotCountX; i++)
        {
            for (int j = 0; j < slotCountY; j++)
            {
                int index = i + j * slotCountX;

                GameObject newSprite = Instantiate(PlayerUIController.emptySprite, rTransform);
                gridSlots[index] = newSprite.GetComponent<InventorySlotSingle>();
                newSprite.GetComponent<RectTransform>().anchoredPosition = new Vector2(
                    rTransform.rect.xMin + ((float)i + 0.5f) / slotCountX * rTransform.rect.width,
                    rTransform.rect.yMin + ((float)j + 0.5f) / slotCountY * rTransform.rect.height);
                newSprite.GetComponent<RectTransform>().sizeDelta = new Vector2(
                    rTransform.rect.width / slotCountX,
                    rTransform.rect.height / slotCountY);

                gridSlots[index].index = index;
                gridSlots[index].inventorySlotGrid = this;
            }
        }
    }

    public void NotifyIsHovered(int index)
    {
        PlayerUIController.main.UpdateHoveredSlot(index, invType);
    }
    public void NotifyIsUnhovered(int index)
    {
        PlayerUIController.main.ClearHoveredSlot(index, invType);
    }
}
