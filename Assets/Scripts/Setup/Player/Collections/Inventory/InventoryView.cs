using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryView : AbsSetupItemCollView
{
    //  Properties & fields

    [SerializeField] private List<InventorySlotView> _inventorySlots = new List<InventorySlotView>();

    //  Monobehaviors

    private void Awake()
    {
        base.CollectionType = Enums.SetupCollections.Inventory;

        for(int i = 0; i < _inventorySlots.Count; ++i)
        {
            _inventorySlots[i].Init(i);
        }
    }

    //  Methods

    public void Display(SetupItem setupItem, uint quantity, int slotId)
    {
        _inventorySlots[slotId].Display(setupItem, quantity);
    }
}
