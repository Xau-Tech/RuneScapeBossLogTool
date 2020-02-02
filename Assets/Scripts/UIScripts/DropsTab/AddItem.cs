using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddItem : MonoBehaviour
{
    [SerializeField] Dropdown m_ItemDropdown;
    [SerializeField] InputField m_ItemAmountField;

    public void OnEnterPressed()
    {
        if (Input.GetKeyDown(KeyCode.Return))
            OnClick();
    }

    public void OnClick()
    {
        string itemName = m_ItemDropdown.options[m_ItemDropdown.value].text;
        int amount = int.Parse(m_ItemAmountField.text);
        m_ItemAmountField.text = "";

        //  Field is empty or negative
        if (amount <= 0)
            EventManager.Instance.InputWarningOpen("You must enter a positive value!");
        //  Add the item
        else
            EventManager.Instance.AddItemButtonClicked(itemName, amount);
    }
}
