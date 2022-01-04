using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// Abstract view class for a SetupItem
/// </summary>
public abstract class AbsSetupItemSlotView : MonoBehaviour
{
    //  Properties & fields

    protected ItemSlot itemSlot;
    protected Enums.ItemSlotCategory slotCategory;

    private readonly string _SETUPCOLTAG = "SetupCollectionView";

    //  Methods

    public virtual void Display(SetupItem setupItem, Image image)
    {
        //  Unity tries to shrink/expand your new sprite to the original sprite's dimensions even with preserve aspect ratio on
        //  Unless you set it to null first....FUN
        image.sprite = null;

        if (setupItem.ItemId == -1)
            image.sprite = GetDefaultSprite();
        else
            image.sprite = setupItem.ItemSprite;

        itemSlot.Item = setupItem;
    }

    protected void OnClick(PointerEventData eventData, int slotId)
    {
        GameObject go = this.gameObject;

        while (!go.CompareTag(_SETUPCOLTAG))
        {
            go = go.transform.parent.gameObject;
        }

        Enums.SetupCollections collType = go.GetComponent<AbsSetupItemCollView>().CollectionType;

        Debug.Log($"Base OnClick [ Slottype: {slotCategory}, SlotID: {slotId}, Collection: {collType} ]");

        //  Left click
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            List<Enums.SetupItemCategory> categories;
            uint amountToWithdraw = ControlsView.Instance.AmountToWithdraw();

            //  Check for subcategories and open a menu with them if they exist
            if (SetupItemTypes.TryGetSubcategories(slotCategory, out categories))
            {
                SetupItemMenuController.Instance.NewMenu(collType, slotCategory, slotId, categories, amountToWithdraw);
            }
            else
            {
                //  Otherwise, get matching SetupItemCategory and create menu with its list of SetupItems
                Enums.SetupItemCategory itemCategory = SetupItemTypes.GetSetupItemCategory(slotCategory);
                SetupItemMenuController.Instance.NewMenu(collType, slotCategory, slotId, SetupItemDictionary.GetItems(itemCategory), amountToWithdraw);
            }
        }
        //  Right click
        else if(eventData.button == PointerEventData.InputButton.Right)
        {
            //  Slot has an item
            if(itemSlot.Item.ItemId != -1)
            {
                RemoveItemButton.Instance.Show(collType, this.slotCategory, slotId, GetDefaultSprite());
            }
        }
    }

    public abstract void Init(int slotId);
    public abstract void Display(SetupItem setupItem, uint quantity);
    public abstract Sprite GetDefaultSprite();
}
