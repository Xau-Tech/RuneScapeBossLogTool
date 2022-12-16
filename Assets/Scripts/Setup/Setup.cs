using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Setup
{
    //  Properties & fields

    public string SetupName { get; set; }
    public float ChargeDrainRate { get; private set; }
    public Player Player { get; set; }
    public int TotalCost { get { return Player.Inventory.TotalCost + Player.Equipment.TotalCost + Player.BeastOfBurden.TotalCost + Player.Prefight.TotalCost + InstanceCost; } }
    public float DegradePerHour { get { return CombatIntensity.DegradePerHour; } }
    public float ChargeDrainPercent { get { return CombatIntensity.DrainPerHour; } }
    public int InstanceCost { get; set; }
    public CombatIntensity CombatIntensity { get; private set; }

    //  Constructor

    public Setup(string setupName)
    {
        this.SetupName = setupName;
        CombatIntensity = new CombatIntensity(Enums.CombatIntensityLevels.Low);
        ChargeDrainRate = 0.0f;
        InstanceCost = 0;
        Player = new Player(string.Empty);
    }
    public Setup(string setupName, Player player)
    {
        this.SetupName = setupName;
        CombatIntensity = new CombatIntensity(Enums.CombatIntensityLevels.Low);
        ChargeDrainRate = 0.0f;
        InstanceCost = 0;

        this.Player = new Player(player.Username);
        this.Player.SmithingLevel = player.SmithingLevel;
        this.Player.PrayerLevel = player.PrayerLevel;
    }
    public Setup(SetupSaveGlob sg, Player player)
    {
        this.SetupName = sg.setupName;
        this.CombatIntensity = new CombatIntensity((Enums.CombatIntensityLevels)sg.combatIntensity);
        this.ChargeDrainRate = sg.chargeDrainRate;
        this.InstanceCost = sg.instanceCost;

        //  Player
        this.Player = new Player(player.Username, sg.player);
        ApplicationController.Instance.CurrentSetup = this;
        this.Player.SmithingLevel = player.SmithingLevel;
        this.Player.PrayerLevel = player.PrayerLevel;
    }

    //  Methods

    public async Task LoadNewPlayerStatsAsync(string username)
    {
        AppState.DataState = Enums.DataStates.Loading;
        Player = await StatsLoader.LoadPlayerStatsAsync(username);
        AppState.DataState = Enums.DataStates.None;
    }

    public void SetChargeDrainRate(float chargeDrainRate)
    {
        this.ChargeDrainRate = chargeDrainRate;
        this.Player.Equipment.DetermineCost();
    }

    public void SetCombatIntensity(Enums.CombatIntensityLevels intensity)
    {
        this.CombatIntensity.IntensityLevel = intensity;
        this.Player.Equipment.DetermineCost();
    }
}
