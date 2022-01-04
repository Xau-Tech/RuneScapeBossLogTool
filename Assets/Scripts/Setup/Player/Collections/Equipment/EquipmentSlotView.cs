using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EquipmentSlotView : AbsSetupItemSlotView, IPointerClickHandler, ITooltipHandler
{
    //  Properties & fields

    public int SlotNum { private get; set; }

    [SerializeField] private Image _itemImage;
    [SerializeField] private Enums.ItemSlotCategory _itemSlotCategory;
    private Sprite _defaultSprite;

    //  Methods

    public override void Init(int slotId)
    {
        base.slotCategory = _itemSlotCategory;
        _defaultSprite = _itemImage.sprite;
        base.itemSlot = new ItemSlot(General.NullItem(), 1);
        this.SlotNum = slotId;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        base.OnClick(eventData, this.SlotNum);
    }

    public override void Display(SetupItem setupItem, uint quantity)
    {
        base.Display(setupItem, _itemImage);
    }

    public override Sprite GetDefaultSprite()
    {
        return _defaultSprite;
    }

    public string GetTooltipMessage()
    {
        if (base.itemSlot.Item.ItemId == -1)
            return "";

        string tooltip = $"Item: {base.itemSlot.Item.ItemName}\n";

        if (base.itemSlot.Item is AugmentedArmour || base.itemSlot.Item is AugmentedWeapon)
            tooltip += $"Total aug costs: {ApplicationController.Instance.CurrentSetup.Player.AugmentsCost.ToString("N0")} gp/hr";
        else
            tooltip += $"Cost: {base.itemSlot.GetValue().ToString("N0")} gp/hr";

        return tooltip;
    }
}
