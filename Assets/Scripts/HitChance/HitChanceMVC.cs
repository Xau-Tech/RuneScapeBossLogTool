using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  Controller for the hit chance calculator
public class HitChanceMVC
{
    private HitChance model;
    private HitChanceView view;

    //  LevelModifier and related lists
    private LevelModifier levelMod = new LevelModifier();
    private ModifierList prayerMods = new ModifierList(ModifierList.ModTypes.Prayer);
    private ModifierList potionMods = new ModifierList(ModifierList.ModTypes.Potion);
    private ModifierList auraMods = new ModifierList(ModifierList.ModTypes.Aura);

    //  AccuracyModifier and related lists
    private AccuracyModifier accMod = new AccuracyModifier();
    private ModifierList nihilMods = new ModifierList(ModifierList.ModTypes.Nihil);
    private ModifierList scrimMods = new ModifierList(ModifierList.ModTypes.Scrimshaw);

    //  HitChanceModifier and related lists
    private HitChanceModifier hcMod = new HitChanceModifier();
    private ModifierList reaperMods = new ModifierList(ModifierList.ModTypes.Reaper);

    //  AffinityModifier and related lists
    private AffinityModifier affinityMod = new AffinityModifier();
    private ModifierList quakeMods = new ModifierList(ModifierList.ModTypes.Quake);
    private ModifierList statWarhMods = new ModifierList(ModifierList.ModTypes.StatWarh);
    private ModifierList gstaffMods = new ModifierList(ModifierList.ModTypes.Gstaff);
    private ModifierList bandosBookMods = new ModifierList(ModifierList.ModTypes.BandosBook);

    private List<ModifierList> modLists = new List<ModifierList>();

    private readonly string VIEWTAG = "HitChanceView";
    private readonly int DEFAULTCMBLVL = 99;
    private readonly int DEFAULTWEAPONTIER = 90;

    public HitChanceMVC()
    {
        //  Add all ModifierLists
        modLists.Add(prayerMods);
        modLists.Add(potionMods);
        modLists.Add(auraMods);
        modLists.Add(nihilMods);
        modLists.Add(scrimMods);
        modLists.Add(reaperMods);
        modLists.Add(quakeMods);
        modLists.Add(statWarhMods);
        modLists.Add(gstaffMods);
        modLists.Add(bandosBookMods);

        //  Sub to event to calculate and display hit chance
        EventManager.Instance.onUpdateHitChance += CalculateHitChance;
        //  Create model with default values
        model = new HitChance(DEFAULTCMBLVL, DEFAULTWEAPONTIER);

        //  null check and setup for view
        if ((view = GameObject.FindGameObjectWithTag(VIEWTAG).GetComponent<HitChanceView>()) == null)
            throw new System.Exception($"No hitchanceview gameobject found!");
    }

    ~HitChanceMVC()
    {
        EventManager.Instance.onUpdateHitChance -= CalculateHitChance;
    }

    public void Setup()
    {
        //  Create list of data to setup all UI elements
        //  MAKE SURE KEPT IN PROPER ORDER MANUALLY - SETUP SIMPLY ITERATES THROUGH EACH LIST
        List<HitChanceUISetupData> setupData = new List<HitChanceUISetupData>();
        setupData.Add(new IFSetupData(SetCombatLevel, DEFAULTCMBLVL));
        setupData.Add(new IFSetupData(SetWeaponAccTier, DEFAULTWEAPONTIER));
        setupData.Add(new DDSetupData(SetAttackStyle, AttackType.GetAttackStyleOptions()));
        setupData.Add(new DDSetupData(SetPrayer, prayerMods.GetOptionNames()));
        setupData.Add(new DDSetupData(SetPotion, potionMods.GetOptionNames()));
        setupData.Add(new DDSetupData(SetAura, auraMods.GetOptionNames()));
        setupData.Add(new DDSetupData(SetNihil, nihilMods.GetOptionNames()));
        setupData.Add(new DDSetupData(SetScrimshaw, scrimMods.GetOptionNames()));
        setupData.Add(new DDSetupData(ToggleReaper, reaperMods.GetOptionNames()));
        setupData.Add(new DDSetupData(ToggleQuake, quakeMods.GetOptionNames()));
        setupData.Add(new DDSetupData(ToggleStatWarh, statWarhMods.GetOptionNames()));
        setupData.Add(new DDSetupData(ToggleGuthixStaff, gstaffMods.GetOptionNames()));
        setupData.Add(new DDSetupData(ToggleBandosBook, bandosBookMods.GetOptionNames()));

        //  Setup the UI elements
        view.Setup(in setupData);

        CalculateHitChance();
    }

