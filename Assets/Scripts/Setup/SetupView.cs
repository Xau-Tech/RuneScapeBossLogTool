using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//  View for the SetupMVC.cs class
public class SetupView : MonoBehaviour, IDisplayable<Setup>
{
    public RemoveSetupItemButton RemoveItemButton { get { return removeItemButton.GetComponent<RemoveSetupItemButton>(); } }

    [SerializeField] private PlayerView playerView;
    [SerializeField] private InfoView infoView;
    [SerializeField] private GameObject removeItemButton;
    [SerializeField] private InputField instanceCostInputField;
    [SerializeField] private InputField chargeDrainInputField;
    [SerializeField] private Dropdown cmbIntensityDropdown;

    //  Display all setup data to view
    public void Display(in Setup value)
    {
        Display(value.player);
        DisplayInstanceCost(value.InstanceCost);
        DisplayCombatIntensity(value.combatIntensity.IntensityLevel);
        DisplayChargeDrainRate(value.ChargeDrainRate);
    }

    //  Display all player data to view
    public void Display(in Player value)
    {
        playerView.Display(value);
    }

    //  Display SetupInfo
    public void DisplaySetupCost(in long totalCost)
    {
        infoView.DisplayCost(in totalCost);
    }

    private void DisplayInstanceCost(int instanceCost)
    {
        instanceCostInputField.text = instanceCost + "";
    }

    private void DisplayCombatIntensity(CombatIntensity.CombatIntensityLevels intensityLevel)
    {
        cmbIntensityDropdown.value = (int)intensityLevel;
    }

    private void DisplayChargeDrainRate(float chargeDrain)
    {
        chargeDrainInputField.text = chargeDrain + "";
    }
}
