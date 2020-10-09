using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//  View for the armor/weapons of the setup
public class EquipmentView : MonoBehaviour
{
    [SerializeField] private List<EquipmentSlotView> equipmentSlots = new List<EquipmentSlotView>();

    public void Awake()
    {
        for (int i = 0; i < equipmentSlots.Count; ++i)
        {
            equipmentSlots[i].Init(i);
        }
    }

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }

    public void Display(SetupItem setupItem, int slotID)
    {
        
    }
}
