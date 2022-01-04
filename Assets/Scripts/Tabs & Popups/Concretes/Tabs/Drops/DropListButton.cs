using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

/// <summary>
/// Button for the drop list in the drops tab - one button per unique item
/// </summary>
public class DropListButton : MonoBehaviour, IPointerClickHandler
{
    //  Properties & fields
    private Button _thisButton;
    private Text _thisText;
    private ItemSlot _itemSlot;
    private int _index;
    private Action<int> _removeCallback;
    [SerializeField] private GameObject _lastAddedNote;

    //  Monobehaviors

    private void Awake()
    {
        _thisButton = this.GetComponent<Button>();
        ((IPointerClickHandler)_thisButton).OnPointerClick(new PointerEventData(EventSystem.current));
        _thisText = _thisButton.GetComponentInChildren<Text>();
    }

    //  Methods

    //  Associate this button with an itemslot drop
    public void Setup(DropList sender, ItemSlot itemSlot, int index, bool lastAddition)
    {
        _removeCallback += sender.ShowRemoveDropButton;
        this._itemSlot = itemSlot;
        this._index = index;
        _thisText.text = _itemSlot.ToString();

        //  Show last addition note if relevant
        if (lastAddition)
            _lastAddedNote.SetActive(true);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //  Show on right click
        if (eventData.button == PointerEventData.InputButton.Right)
            _removeCallback(_index);
    }
}
