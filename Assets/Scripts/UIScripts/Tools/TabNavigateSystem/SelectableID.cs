using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//  TabNavigateSelectables.cs attaches this script to all selectables in its data array
public class SelectableID : MonoBehaviour
{
    private int id;
    private Action<int> onSelectCallback;

    //  Populate the Id and OnSelectCallback action for this Selectable
    public void Setup(in int id, in Action<int> callback)
    {
        this.id = id;
        onSelectCallback = callback;
    }

    //  Notify the TabNavigateSelectable.cs script that the object with int id has been selected
    public void OnSelectCallback(UnityEngine.EventSystems.BaseEventData data)
    {
        onSelectCallback(id);
    }
}
