using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerView : MonoBehaviour
{
    //  Properties & fields

    [SerializeField] private SkillsView _skillsView;
    [SerializeField] private InputField _usernameInputField;
    [SerializeField] private InventoryView _inventoryView;
    [SerializeField] private EquipmentView _equipmentView; 
    [SerializeField] private BeastOfBurdenView _bobView;
    [SerializeField] private PrefightView _prefightView;

    //  Methods

    public void Display(Player player)
    {
        _usernameInputField.text = player.Username;
        _skillsView.Display(player.Skills);
    }

    public void Display(SetupItem setupItem, uint quantity, int slotId, Enums.SetupCollections collection)
    {
        switch (collection)
        {
            case Enums.SetupCollections.Inventory:
                _inventoryView.Display(setupItem, quantity, slotId);
                break;
            case Enums.SetupCollections.Equipment:
                _equipmentView.Display(setupItem, slotId);
                break;
            case Enums.SetupCollections.Prefight:
                _prefightView.Display(setupItem, quantity, slotId);
                break;
            case Enums.SetupCollections.BoB:
                _bobView.Display(setupItem, quantity, slotId);
                break;
            default:
                break;
        }
    }
}
