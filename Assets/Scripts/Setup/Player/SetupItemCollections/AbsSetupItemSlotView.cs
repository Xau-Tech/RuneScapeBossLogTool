﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//  Abstract view class for a SetupItem
public abstract class AbsSetupItemSlotView : MonoBehaviour
{
    protected ItemSlot itemSlot;
    protected ItemSlotCategories slotCategory;

    private static string SETUPCOLTAG = "SetupCollectionView";

    public abstract void Init(int slotID);
    public abstract void Display(in SetupItem setupItem, uint quantity);

    public virtual void Display(in SetupItem setupItem, in Image image)
    {
        //  Unity tries to shrink/expand your new sprite to the original sprite's dimensions even with preserve aspect ratio on
        //  Unless you set it to null first....FUN
        image.sprite = null;

        if (setupItem.itemID == -1)
            image.sprite = GetDefaultSprite();
        else
            image.sprite = setupItem.itemSprite;

        itemSlot.item = setupItem;
    }

    protected void OnClick(in PointerEventData eventData, in int slotID)
    {
        GameObject go = this.gameObject;

        while(!go.CompareTag(SETUPCOLTAG))
        {
            go = go.transform.parent.gameObject;
        }

        SetupCollections collectionType = go.GetComponent<AbstractSetupItemColView>().CollectionType;

        Debug.Log($"Base OnClick [ SlotType: {slotCategory}, SlotID: {slotID}, Collection: {collectionType} ]");

        //  Left click
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            List<SetupItemCategories> categories;
            uint amountToWithdraw = SetupControls.Instance.AmountToWithdraw();

            //  Check for subcategories and open a menu with them if they exist
            if(SetupItemTypes.TryGetSubcategories(in slotCategory, out categories))
                UIController.Instance.SetupItemMenu.NewMenu(collectionType, slotCategory, slotID, in categories, amountToWithdraw);
            else
            {
                //  Otherwise, get matching SetupItemCategory and create menu with its list of SetupItems
                SetupItemCategories itemCategory = SetupItemTypes.GetCategoryFromSlotType(in slotCategory);
                UIController.Instance.SetupItemMenu.NewMenu(collectionType, slotCategory, slotID, SetupItemsDictionary.GetItems(in itemCategory), amountToWithdraw);
            }
        }
        //  Right click
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            //  Slot has an item
            if(itemSlot.item.itemID != -1)
                CacheManager.SetupTab.Setup.RemoveItemButton.Show(collectionType, slotCategory, slotID, GetDefaultSprite());
        }
    }

    public abstract Sprite GetDefaultSprite();
}

public enum ItemSlotCategories { Inventory, Head, Pocket, Cape, Necklace, Ammunition, Mainhand, Body, Offhand, Legs, Gloves, Boots, Ring, Familiar, Scroll };