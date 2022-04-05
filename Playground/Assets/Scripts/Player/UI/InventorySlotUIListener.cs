using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlotUIListener : MonoBehaviour, IPointerEnterHandler
{
    public InventoryGridManager inventorySlotGrid;

    public int index;

    public void OnPointerEnter(PointerEventData eventData)
    {
        inventorySlotGrid.NotifyIsHovered(index);
    }
}
