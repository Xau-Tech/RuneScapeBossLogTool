using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  Class to create lists of modifiers for use in the hit chance calculator
public class ModifierList
{
    //  All the list types necessary
    public enum ModTypes { Potion, Prayer, Aura, Nihil, Scrimshaw, Reaper, Quake, StatWarh, Gstaff, BandosBook };

    //  List of lists of AbsEffects
    protected List<List<AbsEffect>> effects;
    protected int index;

    public ModifierList(ModTypes modType)
    {
        index = 0;
        effects = new List<List<AbsEffect>>();

        //  Create list based on type passed in
        switch (modType)
        {
            case ModTypes.Potion:
                BuildPotionList();
                break;
            case ModTypes.Prayer:
                BuildPrayerList();
                break;
            case ModTypes.Aura:
                BuildAuraList();
                break;
            case ModTypes.Nihil:
                BuildNihilList();
                break;
            case ModTypes.Scrimshaw:
                BuildScrimList();
                break;
            case ModTypes.Reaper:
                BuildReaperList();
                break;
            case ModTypes.Quake:
                BuildQuakeList();
                break;
            case ModTypes.StatWarh:
                BuildStatWarhammerList();
                break;
            case ModTypes.Gstaff:
                BuildGstaffList();
                break;
            case ModTypes.BandosBook:
                BuildBandosBookList();
                break;
            default:
                break;
        }
    }

    //  Set the index to mark the currently active effects list
    public void SetActive(int index)
    {
        this.index = index;
    }

    //  Return a list of the active effects
    public List<AbsEffect> ActiveEffects()
    {
        return effects[index];
    }

    //  Return a list of the names
    //  Used in populating any Dropdown with proper values
    public List<string> GetOptionNames()
    {
        List<string> names = new List<string>();

        for(int i = 0; i < effects.Count; ++i)
        {
            names.Add(effects[i][0].EffectName);
        }

        return names;
    }

    private void BuildPrayerList()
    {
        effects.Add(new List<AbsEffect> { new LevelModifier("None", 0.0d, 0) });        //  No prayer
        effects.Add(new List<AbsEffect> { new LevelModifier("Turmoil variant", 0.0d, 10) });    //  Turmoil equivalent prayers (t95)
        effects.Add(new List<AbsEffect> { new LevelModifier("Malevolence variant", 0.0d, 12) });        //  Malevolent equivalent prayers (t99)
    }

    private void BuildAuraList()
    {
        effects.Add(new List<AbsEffect>() { new AccuracyModifier("None", 0.0d) });      //  No aura
        effects.Add(new List<AbsEffect>() { new AccuracyModifier("Standard brawler variant", 0.03d) });     //  Standard tier accuracy auras
        effects.Add(new List<AbsEffect>() { new AccuracyModifier("Greater brawler variant", 0.05d) });      //  Greater tier accuracy auras
        effects.Add(new List<AbsEffect>() { new AccuracyModifier("Master brawler variant", 0.07d) });       //  Master tier accuracy auras
        effects.Add(new List<AbsEffect>() { new AccuracyModifier("Supreme brawler variant", 0.10d) });      //  Supreme tier accuracy auras
        effects.Add(new List<AbsEffect>() { new AccuracyModifier("Berserker variant", 0.10d), new LevelModifier("Berserker variant", 0.10d, 0) });      //  Berserker equivalent auras
    }

    private void BuildPotionList()
    {
        effects.Add(new List<AbsEffect> { new LevelModifier("None", 0.0d, 0) });        //  No potion
        effects.Add(new List<AbsEffect> { new LevelModifier("Overload", 0.15d, 3) });       //  Overload
        effects.Add(new List<AbsEffect> { new LevelModifier("Supreme overload", 0.16d, 4) });       //  Supreme overload
        effects.Add(new List<AbsEffect> { new LevelModifier("Elder overload", 0.17d, 5) });     //  Elder overload
    }

    private void BuildNihilList()
    {
        effects.Add(new List<AbsEffect> { new AccuracyModifier("False", 0.0d) });       //  No nihil
        effects.Add(new List<AbsEffect> { new AccuracyModifier("True", 0.05d) });       //  Active nihil
    }

    private void BuildScrimList()
    {
        effects.Add(new List<AbsEffect> { new AccuracyModifier("None", 0.0d) });        //  No scrimshaw
        effects.Add(new List<AbsEffect> { new AccuracyModifier("Inferior", 0.02d) });       //  Inferior accuracy scrimshaws
        effects.Add(new List<AbsEffect> { new AccuracyModifier("Superior", 0.04d) });       //  Superior accuracy scrimshaws
    }

    private void BuildReaperList()
    {
        effects.Add(new List<AbsEffect> { new HitChanceModifier("False", 0) });     //  No reaper/eof
        effects.Add(new List<AbsEffect> { new HitChanceModifier("True", 3) });      //  Reaper or eof
    }

    private void BuildQuakeList()
    {
        effects.Add(new List<AbsEffect> { new AffinityModifier("False", 0) });      //  Quake not used
        effects.Add(new List<AbsEffect> { new AffinityModifier("True", 2) });       //  Quake used
    }

    private void BuildStatWarhammerList()
    {
        effects.Add(new List<AbsEffect> { new AffinityModifier("False", 0) });      //  Statius warhammer not used
        effects.Add(new List<AbsEffect> { new AffinityModifier("True", 5) });       //  Statius warhammer used
    }

    private void BuildGstaffList()
    {
        effects.Add(new List<AbsEffect> { new AffinityModifier("False", 0) });      //  Guthix staff not used
        effects.Add(new List<AbsEffect> { new AffinityModifier("True", 2) });       //  Guthix staff used
    }

    private void BuildBandosBookList()
    {
        effects.Add(new List<AbsEffect> { new AffinityModifier("False", 0) });      //  Bandos book not used
        effects.Add(new List<AbsEffect> { new AffinityModifier("True", 3) });       //  Bandos book used
    }
}


