using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryGridManager : MonoBehaviour
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
    public Image[] gridSprites;

    void Start()
    {
        rTransform = GetComponent<RectTransform>();
        gridSprites = new Image[slotCountX * slotCountY];
        for (int i = 0; i < slotCountX; i++)
        {
            for (int j = 0; j < slotCountY; j++)
            {
                GameObject newSprite = Instantiate(PlayerUIController.emptySprite, rTransform);
                gridSprites[i + j * slotCountX] = newSprite.GetComponent<Image>();
                newSprite.GetComponent<RectTransform>().anchoredPosition = new Vector2(
                    rTransform.rect.xMin + ((float)i + 0.5f) / slotCountX * rTransform.rect.width,
                    rTransform.rect.yMin + ((float)j + 0.5f) / slotCountY * rTransform.rect.height);
                newSprite.GetComponent<RectTransform>().sizeDelta = new Vector2(
                    rTransform.rect.width / slotCountX,
                    rTransform.rect.height / slotCountY);
                newSprite.GetComponent<InventorySlotUIListener>().index = i + j * slotCountX;
                newSprite.GetComponent<InventorySlotUIListener>().inventorySlotGrid = this;
            }
        }
    }

    public void NotifyIsHovered(int index)
    {
        PlayerUIController.main.UpdateHoveredSlot(index, invType);
    }
}
