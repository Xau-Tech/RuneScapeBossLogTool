using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//  Data class for the hit chance calculator
public class HitChance
{
    private int combatLevel;
    private int weaponAccTier;
    private AttackType.AttackStyles attStyle;
    private LevelModifier lvlModifier;
    private AffinityModifier affinityModifier;
    private AccuracyModifier accuracyModifier;
    private HitChanceModifier hitChanceModifier;

    public HitChance(int combatLevel, int weaponAccTier)
    {
        this.combatLevel = combatLevel;
        this.weaponAccTier = weaponAccTier;
        this.attStyle = (AttackType.AttackStyles)1; //  0 is none so initiate to 1
        affinityModifier = new AffinityModifier();
        lvlModifier = new LevelModifier();
        accuracyModifier = new AccuracyModifier();
        hitChanceModifier = new HitChanceModifier();
    }

    public int CombatLevel { get { return combatLevel; } }
    public int WeaponAccTier { get { return weaponAccTier; } }
    public AttackType.AttackStyles AttackStyle { get { return attStyle; } }
    public AffinityModifier AffinityModifier { get { return affinityModifier; } }
    public LevelModifier LvlModifier { get { return lvlModifier; } }
    public AccuracyModifier AccuracyModifier { get { return accuracyModifier; } }
    public HitChanceModifier HitChanceModifier { get { return hitChanceModifier; } }

    //  Get the boosted combat level
    public int BoostedCombatLevel()
    {
        return lvlModifier.BoostSkill(combatLevel);
    }

    public void SetCombatLevel(int value)
    {
        combatLevel = value;
    }

    public void SetWeaponAccTier(int value)
    {
        weaponAccTier = value;
    }

    public void SetAttackStyle(AttackType.AttackStyles attackStyle)
    {
        this.attStyle = attackStyle;
    }

    public void SetLevelMod(in LevelModifier levelMod)
    {
        lvlModifier = levelMod;
    }

    public void SetAccuracyMod(in AccuracyModifier accMod)
    {
        accuracyModifier = accMod;
    }

    public void SetHitChanceMod(in HitChanceModifier hcMod)
    {
        hitChanceModifier = hcMod;
    }

    public void SetAffinityMod(in AffinityModifier affMod)
    {
        affinityModifier = affMod;
    }
}
