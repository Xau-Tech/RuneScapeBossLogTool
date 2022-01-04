using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// Use this to attach an action to an input field to trigger when the enter key is pressed
/// </summary>
public class IFOnEndEnter : MonoBehaviour
{
    //  Properties & fields
    public Action EndEditAction { private get; set; }

    //  Monobehaviors

    private void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == this.gameObject && Input.GetKeyDown(KeyCode.Return))
            EndEditAction?.Invoke();
    }
}
