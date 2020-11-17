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

    private void OnEnable()
    {
        EventManager.Instance.onInventoryItemAdded += Display;
    }

    private void OnDisable()
    {
        EventManager.Instance.onInventoryItemAdded -= Display;
    }



    public void Display(SetupItem setupItem, int slotID)
    {
        inventorySlots[slotID].Display(in setupItem);
    }

    public void Display(in Inventory value)
    {
        
    }
}
