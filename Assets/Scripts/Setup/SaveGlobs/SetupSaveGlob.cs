using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SetupSaveGlob
{
    //  Properties & fields

    public float chargeDrainRate { get; set; }
    public int instanceCost { get; set; }
    public int combatIntensity { get; set; }
    public string setupName { get; set; }
    public PlayerGlob player { get; set; }

    //  Constructor

    public SetupSaveGlob(Setup setup)
    {
        this.chargeDrainRate = setup.ChargeDrainRate;
        this.instanceCost = setup.InstanceCost;
        this.combatIntensity = (int)setup.CombatIntensity.IntensityLevel;
        this.setupName = setup.SetupName;
        player = new PlayerGlob(setup.Player);
    }
}
