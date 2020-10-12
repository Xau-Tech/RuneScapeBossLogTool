using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//  View for the SetupMVC.cs class
public class SetupView : MonoBehaviour, IDisplayable<Setup>
{
    public RemoveSetupItemButton RemoveItemButton { get { return removeItemButton.GetComponent<RemoveSetupItemButton>(); } }

    [SerializeField] private PlayerView playerView;
    [SerializeField] private InfoView infoView;
    [SerializeField] private GameObject removeItemButton;

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

    //  Display SetupInfo
    public void DisplaySetupCost(in long totalCost)
    {
        infoView.DisplayCost(in totalCost);
    }
}
