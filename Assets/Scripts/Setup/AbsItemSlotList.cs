using System.Collections.Generic;

//  Abstract class for List of ItemSlots for use in Setups
public abstract class AbsItemSlotList
{
    public AbsItemSlotList(int size)
    {
        for(int i = 0; i < size; ++i)
        {
            data.Add(new ItemSlot(General.NullItem(), 1));
        }
    }

    protected List<ItemSlot> data = new List<ItemSlot>();

    public virtual void SetItemAtIndex(in SetupItem setupItem, uint quantity, int index)
    {
        //  Update the item
        data[index].item = setupItem;
        data[index].quantity = quantity;
    }

    //  Return a list of empty slot indices starting at startIndex with a maximum count of maxNumberOfSlots
    public List<int> GetEmptySlots(int startIndex, int maxNumberOfSlots)
    {
        List<int> emptySlotIndices = new List<int>();
        int emptySlotsFound = 0;

        for(int i = startIndex; i < data.Count; ++i)
        {
            //  Stop if enough empty slots have been found
            if (emptySlotsFound >= maxNumberOfSlots)
                break;
            else
            {
                //  Add index and increment slotsfound if empty (id == -1)
                if(data[i].item.itemID == -1)
                {
                    emptySlotIndices.Add(i);
                    emptySlotsFound++;
                }
            }
        }

        return emptySlotIndices;
    }

    public List<ItemSlot> GetData()
    {
        return data;
    }
}
