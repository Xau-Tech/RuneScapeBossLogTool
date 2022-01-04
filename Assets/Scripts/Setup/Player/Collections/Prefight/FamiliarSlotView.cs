using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class FamiliarSlotView : AbsSetupItemSlotView, IPointerClickHandler, ITooltipHandler
{
    //  Properties & fields

    public int SlotNum { private get; set; }

    [SerializeField] private Image _itemImage;
    [SerializeField] private Text _quantityText;
    private Sprite _defaultSprite;
    private float _emptySlotAlpha;

    //  Methods

    public override void Init(int slotId)
    {
        _defaultSprite = _itemImage.sprite;
        _emptySlotAlpha = _itemImage.color.a;
        base.slotCategory = Enums.ItemSlotCategory.Familiar;
        base.itemSlot = new ItemSlot(General.NullItem(), 1);
        this.SlotNum = slotId;
    }

    public override void Display(SetupItem setupItem, uint quantity)
    {
        itemSlot.Quantity = quantity;

        //  Display base item sprite
        base.Display(setupItem, _itemImage);

        Color col = Color.white;

        if (setupItem.ItemId == -1)
            col.a = _emptySlotAlpha;
        else
            col.a = 255.0f;

        _itemImage.color = col;

        //  If stackable, show and update quantity text for item slot
        if (setupItem.IsStackable)
        {
            _quantityText.enabled = true;
            _quantityText.text = quantity + "";
        }
        //  Otherwise disable quantity text
        else
        {
            _quantityText.enabled = false;
        }
    }

    public override Sprite GetDefaultSprite()
    {
        return _defaultSprite;
    }

    public string GetTooltipMessage()
    {
        //  Return empty string if no item
        if (itemSlot.Item.ItemId == -1)
            return "";

        //  Print item name, quantity, total cost
        return $"Item: {itemSlot.Item.ItemName}\n" +
            $"Cost: {itemSlot.GetValue().ToString("N0")}";
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        base.OnClick(eventData, this.SlotNum);
    }
}
