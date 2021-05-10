using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//  Buttons to swap between showing the areas showing setup costs and hit chance calculator
public class CostHitChanceButtonSwap : MonoBehaviour
{
    [SerializeField] private GameObject costDisplayArea;
    [SerializeField] private GameObject hitChanceDisplayArea;
    [SerializeField] private Button costButton;
    [SerializeField] private Button hitChanceButton;

    private void Awake()
    {
        EventManager.Instance.onDataLoaded += DisplayCostArea;

        costButton.onClick.AddListener(DisplayCostArea);
        hitChanceButton.onClick.AddListener(DisplayHitChanceArea);
    }

    public void DisplayCostArea()
    {
        hitChanceDisplayArea.SetActive(false);
        costDisplayArea.SetActive(true);
    }

    private void DisplayHitChanceArea()
    {
        hitChanceDisplayArea.SetActive(true);
        costDisplayArea.SetActive(false);
    }
}
