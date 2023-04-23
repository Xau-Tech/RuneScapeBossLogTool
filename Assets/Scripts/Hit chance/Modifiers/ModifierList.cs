using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Keeps track of all modifiers used by the hit chance calculator
/// </summary>
public class ModifierList
{
    //  Properties & fields

    protected List<List<AbsEffect>> _effects;
    protected int _index;

    //  Constructor

    public ModifierList(Enums.ModTypes modType)
    {
        _index = 0;
        _effects = new List<List<AbsEffect>>();

        //  Create list based on type passed in

        switch (modType)
        {
            case Enums.ModTypes.Potion:
                BuildPotionList();
                break;
            case Enums.ModTypes.Prayer:
                BuildPrayerList();
                break;
            case Enums.ModTypes.Aura:
                BuildAuraList();
                break;
            case Enums.ModTypes.Nihil:
                BuildNihilList();
                break;
            case Enums.ModTypes.Scrimshaw:
                BuildScrimList();
                break;
            case Enums.ModTypes.Reaper:
                BuildReaperList();
                break;
            case Enums.ModTypes.Quake:
                BuildQuakeList();
                break;
            case Enums.ModTypes.StatWarh:
                BuildStatWarhammerList();
                break;
            case Enums.ModTypes.Gstaff:
                BuildGstaffList();
                break;
            case Enums.ModTypes.BandosBook:
                BuildBandosBookList();
                break;
            default:
                break;
        }
    }

    //  Methods

    //  Set the index to mark the currently active effects list
    public void SetActive(int index)
    {
        _index = index;
    }

    //  Return a list of the active effects
    public List<AbsEffect> ActiveEffects()
    {
        return _effects[_index];
    }

    public List<string> OptionNames()
    {
        List<string> names = new List<string>();

        for (int i = 0; i < _effects.Count; ++i)
        {
            names.Add(_effects[i][0].EffectName);
        }

        return names;
    }

    private void BuildPrayerList()
    {
        _effects.Add(new List<AbsEffect> { new LevelModifier("None", 0.0d, 0) });        //  No prayer
        _effects.Add(new List<AbsEffect> { new LevelModifier("T1 Prayer", 0.0d, 2) });       //  t1 normal prayer
        _effects.Add(new List<AbsEffect> { new LevelModifier("T2 Prayer", 0.0d, 4) });      //  t2 normal prayer
        _effects.Add(new List<AbsEffect> { new LevelModifier("T3 Prayer", 0.0d, 6) });       //  t3 normal prayer
        _effects.Add(new List<AbsEffect> { new LevelModifier("Chivalry", 0.0d, 7) });       //  chivalry
        _effects.Add(new List<AbsEffect> { new LevelModifier("Piety variant", 0.0d, 8) });       //  piety variant
        _effects.Add(new List<AbsEffect> { new LevelModifier("Leech curse", 0.0d, 5) });        //  leech curses
        _effects.Add(new List<AbsEffect> { new LevelModifier("Turmoil variant", 0.0d, 10) });    //  Turmoil equivalent prayers (t95)
        _effects.Add(new List<AbsEffect> { new LevelModifier("Praesul variant", 0.0d, 12) });        //  Praesul equivalent prayers (t99)
    }

    private void BuildAuraList()
    {
        _effects.Add(new List<AbsEffect>() { new AccuracyModifier("None", 0.0d) });      //  No aura
        _effects.Add(new List<AbsEffect>() { new AccuracyModifier("T1 accuracy", 0.03d) });     //  Standard tier accuracy auras
        _effects.Add(new List<AbsEffect>() { new AccuracyModifier("T2 accuracy", 0.05d) });      //  Greater tier accuracy auras
        _effects.Add(new List<AbsEffect>() { new AccuracyModifier("T3 accuracy", 0.07d) });       //  Master tier accuracy auras
        _effects.Add(new List<AbsEffect>() { new AccuracyModifier("T4 accuracy", 0.10d) });      //  Supreme tier accuracy auras
        _effects.Add(new List<AbsEffect>() { new AccuracyModifier("Berserker variant", 0.10d), new LevelModifier("Berserker variant", 0.10d, 0) });      //  Berserker equivalent auras
    }

    private void BuildPotionList()
    {
        _effects.Add(new List<AbsEffect> { new LevelModifier("None", 0.0d, 0) });        //  No potion
        _effects.Add(new List<AbsEffect> { new LevelModifier("Ordinary", .08d, 1) });       //  Ordinary potions
        _effects.Add(new List<AbsEffect> { new LevelModifier("Super", .12d, 2) });          //  Super potions
        _effects.Add(new List<AbsEffect> { new LevelModifier("Grand", .14d, 2) });          //  Grand potions
        _effects.Add(new List<AbsEffect> { new LevelModifier("Extreme", .15d, 3) });        //  Extreme potions
        _effects.Add(new List<AbsEffect> { new LevelModifier("Supreme", .16d, 4) });        //  Supreme potions
        _effects.Add(new List<AbsEffect> { new LevelModifier("Overload", 0.15d, 3) });       //  Overload
        _effects.Add(new List<AbsEffect> { new LevelModifier("Supreme overload", 0.16d, 4) });       //  Supreme overload
        _effects.Add(new List<AbsEffect> { new LevelModifier("Elder overload", 0.17d, 5) });     //  Elder overload
    }

    private void BuildNihilList()
    {
        _effects.Add(new List<AbsEffect> { new AccuracyModifier("False", 0.0d) });       //  No nihil
        _effects.Add(new List<AbsEffect> { new AccuracyModifier("True", 0.05d) });       //  Active nihil
    }

    private void BuildScrimList()
    {
        _effects.Add(new List<AbsEffect> { new AccuracyModifier("None", 0.0d) });        //  No scrimshaw
        _effects.Add(new List<AbsEffect> { new AccuracyModifier("Inferior", 0.02d) });       //  Inferior accuracy scrimshaws
        _effects.Add(new List<AbsEffect> { new AccuracyModifier("Superior", 0.04d) });       //  Superior accuracy scrimshaws
    }

    private void BuildReaperList()
    {
        _effects.Add(new List<AbsEffect> { new HitChanceModifier("False", 0) });     //  No reaper/eof
        _effects.Add(new List<AbsEffect> { new HitChanceModifier("True", 3) });      //  Reaper or eof
    }

    private void BuildQuakeList()
    {
        _effects.Add(new List<AbsEffect> { new AffinityModifier("False", 0) });      //  Quake not used
        _effects.Add(new List<AbsEffect> { new AffinityModifier("True", 2) });       //  Quake used
    }

    private void BuildStatWarhammerList()
    {
        _effects.Add(new List<AbsEffect> { new AffinityModifier("False", 0) });      //  Statius warhammer not used
        _effects.Add(new List<AbsEffect> { new AffinityModifier("True", 5) });       //  Statius warhammer used
    }

    private void BuildGstaffList()
    {
        _effects.Add(new List<AbsEffect> { new AffinityModifier("False", 0) });      //  Guthix staff not used
        _effects.Add(new List<AbsEffect> { new AffinityModifier("True", 2) });       //  Guthix staff used
    }

    private void BuildBandosBookList()
    {
        _effects.Add(new List<AbsEffect> { new AffinityModifier("False", 0) });      //  Bandos book not used
        _effects.Add(new List<AbsEffect> { new AffinityModifier("True", 3) });       //  Bandos book used
    }
}
