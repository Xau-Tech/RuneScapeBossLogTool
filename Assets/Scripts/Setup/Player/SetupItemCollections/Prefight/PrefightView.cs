using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  View class for Prefight setupitem collection
public class PrefightView : AbstractSetupItemColView
{
    [SerializeField] private List<AbsSetupItemSlotView> prefightSlots = new List<AbsSetupItemSlotView>();

    public void Awake()
    {
        CollectionType = SetupCollections.Prefight;

        for (int i = 0; i < prefightSlots.Count; ++i)
        {
            prefightSlots[i].Init(i);
        }
    }

    private void OnEnable()
    {
        EventManager.Instance.onPrefightItemAdded += Display;
    }

    private void OnDisable()
    {
        EventManager.Instance.onPrefightItemAdded -= Display;
    }

    public void Display(SetupItem setupItem, uint quantity, int slotID)
    {
        prefightSlots[slotID].Display(in setupItem, quantity);
    }
}
