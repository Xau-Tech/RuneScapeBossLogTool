using UnityEngine;
using UnityEngine.EventSystems;

//  Generic class for any tooltip
public abstract class GenericTooltip : MonoBehaviour
{
    //  Show tooltip
    protected void DisplayTooltip(in Vector2 pointerLocation, in string tooltipText)
    {
        TooltipController.Instance.DisplayTooltip(in pointerLocation, this.gameObject.transform.parent, in tooltipText);
    }

    //  Remove tooltip
    protected void RemoveTooltip()
    {
        TooltipController.Instance.DisableTooltip();
    }
}
