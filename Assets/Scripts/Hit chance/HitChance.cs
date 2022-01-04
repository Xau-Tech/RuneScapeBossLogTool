using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitChance
{
    //  Properties & fields

    public int CombatLevel { get; set; }
    public int WeaponAccTier { get; set; }
    public Enums.AttackStyles AttackStyle { get; set; }
    public AffinityModifier AffinityMod { get; set; }
    public LevelModifier LvlMod { get; set; }
    public AccuracyModifier AccuracyMod { get; set; }
    public HitChanceModifier HitChanceMod { get; set; }

    //  Constructor

    public HitChance(int combatLevel, int weaponAccTier)
    {
        this.CombatLevel = combatLevel;
        this.WeaponAccTier = weaponAccTier;
        this.AttackStyle = (Enums.AttackStyles)1;
        this.AffinityMod = new AffinityModifier();
        this.LvlMod = new LevelModifier();
        this.AccuracyMod = new AccuracyModifier();
        this.HitChanceMod = new HitChanceModifier();
    }

    //  Methods

    public int BoostedCombatLevel()
    {
        return LvlMod.BoostSkill(this.CombatLevel);
    }
}
