using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetupView : MonoBehaviour
{
    //  Properties & fields

    [SerializeField] private GameObject _setupItemMenuController;
    [SerializeField] private PlayerView _playerView;
    [SerializeField] private InfoView _infoView;
    [SerializeField] private InputField _instanceCostInputField;
    [SerializeField] private InputField _chargeDrainInputField;
    [SerializeField] private Dropdown _cmbIntensityDropdown;

    //  Methods

    public void Display(Setup setup)
    {
        Display(setup.Player);
        Display(setup.InstanceCost);
        Display(setup.CombatIntensity.IntensityLevel);
        Display(setup.ChargeDrainRate);
    }

    public void Display(Player player)
    {
        _playerView.Display(player);
    }

    public void Display(SetupItem setupItem, uint quantity, int slotId, Enums.SetupCollections collection)
    {
        _playerView.Display(setupItem, quantity, slotId, collection);
    }

    public void Display(long totalCost, int equipmentCost, int inventoryCost, int prefightCost, int summoningCost)
    {
        _infoView.Display(totalCost, equipmentCost, inventoryCost, prefightCost, summoningCost);
    }

    private void Display(int instanceCost)
    {
        _instanceCostInputField.text = instanceCost + "";
    }

    private void Display(Enums.CombatIntensityLevels intensity)
    {
        _cmbIntensityDropdown.SetValueWithoutNotify((int)intensity);
    }

    private void Display(float chargeDrain)
    {
        _chargeDrainInputField.text = chargeDrain + "";
    }
}
