using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controller class for the tooltip system
/// </summary>
public class TooltipController : MonoBehaviour
{
    //  Properties & fields

    public static TooltipController Instance { get; } = new TooltipController();

    private static GameObject _tooltipObject;
    private readonly Vector2 _OFFSET = new Vector2(-5.0f, 8.0f);

    //  Monobehaviors

    private void Update()
    {
        if (_tooltipObject == null)
            return;

        if (_tooltipObject.activeSelf)
            UpdatePosition();
    }

    //  Methods

    public void Display(Vector2 pointerLoc, Transform refObj, string text)
    {
        //  Create if null
        if (_tooltipObject == null)
            _tooltipObject = Instantiate((Resources.Load("TooltipContainer") as GameObject), pointerLoc, Quaternion.identity, refObj) as GameObject;

        //  Update parent
        _tooltipObject.transform.SetParent(refObj);

        //  Climb parent objects until a panel is found
        while (!_tooltipObject.transform.parent.CompareTag("Panel"))
        {
            _tooltipObject.transform.SetParent(_tooltipObject.transform.parent.parent);
        }

        _tooltipObject.SetActive(true);
        _tooltipObject.GetComponentInChildren<Text>().text = text;

        UpdatePosition();
    }

    private void UpdatePosition()
    {
        RectTransform rect = _tooltipObject.GetComponent<RectTransform>();

        Vector3 newPos = new Vector3(
            Input.mousePosition.x + _OFFSET.x - (rect.sizeDelta.x / 2.0f * ApplicationController.Instance.CanvasScale),
            Input.mousePosition.y + _OFFSET.y + (rect.sizeDelta.y * ApplicationController.Instance.CanvasScale),
            0.0f);

        _tooltipObject.transform.position = newPos;
    }

    public void DisableTooltip()
    {
        if (!_tooltipObject)
            return;

        _tooltipObject.SetActive(false);
    }
}
