using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Tooltip placed on a UI object to show the name of its sprite
/// </summary>
public class ImageTooltip : MonoBehaviour, ITooltipHandler
{
    //  Properties & fields

    private readonly string _MASKIMGNAME = "UIMask";

    //  Methods

    string ITooltipHandler.GetTooltipMessage()
    {
        Image img;

        if ((img = GetComponent<Image>()) != null)
        {
            if (img.sprite.name.CompareTo(_MASKIMGNAME) == 0)
                return "";
            else
                return img.sprite.name;
        }
        else
        {
            return "";
        }
    }
}
