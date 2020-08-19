using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Food", menuName = "Setup/ItemTypes/Food", order = 1)]
public class Food : SetupItem
{
    public Food(bool isStackable) : base(isStackable) { }

    [SerializeField] private short baseHealAmount;

    public void Eat(Player player)
    {
        player.healthRestored += baseHealAmount;
        base.Apply(player);
    }
}
