using UnityEngine;
using UnityEngine.UI;

public class InfoView : MonoBehaviour
{
    [SerializeField] private Text totalCostText;
    [SerializeField] private Text equipmentCostText;
    [SerializeField] private Text inventoryCostText;
    [SerializeField] private Text prefightCostText;
    [SerializeField] private Text summoningCostText;

    public void DisplayCost(in long setupCost, int equipmentCost, int inventoryCost, int prefightCost, int summoningCost)
    {
        totalCostText.text = $"\tTotal cost: {setupCost.ToString("N0")} gp/hr";
        equipmentCostText.text = $"\t\tEquipment: {equipmentCost.ToString("N0")}";
        inventoryCostText.text = $"\t\tInventory: {inventoryCost.ToString("N0")}";
        prefightCostText.text = $"\t\tPrefight: {prefightCost.ToString("N0")}";
        summoningCostText.text = $"\t\tSummoning: {summoningCost.ToString("N0")}";
    }
}
