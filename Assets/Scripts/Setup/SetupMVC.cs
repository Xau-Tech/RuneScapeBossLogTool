using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupMVC
{
    public SetupMVC(SetupView view)
    {
        model = new Setup();
        this.view = view;
        view.Display(model);
        EventManager.Instance.onNewUsernameEntered += LoadNewPlayerStats;
    }
    ~SetupMVC()
    {
        EventManager.Instance.onNewUsernameEntered -= LoadNewPlayerStats;
    }

    private Setup model;
    private SetupView view;

    public Player Player { get { return model.player; }}

    //  Load stats of a player from username
    public async void LoadNewPlayerStats(string username)
    {
        model.player = await SetupLoader.LoadPlayerStatsAsync(username);
        view.DisplayPlayer(Player);
    }
}
