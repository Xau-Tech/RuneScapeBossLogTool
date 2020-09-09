﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Setup/ItemTypes/Potion", order = 2)]
public class Potion : SetupItem
{
    public Potion(bool isStackable) { }

    public void Drink(in Setup setup)
    {
        base.Apply(setup);
    }
}
