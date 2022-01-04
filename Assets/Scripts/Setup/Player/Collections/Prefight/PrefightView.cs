using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefightView : AbsSetupItemCollView
{
    //  Properties & fields

    [SerializeField] private List<AbsSetupItemSlotView> _slots = new List<AbsSetupItemSlotView>();

    //  Monobehaviors

    private void Awake()
    {
        base.CollectionType = Enums.SetupCollections.Prefight;

        for(int i = 0; i < _slots.Count; ++i)
        {
            _slots[i].Init(i);
        }
    }

    //  Methods

    public void Display(SetupItem setupItem, uint quantity, int slotId)
    {
        _slots[slotId].Display(setupItem, quantity);
    }
}
