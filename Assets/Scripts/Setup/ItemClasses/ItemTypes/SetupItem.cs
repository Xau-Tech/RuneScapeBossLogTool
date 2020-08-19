using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SetupItem", menuName = "Setup/ItemTypes/GeneralItem", order = 0)]
public class SetupItem : Item
{
    public SetupItem(bool isStackable)
    {
        this.isStackable = isStackable;
    }

    public bool IsStackable { get { return isStackable; } protected set { isStackable = value; } }
    public Sprite ItemSprite { get { return itemSprite; } }
    public List<AbstractItemEffect> ItemEffects { get { return itemEffects; } }

    [SerializeField] private bool isStackable;
    [SerializeField] private Sprite itemSprite;
    [SerializeField] private List<AbstractItemEffect> itemEffects;

    public void Apply(Player player)
    {
        for (int i = 0; i < ItemEffects.Count; ++i)
            ItemEffects[i].Apply(player);
    }
}
