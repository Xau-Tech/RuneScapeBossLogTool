using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//  Script implementing the tooltip handler to return the name of the sprite in an image monobehavior
public class ImageNameTooltip : MonoBehaviour, ITooltipHandler
{
    private static readonly string MASKIMGNAME = "UIMask";

    string ITooltipHandler.GetTooltipMessage()
    {
        Image img;

        //  Return empty string if no image is attached or the image is the UIMask image
        if ((img = GetComponent<Image>()) != null)
        {
            if (img.sprite.name.CompareTo(MASKIMGNAME) == 0)
                return "";
            else
                return img.sprite.name;
        }
        else
            return "";
    }
}
