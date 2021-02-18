using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeastOfBurdenView : AbstractSetupItemColView
{
    [SerializeField] private List<AbsSetupItemSlotView> beastOfBurdenSlots = new List<AbsSetupItemSlotView>();

    public void Awake()
    {
        CollectionType = SetupCollections.BoB;

        for (int i = 0; i < beastOfBurdenSlots.Count; ++i)
        {
            beastOfBurdenSlots[i].Init(i);
        }
    }

    private void OnEnable()
    {
        EventManager.Instance.onBeastOfBurdenItemAdded += Display;
    }

    private void OnDisable()
    {
        EventManager.Instance.onBeastOfBurdenItemAdded -= Display;
    }

    public void Display(SetupItem setupItem, uint quantity, int slotID)
    {
        beastOfBurdenSlots[slotID].Display(in setupItem, quantity);
    }
}