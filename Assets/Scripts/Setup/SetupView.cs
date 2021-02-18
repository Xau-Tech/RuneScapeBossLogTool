using UnityEngine;
using UnityEngine.UI;

//  View for the SetupMVC.cs class
public class SetupView : MonoBehaviour, IDisplayable<Setup>
{
    public RemoveSetupItemButton RemoveItemButton { get { return removeItemButton.GetComponent<RemoveSetupItemButton>(); } }

    [SerializeField] private PlayerView playerView;
    [SerializeField] private InfoView infoView;
    [SerializeField] private InvFamiliarSwapButton invFamiliarSwapScript;
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
    public void DisplaySetupCost(in long totalCost, int equipmentCost, int inventoryCost, int prefightCost, int summoningCost)
    {
        infoView.DisplayCost(in totalCost, equipmentCost, inventoryCost, prefightCost, summoningCost);
    }

    private void DisplayInstanceCost(int instanceCost)
    {
        instanceCostInputField.text = instanceCost + "";
    }

    private void DisplayCombatIntensity(CombatIntensity.CombatIntensityLevels intensityLevel)
    {
        cmbIntensityDropdown.SetValueWithoutNotify((int)intensityLevel);
    }

    private void DisplayChargeDrainRate(float chargeDrain)
    {
        chargeDrainInputField.text = chargeDrain + "";
    }

    public void ShowInventoryAndBeastOfBurden()
    {
        invFamiliarSwapScript.ShowBoth();
    }
}
