using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentView : AbsSetupItemCollView
{
    //  Properties & fields

    [SerializeField] private List<EquipmentSlotView> _slots = new List<EquipmentSlotView>();

    //  Monobehaviors

    private void Awake()
    {
        base.CollectionType = Enums.SetupCollections.Equipment;

        for(int i = 0; i < _slots.Count; ++i)
        {
            _slots[i].Init(i);
        }
    }

    //  Methods

    public void Display(SetupItem setupItem, int slotId)
    {
        _slots[slotId].Display(setupItem, 1);
    }
}
