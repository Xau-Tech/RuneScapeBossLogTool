//  Slot that holds a quantity of one type of item
public class ItemSlot
{
    public ItemSlot()
    {
        quantity = 1;
    }
    public ItemSlot(Item item, uint quantity)
    {
        this.item = item;
        this.quantity = quantity;
    }

    public uint quantity { get; set; }
    public Item item { get; set; }

    public virtual ulong GetValue()
    {
        return (quantity * item.GetValue());
    }

    public string Print()
    {
        return $"ItemQuantity [ Name: {item.itemName}, Price: {item.price}\nQuantity: {quantity} ]";
    }

    public override string ToString()
    {
        return $"{item.itemName}\nQuantity: {quantity}\nValue: {GetValue().ToString("N0")} gp";
    }
}
