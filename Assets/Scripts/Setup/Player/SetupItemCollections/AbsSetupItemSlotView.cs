using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//  Abstract view class for a SetupItem
public abstract class AbsSetupItemSlotView : MonoBehaviour
{
    protected ItemSlot itemSlot;
    protected ItemSlotCategories slotCategory;

    public virtual void Display(in SetupItem setupItem, in Image image)
    {
        //  Unity tries to shrink/expand your new sprite to the original sprite's dimensions even with preserve aspect ratio on
        //  Unless you set it to null first....FUN
        image.sprite = null;
        image.sprite = setupItem.itemSprite;

        itemSlot.item = setupItem;
    }

    protected void OnClick(in PointerEventData eventData, in int slotID)
    {
        Debug.Log($"Base OnClick [ SlotType = {slotCategory}, SlotID: {slotID} ]");

        //  Left click
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            List<SetupItemCategories> categories;

            //  Check for subcategories and open a menu with them if they exist
            if(SetupItemTypes.TryGetSubcategories(in slotCategory, out categories))
                UIController.Instance.SetupItemMenu.NewMenu(slotCategory, slotID, in categories);
            else
            {
                //  Otherwise, get matching SetupItemCategory and create menu with its list of SetupItems
                SetupItemCategories itemCategory = SetupItemTypes.GetCategoryFromSlotType(in slotCategory);
                UIController.Instance.SetupItemMenu.NewMenu(slotCategory, slotID, SetupItemsDictionary.GetItems(in itemCategory));
            }
        }
        //  Right click
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            //  Slot has an item
            if(itemSlot.item.itemID != -1)
                CacheManager.SetupTab.Setup.RemoveItemButton.Show(slotCategory, slotID, GetDefaultSprite());
        }
    }

    public abstract Sprite GetDefaultSprite();
}

public enum ItemSlotCategories { Inventory, Head, Pocket, Cape, Necklace, Ammunition, Mainhand, Body, Offhand, Legs, Gloves, Boots, Ring };