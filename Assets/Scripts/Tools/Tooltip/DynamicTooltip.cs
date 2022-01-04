using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;

/// <summary>
/// Tooltip for any UI element that may change during runtime
/// </summary>
public class DynamicTooltip : AbsTooltip, IPointerEnterHandler, IPointerExitHandler
{
    //  Methods

    public void OnPointerEnter(PointerEventData eventData)
    {
        string tooltipText = "";
        Component[] monos = gameObject.GetComponents<MonoBehaviour>();

        foreach(Component mono in monos)
        {
            //  Search for tooltip handler to get message
            if(mono is ITooltipHandler)
            {
                tooltipText = mono.GetComponent<ITooltipHandler>().GetTooltipMessage();
                break;
            }
        }

        //  If string is empty, don't enable
        if (string.IsNullOrEmpty(tooltipText))
            return;
        else
            base.Display(Input.mousePosition, tooltipText);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        base.Remove();
    }
}
