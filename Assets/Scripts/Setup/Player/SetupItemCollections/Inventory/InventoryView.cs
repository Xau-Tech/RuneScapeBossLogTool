using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryView : MonoBehaviour, IDisplayable<Inventory>
{
    [SerializeField] private List<InventorySlotView> inventorySlots = new List<InventorySlotView>();

    public void Awake()
    {
        for(int i = 0; i < inventorySlots.Count; ++i)
        {
            inventorySlots[i].Init(in i);
        }
    }

    public void Display(in Inventory value)
    {
        throw new System.NotImplementedException();
    }

    public void Display(in ItemSlot itemSlot, in int slotID)
    {
        inventorySlots[slotID].Display(in itemSlot);
    }
}
