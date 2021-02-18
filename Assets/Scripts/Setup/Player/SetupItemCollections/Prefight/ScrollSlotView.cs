using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//  View for an itemslot used only for SummoningScroll items
public class ScrollSlotView : AbsSetupItemSlotView, IPointerClickHandler, ITooltipHandler
{
    public int slotNumber { private get; set; }

    [SerializeField] private Image itemImage;
    [SerializeField] private Text quantityText;
    private Sprite defaultSprite;
    private float emptySlotAlpha;

    public override void Init(int slotNumber)
    {
        defaultSprite = itemImage.sprite;
        emptySlotAlpha = itemImage.color.a;
        slotCategory = ItemSlotCategories.Scroll;
        itemSlot = new ItemSlot(General.NullItem(), 1);
        this.slotNumber = slotNumber;
    }

    public override void Display(in SetupItem setupItem, uint quantity)
    {
        itemSlot.quantity = quantity;

        //  Display base item sprite
        base.Display(in setupItem, in itemImage);

        Color col = Color.white;

        if (setupItem.itemID == -1)
            col.a = emptySlotAlpha;
        else
            col.a = 255.0f;

        itemImage.color = col;

        //  If stackable, show and update quantity text for item slot
        if (setupItem.isStackable)
        {
            quantityText.enabled = true;
            quantityText.text = quantity + "";
        }
        //  Otherwise disable quantity text
        else
        {
            quantityText.enabled = false;
        }
    }

    public override Sprite GetDefaultSprite()
    {
        return defaultSprite;
    }

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

    public void OnPointerClick(PointerEventData eventData)
    {
        base.OnClick(in eventData, slotNumber);
    }
}