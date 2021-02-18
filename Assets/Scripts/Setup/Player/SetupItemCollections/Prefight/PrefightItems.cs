//  Data class for Prefight setupitem collection
public class PrefightItems : AbsItemSlotList
{
    public int TotalCost { get; private set; }

    private const int SIZE = 8;

    public PrefightItems() : base(SIZE)
    {
        TotalCost = 0;
    }

    public PrefightItems(PrefightItemsGlob prefightItemsGlob) : base(SIZE)
    {
        TotalCost = 0;

        bool emptyGlob = (prefightItemsGlob == null);
        SetupItem setupItem;

        for (int i = 0; i < SIZE; ++i)
        {
            if (emptyGlob)
            {
                data[i] = new ItemSlot(General.NullItem(), 1);
            }
            else
            {
                SetupItemsDictionary.TryGetItem(prefightItemsGlob.itemSlots[i].itemID, out setupItem);
                data[i] = new ItemSlot(setupItem, prefightItemsGlob.itemSlots[i].quantity);
                TotalCost += (int)data[i].GetValue();
            }
        }
    }

    public override void SetItemAtIndex(in SetupItem setupItem, uint quantity, int index)
    {
        ulong previousCost = data[index].GetValue();

        base.SetItemAtIndex(setupItem, quantity, index);
        TotalCost += (int)(data[index].GetValue() - previousCost);

        EventManager.Instance.PrefightItemAdded(in setupItem, quantity, index);
    }

    public override void FillUI()
    {
        for(int i = 0; i < data.Count; ++i)
        {
            EventManager.Instance.PrefightItemAdded(data[i].item as SetupItem, data[i].quantity, i);
        }
    }
}
