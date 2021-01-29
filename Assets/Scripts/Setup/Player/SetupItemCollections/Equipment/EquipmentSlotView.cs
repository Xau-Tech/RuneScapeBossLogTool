using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//  View for an individual EquipmentSlot
public class EquipmentSlotView : AbsSetupItemSlotView, IDisplayable<SetupItem>, IPointerClickHandler, ITooltipHandler
{
    public int equipmentSlotNumber { private get; set; }

    [SerializeField] private Image itemImage;
    [SerializeField] private ItemSlotCategories itemSlotCategory;
    //  Default image is the equipment slot's placeholder for when it has no item
    private Sprite defaultSprite;

    public void Init(int equipmentSlotNumber)
    {
        slotCategory = itemSlotCategory;
        defaultSprite = itemImage.sprite;
        itemSlot = new ItemSlot(General.NullItem(), 1);
        this.equipmentSlotNumber = equipmentSlotNumber;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        base.OnClick(in eventData, equipmentSlotNumber);
    }

    public void Display(in SetupItem setupItem)
    {
        base.Display(in setupItem, in itemImage);
    }

    public override Sprite GetDefaultSprite()
    {
        return defaultSprite;
    }

    //  ITooltipHandler message implementation
    public string GetTooltipMessage()
    {
        //  Return empty if no item
        if (itemSlot.item.itemID == -1)
            return "";

        string tooltip = $"Item: {itemSlot.item.itemName}\n";

        //  Augmented items compute as a group not individually so it will not display on a tooltip over the charge drain section
        if(itemSlot.item is AugmentedArmour || itemSlot.item is AugmentedWeapon)
        {
            tooltip += $"Total aug costs: {CacheManager.SetupTab.Setup.Player.AugmentsCost} gp/hr";
        }
        //  Otherwise simply get the value
        else
        {
            tooltip += $"Cost: {itemSlot.GetValue().ToString()} gp/hr";
        }

        //  Print item name and cost
        return tooltip;
    }
}
