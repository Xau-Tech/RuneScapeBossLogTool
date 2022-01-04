using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// View for the controls area of the Setup tab that includes swapping from inventory to beast of burden and withdraw options
/// </summary>
public class ControlsView : MonoBehaviour
{
    //  Properties & fields
    
    public static ControlsView Instance { get { return _instance; } }

    [SerializeField] private Button _inventoryButton;
    [SerializeField] private Button _familiarButton;
    [SerializeField] private GameObject _inventoryArea;
    [SerializeField] private GameObject _familiarArea;
    [SerializeField] private InputField _withdrawAmountInputField;
    [SerializeField] private Toggle _notedToggle;
    private Color _selectedColor;
    private static ControlsView _instance = null;

    //  Monobehaviors

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else if (_instance != this)
            Destroy(gameObject);

        _selectedColor = _inventoryButton.image.color;
        _inventoryButton.onClick.AddListener(ShowInventory);
        _familiarButton.onClick.AddListener(ShowFamiliar);
        _withdrawAmountInputField.onValidateInput += new InputField.OnValidateInput(ValidatePosInt);
        _withdrawAmountInputField.onEndEdit.AddListener(EndEdit);
    }

    //  Methods

    public void ShowInventory()
    {
        _familiarButton.image.color = Color.white;
        _familiarArea.SetActive(false);

        _inventoryButton.image.color = _selectedColor;
        _inventoryArea.SetActive(true);
    }

    private void ShowFamiliar()
    {
        _inventoryButton.image.color = Color.white;
        _inventoryArea.SetActive(false);

        _familiarButton.image.color = _selectedColor;
        _familiarArea.SetActive(true);
    }

    public void ShowBoth()
    {
        _inventoryArea.SetActive(true);
        _familiarArea.SetActive(true);
    }

    public uint AmountToWithdraw()
    {
        return uint.Parse(_withdrawAmountInputField.text);
    }

    public bool WithdrawNotes()
    {
        return _notedToggle.isOn;
    }

    private char ValidatePosInt(string text, int charIndex, char addedChar)
    {
        if (char.IsDigit(addedChar))
        {
            if (charIndex == 0 && addedChar == '0')
                return '\0';
            else
                return addedChar;
        }
        else
        {
            return '\0';
        }
    }

    private void EndEdit(string value)
    {
        if (string.IsNullOrEmpty(value))
            _withdrawAmountInputField.text = "1";
    }
}
