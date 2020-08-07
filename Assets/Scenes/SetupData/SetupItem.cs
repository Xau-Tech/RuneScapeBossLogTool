using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SetupItem : Item
{
    public SetupItem(Item item) : base(item.itemID, item.name, item.price, item.isStackable)
    {

    }

    public AbsItemEffect[] itemEffects;

    public void ApplyEffects()
    {
        for(int i = 0; i < itemEffects.Length; ++i)
        {
            itemEffects[i].Apply();
        }
    }
}
