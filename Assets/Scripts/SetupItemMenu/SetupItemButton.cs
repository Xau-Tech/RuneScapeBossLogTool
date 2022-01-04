using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// Button created for SetupItems in the SetupItemMenu
/// </summary>
public class SetupItemButton : MonoBehaviour, IPointerClickHandler
{
    //  Properties & fields

    private Button _thisButton;
    private SetupItemStruct _setupItemStruct;

    //  Monobehaviors

    private void Awake()
    {
        _thisButton = GetComponent<Button>();
    }

    //  Methods

    //  Set item category and button text
    public void Setup(SetupItemStruct setupItemStruct)
    {
        this._setupItemStruct = setupItemStruct;
        GetComponentInChildren<Text>().text = setupItemStruct.ItemName;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        SetupItemMenuController menu = gameObject.GetComponentInParent<SetupItemMenuController>();
        SetupItem setupItem;

        if(SetupItemDictionary.TryGetItem(_setupItemStruct.ItemId, out setupItem))
        {
            //  If the itemslot was in the inventory, add the amount from the input field, otherwise add 1
            if (menu.ItemSlotCategory == Enums.ItemSlotCategory.Inventory || menu.ItemSlotCategory == Enums.ItemSlotCategory.Scroll)
                EventManager.Instance.SetupItemAdded(setupItem, menu.AmountToWithdraw, menu.CollectionType, menu.ItemSlotCategory, menu.ClickedSlotID);
            else
                EventManager.Instance.SetupItemAdded(setupItem, 1, menu.CollectionType, menu.ItemSlotCategory, menu.ClickedSlotID);
        }

        menu.OnPointerExit(new PointerEventData(EventSystem.current));
    }
}

public struct AddedItemData
{
    public SetupItem Item;
    public Enums.ItemSlotCategory ItemSlotCategory;
    public int SlotIndex;

    public AddedItemData(SetupItem item, Enums.ItemSlotCategory itemSlotCategory, int slotIndex)
    {
        this.Item = item;
        this.ItemSlotCategory = itemSlotCategory;
        this.SlotIndex = slotIndex;
    }
}