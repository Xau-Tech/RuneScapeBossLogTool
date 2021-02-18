using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//  Script to swap between showing the Inventory and Summoning setupitem collections
public class InvFamiliarSwapButton : MonoBehaviour
{
    [SerializeField] private Button inventoryButton;
    [SerializeField] private Button familiarButton;
    [SerializeField] private GameObject inventoryArea;
    [SerializeField] private GameObject familiarArea;
    private Color selectedColor;

    private void Awake()
    {
        selectedColor = inventoryButton.image.color;
        inventoryButton.onClick.AddListener(ShowInventory);
        familiarButton.onClick.AddListener(ShowFamiliar);
    }

    private void OnEnable()
    {
        EventManager.Instance.onSetupUIFilled += ShowInventory;
    }

    private void OnDisable()
    {
        EventManager.Instance.onSetupUIFilled -= ShowInventory;
    }

    private void ShowInventory()
    {
        familiarButton.image.color = Color.white;
        familiarArea.SetActive(false);

        inventoryButton.image.color = selectedColor;
        inventoryArea.SetActive(true);
    }

    private void ShowFamiliar()
    {
        inventoryButton.image.color = Color.white;
        inventoryArea.SetActive(false);

        familiarButton.image.color = selectedColor;
        familiarArea.SetActive(true);
    }

    public void ShowBoth()
    {
        inventoryArea.SetActive(true);
        familiarArea.SetActive(true);
    }
}
