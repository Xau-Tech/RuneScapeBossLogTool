using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Setup/Food")]
public class Food : SetupItem
{
    public Food(Item item) : base(item)
    {

    }

    public short baseHealAmount;

    public void Eat()
    {
        Debug.Log($"You eat the {base.name} healing {baseHealAmount} lifepoints!");
        for(int i = 0; i < base.itemEffects.Length; ++i)
        {
            base.itemEffects[i].Apply();
        }
    }
}
