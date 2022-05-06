using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UISlotGrid : MonoBehaviour
{
    public int slotCountX;
    public int slotCountY;

    protected RectTransform rTransform;
    public UISlot[] gridSlots;

    public GameObject emptySlot;

    protected void Awake()
    {
        rTransform = GetComponent<RectTransform>();
        gridSlots = new UISlot[slotCountX * slotCountY];
        for (int i = 0; i < slotCountX; i++)
        {
            for (int j = 0; j < slotCountY; j++)
            {
                int index = i + j * slotCountX;

                GameObject newSlot = MakeSlot();

                newSlot.GetComponent<RectTransform>().anchoredPosition = new Vector2(
                    rTransform.rect.xMin + ((float)i + 0.5f) / slotCountX * rTransform.rect.width,
                    rTransform.rect.yMin + ((float)j + 0.5f) / slotCountY * rTransform.rect.height);
                newSlot.GetComponent<RectTransform>().sizeDelta = new Vector2(
                    rTransform.rect.width / slotCountX,
                    rTransform.rect.height / slotCountY);

                gridSlots[index] = newSlot.GetComponent<UISlot>();

                gridSlots[index].index = index;
                gridSlots[index].parentGrid = this;
            }
        }
    }
    protected void Start()
    {
    }
    protected virtual GameObject MakeSlot()
    {
        GameObject newSlot = Instantiate(emptySlot, rTransform);
        newSlot.AddComponent<UISlot>();

        return newSlot;
    }
}
