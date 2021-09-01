using UnityEngine;
using UnityEngine.UI;

//  Class to control the tooltip system
public class TooltipController : MonoBehaviour
{
    public static TooltipController Instance { get; } = new TooltipController();

    private static GameObject tooltipObject;

    private readonly Vector2 offset = new Vector3(-5.0f, 8.0f);

    private void Update()
    {
        if (tooltipObject == null)
            return;

        //  If tooltip is active, update position
        if (tooltipObject.activeSelf)
            UpdatePosition();
    }

    //  Display tooltip on initial enter of a UI object that can display one
    public void DisplayTooltip(in Vector2 pointerLocation, in Transform referenceObject, in string tooltipText)
    {
        //  If not yet instantiated, instantiate
        if (tooltipObject == null)
            tooltipObject = Instantiate((Resources.Load("TooltipContainer") as GameObject), pointerLocation, Quaternion.identity, referenceObject) as GameObject;

        //  Update the tooltip's parent in case the sender is in another tab of the application as the previous sender
        tooltipObject.transform.SetParent(referenceObject);

        //  Climb the tree of parent objects until finding a Panel, one of the 3 main screens, in order to display properly
        while(!tooltipObject.transform.parent.CompareTag("Panel"))
        {
            tooltipObject.transform.SetParent(tooltipObject.transform.parent.parent);
        }

        //  Turn on and set text
        tooltipObject.SetActive(true);
        tooltipObject.GetComponentInChildren<Text>().text = tooltipText;

        UpdatePosition();
    }

    //  Set position of tooltip box
    private void UpdatePosition()
    {
        RectTransform rectT = tooltipObject.GetComponent<RectTransform>();

        Vector3 posAdjustment = new Vector3(Input.mousePosition.x + offset.x - (rectT.sizeDelta.x / 2.0f * UIController.Instance.CanvasScale),
                Input.mousePosition.y + offset.y + (rectT.sizeDelta.y * UIController.Instance.CanvasScale),
                0.0f);
        tooltipObject.transform.position = posAdjustment;
    }

    //  Turn off tooltip
    public void DisableTooltip()
    {
        if (!tooltipObject)
            return;

        tooltipObject.SetActive(false);
    }
}
