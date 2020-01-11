using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddItem : MonoBehaviour
{
    public void OnEnterPressed()
    {
        if (Input.GetKeyDown(KeyCode.Return))
            OnClick();
    }

    public void OnClick()
    {
        if (UIController.uicontroller.m_ItemAmountInputField.text == "")
            EventManager.manager.InputWarningOpen("You must enter a value!");
        else
            EventManager.manager.AddItemButtonClicked();
    }
}
