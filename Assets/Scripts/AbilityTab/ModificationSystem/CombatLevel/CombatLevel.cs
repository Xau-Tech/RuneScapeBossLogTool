using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatLevel
{
    public byte BaseCombatSkillLevel
    {
        get { return m_BaseCombatSkillLevel; }
        set
        {
            m_BaseCombatSkillLevel = value;
            Modify();
        }
    }
    public byte ModdedCombatSkillLevel { get { return m_ModdedCombatSkillLevel; } }
    public byte BoostAmount { get { return (byte)(m_ModdedCombatSkillLevel - m_BaseCombatSkillLevel); } }
    public double BleedInclusive_Aura { get { return m_ActiveBleedInclAuraBoost; } }

    private byte m_BaseCombatSkillLevel;
    private byte m_ModdedCombatSkillLevel;
    private IModdableUnit<byte> m_ModdableUnit;
    private PotionMods m_ActivePotionBoost;
    private MultiplicativeBoost m_ActiveAuraBoost;
    private double m_ActiveBleedInclAuraBoost;
    private readonly byte M_DEFAULTLVL = 99;
    private readonly Dictionary<string, MultiplicativeBoost> m_CmbLvlModDict_Aura = new();
    private readonly Dictionary<string, PotionMods> m_CmbLvlModDict_Potion = new();
    private readonly Dictionary<string, double> m_BleedInclusiveAuraMultipliers = new();

    public CombatLevel()
    {
        BuildAuraDict();
        BuildPotionDict();
        m_ActivePotionBoost = m_CmbLvlModDict_Potion["None"];
        m_ActiveAuraBoost = m_CmbLvlModDict_Aura["None"];
        m_ActiveBleedInclAuraBoost = m_BleedInclusiveAuraMultipliers["None"];
        m_BaseCombatSkillLevel = M_DEFAULTLVL;
        m_ModdedCombatSkillLevel = BaseCombatSkillLevel;
        Modify();
    }

    public void Modify()
    {
        List<IModifier<byte>> combatLvlMods = new();

        CombatLevelModifier combinedMod = new(m_ActiveAuraBoost + m_ActivePotionBoost.MultiplyBoost);

        combatLvlMods.Add(combinedMod);
        combatLvlMods.Add(m_ActivePotionBoost.AddBoost);

        ModdableUnit<byte> mu = new(m_BaseCombatSkillLevel, combatLvlMods);
        m_ModdableUnit = mu;

        m_ModdedCombatSkillLevel = m_ModdableUnit.ModifyObject();
        Debug.Log("Modded combat lvl is now " + m_ModdedCombatSkillLevel);
    }

    public void ApplyPotion(string potionName)
    {
        m_ActivePotionBoost = m_CmbLvlModDict_Potion[potionName];
        Modify();
        Player_Ability.Instance.AbilDamage.BoostedCombatLevel = m_ModdedCombatSkillLevel;
    }

    public void ApplyAura(string auraName)
    {
        m_ActiveAuraBoost = m_CmbLvlModDict_Aura[auraName];
        m_ActiveBleedInclAuraBoost = m_BleedInclusiveAuraMultipliers[auraName];
        Modify();
        Player_Ability.Instance.AbilDamage.BoostedCombatLevel = m_ModdedCombatSkillLevel;
    }

    public List<string> GetAuraOptions()
    {
        return new List<string>(m_CmbLvlModDict_Aura.Keys);
    }

    public List<string> GetPotionOptions()
    {
        return new List<string>(m_CmbLvlModDict_Potion.Keys);
    }

    private void BuildAuraDict()
    {
        //  Combat level boost info
        MultiplicativeBoost emptyBoost = new(1.0d);
        MultiplicativeBoost zerkStyleBoost = new(1.1d);

        m_CmbLvlModDict_Aura.Add("None", emptyBoost);
        m_CmbLvlModDict_Aura.Add("Berserker variant", zerkStyleBoost);
        m_CmbLvlModDict_Aura.Add("Mahjarrat", emptyBoost);

        //  Bleed inclusive boost info
        m_BleedInclusiveAuraMultipliers.Add("None", 1.0d);
        m_BleedInclusiveAuraMultipliers.Add("Berserker variant", 1.1d);
        m_BleedInclusiveAuraMultipliers.Add("Mahjarrat", 1.05d);
    }

    private void BuildPotionDict()
    {
        PotionMods emptyMod = new(new MultiplicativeBoost(1.0d), new(new AdditiveBoost(0.0d)));
        PotionMods ordinarymod = new(new MultiplicativeBoost(1.08d), new(new AdditiveBoost(1.0d)));
        PotionMods superMod = new(new MultiplicativeBoost(1.12d), new(new AdditiveBoost(2.0d)));
        PotionMods grandMod = new(new MultiplicativeBoost(1.14d), new(new AdditiveBoost(2.0d)));
        PotionMods extremeMod = new(new MultiplicativeBoost(1.15d), new(new AdditiveBoost(3.0d)));
        PotionMods supremeMod = new(new MultiplicativeBoost(1.16d), new(new AdditiveBoost(4.0d)));
        PotionMods overloadMod = new(new MultiplicativeBoost(1.15d), new(new AdditiveBoost(3.0d)));
        PotionMods supremeOverloadMod = new(new MultiplicativeBoost(1.16d), new(new AdditiveBoost(4.0d)));
        PotionMods elderOverloadMod = new(new MultiplicativeBoost(1.17d), new(new AdditiveBoost(5.0d)));

        m_CmbLvlModDict_Potion.Add("None", emptyMod);
        m_CmbLvlModDict_Potion.Add("Ordinary", ordinarymod);
        m_CmbLvlModDict_Potion.Add("Super", superMod);
        m_CmbLvlModDict_Potion.Add("Grand", grandMod);
        m_CmbLvlModDict_Potion.Add("Extreme", extremeMod);
        m_CmbLvlModDict_Potion.Add("Supreme", supremeMod);
        m_CmbLvlModDict_Potion.Add("Overload", overloadMod);
        m_CmbLvlModDict_Potion.Add("Supreme overload", supremeOverloadMod);
        m_CmbLvlModDict_Potion.Add("Elder overload", elderOverloadMod);
    }

    private class PotionMods
    {
        public MultiplicativeBoost MultiplyBoost { get { return m_MultiplyBoost; } }
        public CombatLevelModifier AddBoost { get { return m_AddBoost; } }

        private readonly MultiplicativeBoost m_MultiplyBoost;
        private readonly CombatLevelModifier m_AddBoost;

        public PotionMods(MultiplicativeBoost multiBoost, CombatLevelModifier addBoost)
        {
            m_MultiplyBoost = multiBoost;
            m_AddBoost = addBoost;
        }
    }
}