    //  Calculate the hit chance
    private void CalculateHitChance()
    {
        Debug.Log($"HIT CHANCE VS {CacheManager.SetupTab.CurrentSubBoss.name}");

        //  create player from data to feed into boss attack method
        AttackingPlayer attPl = new AttackingPlayer(new Weapon(model.AttackStyle, model.WeaponAccTier),
            model.BoostedCombatLevel(),
            model.AccuracyModifier,
            model.AffinityModifier);

        //  feed in and get value
        double val = CacheManager.SetupTab.CurrentSubBoss.HitChance(in attPl) + model.HitChanceModifier.Modifier;

        //  display as percentage to 2 decimal points
        view.SetHitChanceText(val.ToString("N2") + "%");
    }

    //  Add each effect to its proper variable based on EffectType
    private void AddEffect(AbsEffect effect)
    {
        switch (effect.EffectType)
        {
            case AbsEffect.EffectTypes.Level:
                levelMod += (LevelModifier)effect;
                break;
            case AbsEffect.EffectTypes.Accuracy:
                accMod += (AccuracyModifier)effect;
                break;
            case AbsEffect.EffectTypes.HitChance:
                hcMod += (HitChanceModifier)effect;
                break;
            case AbsEffect.EffectTypes.Affinity:
                affinityMod += (AffinityModifier)effect;
                break;
            default:
                break;
        }
    }

    //  Add up all active effects into one final effect for each category
    private void DetermineEffects()
    {
        //  Reset all effects
        accMod = new AccuracyModifier();
        levelMod = new LevelModifier();
        hcMod = new HitChanceModifier();
        affinityMod = new AffinityModifier();

        //  O(n) time since we need to either cache or recalculate all effects
        //  I chose to calculate since I have previously cached most values so this simply goes through each effect; hence O(n)
        //  List<List<AbsEffect>>
        foreach (ModifierList list in modLists)
        {
            //  List<AbsEffect>
            foreach (AbsEffect effect in list.ActiveEffects())
            {
                AddEffect(effect);
            }
        }

        //  Set each modifier and calculate hit chance
        model.SetAccuracyMod(in accMod);
        model.SetLevelMod(in levelMod);
        model.SetHitChanceMod(in hcMod);
        model.SetAffinityMod(in affinityMod);
        CalculateHitChance();
    }

    //  Set combat level
    private void SetCombatLevel(string value)
    {
        int num;

        //  Parse check - should never happen as field is set to 2 digit ints only
        if (!int.TryParse(value, out num))
            throw new System.Exception($"Cannot convert {value} to int!");

        //  If number less than one is entered, send player a reminder and reset value
        if (num < 1)
        {
            InputWarningWindow.Instance.OpenWindow($"Value must be from 1-99.");
            view.ResetCombatLevelText(model.CombatLevel + "");
        }
        //  Otherwise set the level and update hit chance
        else
        {
            model.SetCombatLevel(num);
            CalculateHitChance();
        }
    }

    //  Set weapon accuracy tier
    private void SetWeaponAccTier(string value)
    {
        int num;

        //  Parse check - should never happen as field is set to 2 digit ints only
        if (!int.TryParse(value, out num))
            throw new System.Exception($"Cannot convert {value} to int!");

        //  If number less than one is entered, send player a reminder and reset value
        if (num < 1)
        {
            InputWarningWindow.Instance.OpenWindow($"Value must be from 1-99.");
            view.ResetWeaponAccuracyText(model.WeaponAccTier + "");
        }
        //  Otherwise set the tier and update hit chance
        else
        {
            model.SetWeaponAccTier(num);
            CalculateHitChance();
        }
    }

    private void SetAttackStyle(int value)
    {
        model.SetAttackStyle((AttackType.AttackStyles)(++value));       //  ++ used as the first value is None which is not used in the Dropdown
        CalculateHitChance();
    }

    private void SetPrayer(int value)
    {
        prayerMods.SetActive(value);
        DetermineEffects();
    }

    private void SetPotion(int value)
    {
        potionMods.SetActive(value);
        DetermineEffects();
    }

    private void SetAura(int value)
    {
        auraMods.SetActive(value);
        DetermineEffects();
    }

    private void SetNihil(int value)
    {
        nihilMods.SetActive(value);
        DetermineEffects();
    }

    private void SetScrimshaw(int value)
    {
        scrimMods.SetActive(value);
        DetermineEffects();
    }

    private void ToggleReaper(int value)
    {
        reaperMods.SetActive(value);
        DetermineEffects();
    }

    private void ToggleQuake(int value)
    {
        quakeMods.SetActive(value);
        DetermineEffects();
    }

    private void ToggleStatWarh(int value)
    {
        statWarhMods.SetActive(value);
        DetermineEffects();
    }

    private void ToggleGuthixStaff(int value)
    {
        gstaffMods.SetActive(value);
        DetermineEffects();
    }

    private void ToggleBandosBook(int value)
    {
        bandosBookMods.SetActive(value);
        DetermineEffects();
    }
}