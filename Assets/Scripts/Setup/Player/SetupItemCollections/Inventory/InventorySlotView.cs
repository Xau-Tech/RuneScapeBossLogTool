using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlotView : AbsSetupItemSlotView, IDisplayable<ItemSlot>, IPointerClickHandler
{
    public int inventorySlotNumber { private get; set; }

    public void Init(in int inventorySlotNumber)
    {
        slotCategory = ItemSlotCategories.Inventory;
        item = null;
        this.inventorySlotNumber = inventorySlotNumber;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        base.OnClick(in eventData, inventorySlotNumber);
    }

    public void Display(in ItemSlot value)
    {
        throw new System.NotImplementedException();
    }
}
