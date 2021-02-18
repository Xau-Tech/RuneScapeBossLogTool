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
        equipmentCostText.text = $"\t\tEquipment cost: {equipmentCost.ToString("N0")} gp/hr";
        inventoryCostText.text = $"\t\tInventory cost: {inventoryCost.ToString("N0")} gp/hr";
        prefightCostText.text = $"\t\tPrefight cost: {prefightCost.ToString("N0")} gp/hr";
        summoningCostText.text = $"\t\tSummoning cost: {summoningCost.ToString("N0")} gp/hr";
    }
}
