using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  Model for the SetupMVC.cs class
//  Holds data for the Setup tab including player, items, etc
public class Setup
{
    public Setup()
    {
        combatIntensity = new CombatIntensity(CombatIntensity.CombatIntensityLevels.Low);
        ChargeDrainRate = 0.0f;
        player = new Player(string.Empty);
    }

    public float ChargeDrainRate { get; private set; }
    public Player player { get; set; }
    public int TotalCost { get { return player.Inventory.TotalCost + player.Equipment.TotalCost + InstanceCost;} }
    public float DegradePerHour { get { return combatIntensity.degradePerHour; } }
    public float ChargeDrainPerHour { get { return combatIntensity.drainPerHour; } }
    public int InstanceCost { get; set; }

    private CombatIntensity combatIntensity;

    //  Set charge drain rate
    public void SetChargeDrainRate(in float chargeDrainRate)
    {
        this.ChargeDrainRate = chargeDrainRate;
        player.Equipment.DetermineCost();
    }

    public void SetCombatIntensity(in CombatIntensity.CombatIntensityLevels intensityLevel)
    {
        combatIntensity.IntensityLevel = intensityLevel;
        player.Equipment.DetermineCost();
    }
}