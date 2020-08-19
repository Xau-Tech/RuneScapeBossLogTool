using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Setup/ItemTypes/Potion", order = 2)]
public class Potion : SetupItem
{
    public Potion(bool isStackable) : base(isStackable) { }

    public void Drink(Player player)
    {
        base.Apply(player);
    }
}
