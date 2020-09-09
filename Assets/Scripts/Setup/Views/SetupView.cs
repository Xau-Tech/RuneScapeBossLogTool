using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//  View for the SetupMVC.cs class
public class SetupView : MonoBehaviour, IDisplayable<Setup>
{
    [SerializeField] private PlayerView playerView;

    //  Display all setup data to view
    public void Display(in Setup value)
    {
        Display(value.player);
    }

    //  Display all player data to view
    public void Display(in Player value)
    {
        playerView.Display(value);
    }
}
