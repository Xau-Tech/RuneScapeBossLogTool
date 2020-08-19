using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//  View for the SetupMVC.cs class
public class SetupView : MonoBehaviour, IDisplayable<Setup>
{
    [SerializeField] private PlayerView playerView;

    public void Display(in Setup value)
    {
        DisplayPlayer(value.player);
    }

    public void DisplayPlayer(in Player player)
    {
        playerView.Display(player);
    }
}
