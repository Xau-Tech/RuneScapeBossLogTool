using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  Abstract class for tooltips to inherit from
/// </summary>
public abstract class AbsTooltip : MonoBehaviour
{
    //  Methods

    protected void Display(Vector2 pointerLoc, string text)
    {
        TooltipController.Instance.Display(pointerLoc, this.gameObject.transform.parent, text);
    }

    protected void Remove()
    {
        TooltipController.Instance.DisableTooltip();
    }
}
