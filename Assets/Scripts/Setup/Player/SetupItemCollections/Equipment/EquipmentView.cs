using System.Collections.Generic;
using UnityEngine;

//  View for the armor/weapons of the setup
public class EquipmentView : AbstractSetupItemColView
{
    [SerializeField] private List<EquipmentSlotView> equipmentSlots = new List<EquipmentSlotView>();

    public void Awake()
    {
        CollectionType = SetupCollections.Equipment;

        for (int i = 0; i < equipmentSlots.Count; ++i)
        {
            equipmentSlots[i].Init(i);
        }
    }

    private void OnEnable()
    {
        EventManager.Instance.onEquipmentAdded += Display;
    }

    private void OnDisable()
    {
        EventManager.Instance.onEquipmentAdded -= Display;
    }

    public void Display(SetupItem setupItem, int slotID)
    {
        equipmentSlots[slotID].Display(in setupItem, 1);
    }
}