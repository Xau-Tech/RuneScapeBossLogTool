using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//  Script to swap between showing the Inventory and Summoning setupitem collections
public class SetupControls : MonoBehaviour
{
    public static SetupControls Instance;

    [SerializeField] private Button inventoryButton;
    [SerializeField] private Button familiarButton;
    [SerializeField] private GameObject inventoryArea;
    [SerializeField] private GameObject familiarArea;
    [SerializeField] private InputField withdrawAmountIF;
    [SerializeField] private Toggle notedToggle;
    private Color selectedColor;
    private static SetupControls _instance = new SetupControls();

    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

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

    public uint AmountToWithdraw()
    {
        return uint.Parse(withdrawAmountIF.text);
    }

    public bool WithdrawNotes()
    {
        return notedToggle.isOn;
    }
}
