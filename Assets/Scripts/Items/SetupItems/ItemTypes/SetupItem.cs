using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SetupItem", menuName = "Setup/ItemTypes/GeneralItem", order = 0)]
public abstract class SetupItem : Item
{
    public SetupItem() { }

    public bool IsStackable { get { return isStackable; } }
    public Sprite ItemSprite { get { return itemSprite; } }
    public List<AbstractItemEffect> ItemEffects { get { return itemEffects; } }

    [SerializeField] private bool isStackable;
    [SerializeField] private Sprite itemSprite;
    [SerializeField] private List<AbstractItemEffect> itemEffects;

    public void Apply()
    {
        for (int i = 0; i < ItemEffects.Count; ++i)
            ItemEffects[i].Apply();
    }

    public abstract ulong GetCost();
}
