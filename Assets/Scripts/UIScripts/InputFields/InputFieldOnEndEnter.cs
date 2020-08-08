using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

//  Script to enable inputfield's to run a passed action on enter press while active
public class InputFieldOnEndEnter : MonoBehaviour
{
    public Action endEditAction { private get; set; }

    private InputField thisInputField;

    private void Awake()
    {
        thisInputField = GetComponent<InputField>();

        if (!thisInputField)
            throw new System.Exception($"ItemAmountInputField.cs is not attached to an inputfield gameobject!");
    }

    private void Update()
    {
        //  Calls the AddItem function on enter press within this InputField
        if(EventSystem.current.currentSelectedGameObject == thisInputField.gameObject)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                endEditAction();
            }
        }
    }
}
