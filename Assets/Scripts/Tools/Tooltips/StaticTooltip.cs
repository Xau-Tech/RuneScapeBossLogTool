using UnityEngine;
using UnityEngine.EventSystems;

//  Any tooltip that is static - can be written out before compiling and building
public class StaticTooltip : GenericTooltip, IPointerEnterHandler, IPointerExitHandler
{
    public string tooltipText;

    public void OnPointerEnter(PointerEventData eventData)
    {
        base.DisplayTooltip(Input.mousePosition, in tooltipText);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        base.RemoveTooltip();
    }
}
