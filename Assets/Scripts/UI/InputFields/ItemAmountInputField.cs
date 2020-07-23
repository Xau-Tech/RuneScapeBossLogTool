using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//  Script for inputfield where user enters the amount of selected item to add to droplist
public class ItemAmountInputField : MonoBehaviour
{
    private InputField thisInputField;
    [SerializeField] private Dropdown itemListDropdown;

    private void Awake()
    {
        thisInputField = GetComponent<InputField>();

        if (!thisInputField)
            throw new System.Exception($"ItemAmountInputField.cs is not attached to an input field gameobject!");
        else
            thisInputField.GetComponent<InputFieldOnEndEnter>().endEditAction = SelectItemListDropdown;
    }

    private void OnEnable()
    {
        EventManager.Instance.onLogUpdated += Clear;
        EventManager.Instance.onBossDropdownValueChanged += Clear;
    }

    private void OnDisable()
    {
        EventManager.Instance.onLogUpdated -= Clear;
        EventManager.Instance.onBossDropdownValueChanged -= Clear;
    }

    //  Clear text
    private void Clear()
    {
        thisInputField.text = "";
    }

    private void SelectItemListDropdown()
    {
        itemListDropdown.Select();
    }
}
