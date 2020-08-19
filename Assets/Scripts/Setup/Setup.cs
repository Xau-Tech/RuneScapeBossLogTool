using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  Model for the SetupMVC.cs class
//  Holds data for the Setup tab including player, items, etc
public class Setup
{
    public Setup()
    {
        player = new Player(string.Empty);
    }

    public Player player { get; set; }
}
