using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

//  MVC for the Setup
public class SetupMVC
{
    public SetupMVC(SetupView view)
    {
        model = new Setup();
        this.view = view;

        //  Sub to events
        EventManager.Instance.onNewUsernameEntered += LoadNewPlayerStatsAsync;
    }
    ~SetupMVC()
    {
        //  Unsub to events
        EventManager.Instance.onNewUsernameEntered -= LoadNewPlayerStatsAsync;
    }

    public Player Player { get { return model.player; } }

    private Setup model;
    private SetupView view;

    //  Load stats of a player from username asynchronously
    public async void LoadNewPlayerStatsAsync(string username)
    {
        model.player = await SetupLoader.LoadPlayerStatsAsync(username);
        view.Display(Player);
    }

    public void AddSetupItem(in SetupItem setupItem, in ItemSlotCategories itemSlotCategory, in int index)
    {
        switch (itemSlotCategory)
        {
            case ItemSlotCategories.Inventory:
                Player.Inventory.SetItemAtIndex(in setupItem, in index);
                view.DisplaySetupCost(SetupInfo.Instance.TotalCost);
                break;
            default:
                break;
        }
    }
}
