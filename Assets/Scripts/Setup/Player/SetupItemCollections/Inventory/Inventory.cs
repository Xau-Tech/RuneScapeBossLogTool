public class Inventory : AbsItemSlotList
{
    public int TotalCost { get; private set; }

    private const int SIZE = 28;

    public Inventory() : base(SIZE)
    {
        TotalCost = 0;
    }

    public Inventory(InventoryGlob inventorySaveGlob) : base(SIZE)
    {
        TotalCost = 0;

        SetupItem setupItem;

        for(int i = 0; i < SIZE; ++i)
        {
            SetupItemsDictionary.TryGetItem(inventorySaveGlob.itemSlots[i].itemID, out setupItem);
            data[i] = new ItemSlot(setupItem, inventorySaveGlob.itemSlots[i].quantity);
            TotalCost += (int)data[i].GetValue();
        }
    }

    public override void SetItemAtIndex(in SetupItem setupItem, uint quantity, int index)
    {
        ulong previousCost = data[index].GetValue();

        base.SetItemAtIndex(setupItem, quantity, index);
        TotalCost += (int)(data[index].GetValue() - previousCost);

        EventManager.Instance.InventoryItemAdded(in setupItem, quantity, in index);
    }

    public override void FillUI()
    {
        for(int i = 0; i < data.Count; ++i)
        {
            EventManager.Instance.InventoryItemAdded(data[i].item as SetupItem, data[i].quantity, i);
        }
    }
}
