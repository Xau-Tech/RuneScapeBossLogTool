using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlotView : AbsSetupItemSlotView, IPointerClickHandler, ITooltipHandler
{
    public int inventorySlotNumber { private get; set; }

    [SerializeField] private Image noteImage;
    [SerializeField] private Image itemImage;
    [SerializeField] private Text quantityText;
    private Sprite defaultSprite;

    public override void Init(int inventorySlotNumber)
    {
        defaultSprite = itemImage.sprite;
        slotCategory = ItemSlotCategories.Inventory;
        itemSlot = new ItemSlot(General.NullItem(), 1);
        this.inventorySlotNumber = inventorySlotNumber;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        base.OnClick(in eventData, inventorySlotNumber);
    }

    public override void Display(in SetupItem setupItem, uint quantity)
    {
        itemSlot.quantity = quantity;

        //  Display base item sprite
        base.Display(in setupItem, in itemImage);

        //  If stackable, show and update quantity text for item slot and disable note image
        if (setupItem.isStackable)
        {
            quantityText.enabled = true;
            quantityText.text = quantity + "";
            noteImage.gameObject.SetActive(false);
        }
        //  Item is not stackable
        else
        {
            //  More than one unstackable item in the slot
            if(quantity > 1)
            {
                quantityText.enabled = true;
                quantityText.text = quantity + "";
                noteImage.gameObject.SetActive(true);
            }
            //  One item in the slot
            else
            {
                quantityText.enabled = false;
                noteImage.gameObject.SetActive(false);
            }
        }

        //  If not stackable AND multiple items, display the note image
        if(!setupItem.isStackable && quantity > 1)
        {
            noteImage.gameObject.SetActive(true);
        }
        else
        {
            noteImage.gameObject.SetActive(false);
        }
    }

    public override Sprite GetDefaultSprite()
    {
        return defaultSprite;
    }

    //  ITooltipHandler message
    public string GetTooltipMessage()
    {
        //  Return empty string if no item
        if (itemSlot.item.itemID == -1)
            return "";

        //  Print item name, quantity, total cost
        return $"Item: {itemSlot.item.itemName}\n" +
            $"Quantity: {itemSlot.quantity}\n" +
            $"Cost: {itemSlot.GetValue().ToString("N0")}";
    }
}
