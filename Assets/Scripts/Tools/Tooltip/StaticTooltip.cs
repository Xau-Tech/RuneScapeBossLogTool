using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Tooltip for any object that is static - never changes
/// </summary>
public class StaticTooltip : AbsTooltip, IPointerEnterHandler, IPointerExitHandler
{
    //  Properties & fields

    [SerializeField] private string _toolTipText;

    //  Methods

    public void OnPointerEnter(PointerEventData eventData)
    {
        base.Display(Input.mousePosition, _toolTipText);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        base.Remove();
    }
}
