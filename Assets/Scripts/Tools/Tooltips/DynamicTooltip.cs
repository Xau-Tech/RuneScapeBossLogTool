using UnityEngine;
using UnityEngine.EventSystems;

//  Tooltip for any element that may change at runtime
public class DynamicTooltip : GenericTooltip, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        string tooltipText = "";
        Component[] monos = gameObject.GetComponents<MonoBehaviour>();

        //  Iterate through all Monobehaviors on the gameobject
        foreach(Component mono in monos)
        {
            //  Once a tooltiphandler is found, get the message and break
            if(mono is ITooltipHandler)
            {
                tooltipText = mono.GetComponent<ITooltipHandler>().GetTooltipMessage();
                break;
            }
        }

        //  If the message is empty, don't enable the tooltip
        if (string.IsNullOrEmpty(tooltipText))
            return;

        base.DisplayTooltip(Input.mousePosition, in tooltipText);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        base.RemoveTooltip();
    } 
}
