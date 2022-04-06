using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlotSingle : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public InventorySlotGrid inventorySlotGrid;

    public int index;
    
    bool hovered = false;

    Image itemSpriteDisplay;
    Text itemCount;

    void Start()
    {
        itemSpriteDisplay = gameObject.GetComponent<Image>();
        itemCount = gameObject.GetComponentInChildren<Text>();
    }
    public void ShowStack(ItemStack stack)
    {
        if (stack.item.itemName == null)
        {
            itemSpriteDisplay.sprite = PlayerUIController.emptySprite.GetComponent<Image>().sprite;
            itemCount.text = "";
        }
        else
        {
            itemSpriteDisplay.sprite = stack.item.sprite;
            itemCount.text = stack.count.ToString();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        hovered = true;
        inventorySlotGrid.NotifyIsHovered(index);
        this.GetComponent<Image>().color = Color.white * 0.8f;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        hovered = false;
        inventorySlotGrid.NotifyIsUnhovered(index);
        this.GetComponent<Image>().color = Color.white;
    }
    void OnDisable()
    {
        if (hovered)
        {
            hovered = false;
            inventorySlotGrid.NotifyIsUnhovered(index);
            this.GetComponent<Image>().color = Color.white;
        }
    }
}
