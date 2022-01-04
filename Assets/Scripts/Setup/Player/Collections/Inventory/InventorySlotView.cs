using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlotView : AbsSetupItemSlotView, IPointerClickHandler, ITooltipHandler
{
    //  Properties & fields

    public int InventorySlotNumber { private get; set; }

    [SerializeField] private Image _noteImage;
    [SerializeField] private Image _itemImage;
    [SerializeField] private Text _quantityText;
    private Sprite _defaultSprite;

    //  Methods

    public override void Init(int slotId)
    {
        _defaultSprite = _itemImage.sprite;
        slotCategory = Enums.ItemSlotCategory.Inventory;
        base.itemSlot = new ItemSlot(General.NullItem(), 1);
        this.InventorySlotNumber = slotId;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        base.OnClick(eventData, InventorySlotNumber);
    }

    public override void Display(SetupItem setupItem, uint quantity)
    {
        base.itemSlot.Quantity = quantity;

        //  Display base item sprite
        base.Display(setupItem, _itemImage);

        //  If stackable, show and update quantity text for item slot and disable note image
        if (setupItem.IsStackable)
        {
            _quantityText.enabled = true;
            _quantityText.text = quantity + "";
            _noteImage.gameObject.SetActive(false);
        }
        //  Item is not stackable
        else
        {
            //  More than one unstackable item
            if(quantity > 1)
            {
                _quantityText.enabled = true;
                _quantityText.text = quantity + "";
                _noteImage.gameObject.SetActive(true);
            }
            else
            {
                _quantityText.enabled = false;
                _noteImage.gameObject.SetActive(false);
            }
        }

        if (!setupItem.IsStackable && quantity > 1)
            _noteImage.gameObject.SetActive(true);
        else
            _noteImage.gameObject.SetActive(false);
    }

    public override Sprite GetDefaultSprite()
    {
        return _defaultSprite;
    }

    public string GetTooltipMessage()
    {
        if (itemSlot.Item.ItemId == -1)
            return "";
        else
            return $"Item: {itemSlot.Item.ItemName}\n" +
                $"Quantity: {itemSlot.Quantity}\n" +
                $"Cost: {itemSlot.GetValue().ToString("N0")}";
    }
}
