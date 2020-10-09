using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoView : MonoBehaviour
{
    [SerializeField] private Text totalCostText;

    private void Awake()
    {
        if (!totalCostText)
            throw new System.Exception($"TotalCostText has not been set in the inspector!");
    }

    public void DisplayCost(in long setupCost)
    {
        totalCostText.text = $"\tTotal cost: {setupCost.ToString("N0")} gp/hr";
    }
}
