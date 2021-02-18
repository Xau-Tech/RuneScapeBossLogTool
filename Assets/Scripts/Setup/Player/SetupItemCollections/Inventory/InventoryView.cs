using System.Collections.Generic;
using UnityEngine;

public class InventoryView : AbstractSetupItemColView
{
    [SerializeField] private List<InventorySlotView> inventorySlots = new List<InventorySlotView>();

    public void Awake()
    {
        CollectionType = SetupCollections.Inventory;

        for(int i = 0; i < inventorySlots.Count; ++i)
        {
            inventorySlots[i].Init(i);
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

    public void Display(SetupItem setupItem, uint quantity, int slotID)
    {
        inventorySlots[slotID].Display(in setupItem, quantity);
    }
}
