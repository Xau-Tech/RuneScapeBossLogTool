using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

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
}
