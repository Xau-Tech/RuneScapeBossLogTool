using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// View for Info area of the Setup tab
/// </summary>
public class InfoView : MonoBehaviour
{
    //  Properties & fields

    [SerializeField] private Text _totalCostText;
    [SerializeField] private Text _equipmentCostText;
    [SerializeField] private Text _inventoryCostText;
    [SerializeField] private Text _prefightCostText;
    [SerializeField] private Text _summoningCostText;

    public void Display(long setupCost, int equipmentCost, int inventoryCost, int prefightCost, int summoningCost)
    {
        _totalCostText.text = $"\tTotal cost: {setupCost.ToString("N0")} gp/hr";
        _equipmentCostText.text = $"\t\tEquipment cost: {equipmentCost.ToString("N0")} gp/hr";
        _inventoryCostText.text = $"\t\tInventory cost: {inventoryCost.ToString("N0")} gp/hr";
        _prefightCostText.text = $"\t\tPrefight cost: {prefightCost.ToString("N0")} gp/hr";
        _summoningCostText.text = $"\t\tSummoning cost: {summoningCost.ToString("N0")} gp/hr";
    }
}
