using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UISlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public int index;

    public UISlotGrid parentGrid;

    protected bool hovered = false;

    protected Image itemSpriteDisplay;
    protected Text itemCount;
    public string primaryText;
    public string descriptionText;

    void Awake()
    {
        itemSpriteDisplay = gameObject.GetComponent<Image>();
        itemCount = gameObject.GetComponentInChildren<Text>();
    }

    public virtual void PrimaryActivate() { }
    public virtual void SecondaryActivate() { }
    public virtual void TertiaryActivate() { }

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        hovered = true;
        PlayerUIController.main.UpdateHoveredSlot(this);
        this.GetComponent<Image>().color = Color.white * 0.8f;
    }
    public virtual void OnPointerExit(PointerEventData eventData)
    {
        hovered = false;
        PlayerUIController.main.ClearHoveredSlot(this);
        this.GetComponent<Image>().color = Color.white;
    }

    public void ShowStack(ItemStack stack)
    {
        if (stack.item.itemName == null)
        {
            itemSpriteDisplay.sprite = PlayerUIController.emptySprite.GetComponent<Image>().sprite;
            itemCount.text = "";
            primaryText = "";
            descriptionText = "";
        }
        else
        {
            itemSpriteDisplay.sprite = stack.item.sprite;
            itemCount.text = stack.count.ToString();
            primaryText = stack.item.displayName;
            descriptionText = stack.item.itemDescription;
        }
    }
    public void ShowSprite(Sprite sprite)
    {
        if (sprite == null)
        {
            itemSpriteDisplay.sprite = PlayerUIController.emptySprite.GetComponent<Image>().sprite;
        }
        else
        {
            itemSpriteDisplay.sprite = sprite;
        }
        itemCount.text = "";
        primaryText = "";
    }
    public void ShowSpriteWithTooltip(Sprite sprite, string tooltipText, string descriptionText = "")
    {
        ShowSprite(sprite);
        primaryText = tooltipText;
        this.descriptionText = descriptionText;
    }

    void OnDisable()
    {
        if (hovered)
        {
            hovered = false;
            PlayerUIController.main.ClearHoveredSlot(this);
            this.GetComponent<Image>().color = Color.white;
        }
    }
}
