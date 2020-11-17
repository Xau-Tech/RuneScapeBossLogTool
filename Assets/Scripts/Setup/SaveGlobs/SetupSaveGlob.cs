using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  Aspect of the Setup class that will be saved
[System.Serializable]
public class SetupSaveGlob
{
    public float chargeDrainRate { get; set; }
    public int instanceCost { get; set; }
    public int combatIntensity { get; set; }
    public string setupName { get; set; }
    public PlayerGlob player { get; set; }

    public SetupSaveGlob(in Setup setup)
    {
        this.chargeDrainRate = setup.ChargeDrainRate;
        this.instanceCost = setup.InstanceCost;
        this.combatIntensity = (int)setup.combatIntensity.IntensityLevel;
        this.setupName = setup.SetupName;
        player = new PlayerGlob(setup.player);
    }
}
