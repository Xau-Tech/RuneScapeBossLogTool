using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class AbstractItemSlotView : MonoBehaviour
{
    protected Button thisButton;

    private void Awake()
    { 
        thisButton = GetComponent<Button>();

        if (!thisButton)
            throw new System.Exception($"SetupItemButton.cs is not attached to a button!");
    }
}

public enum ItemSlotTypes { Inventory, Armor, Mainhand, Offhand };
