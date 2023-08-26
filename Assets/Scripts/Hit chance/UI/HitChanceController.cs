using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitChanceController
{
    //  Properties & fields

    private readonly HitChance _model;
    private readonly HitChanceView _view;
    private BossCombatData _currentSubBoss;

    //  LevelModifier and related lists
    private LevelModifier _levelMod = new();
    private readonly ModifierList _prayerMods = new(Enums.ModTypes.Prayer);
    private readonly ModifierList _potionMods = new(Enums.ModTypes.Potion);
    private readonly ModifierList _auraMods = new(Enums.ModTypes.Aura);

    //  AccuracyModifier and related lists
    private AccuracyModifier _accMod = new();
    private readonly ModifierList _nihilMods = new(Enums.ModTypes.Nihil);
    private readonly ModifierList _scrimMods = new(Enums.ModTypes.Scrimshaw);

    //  HitChanceModifier and related lists
    private HitChanceModifier _hcMod = new();
    private readonly ModifierList _reaperMods = new(Enums.ModTypes.Reaper);

    //  AffinityModifier and related lists
    private AffinityModifier _affinityMod = new();
    private readonly ModifierList _quakeMods = new(Enums.ModTypes.Quake);
    private readonly ModifierList _statWarhMods = new(Enums.ModTypes.StatWarh);
    private readonly ModifierList _gstaffMods = new(Enums.ModTypes.Gstaff);
    private readonly ModifierList _bandosBookMods = new(Enums.ModTypes.BandosBook);

    private readonly List<ModifierList> _modLists = new();

    private readonly string _VIEWTAG = "HitChanceView";
    private readonly int _DEFAULTCMBLVL = 99;
    private readonly int _DEFAULTWEAPONTIER = 90;

    //  Constructor

    public HitChanceController()
    {
        //  Add all modifier lists
        _modLists.Add(_prayerMods);
        _modLists.Add(_potionMods);
        _modLists.Add(_auraMods);
        _modLists.Add(_nihilMods);
        _modLists.Add(_scrimMods);
        _modLists.Add(_reaperMods);
        _modLists.Add(_quakeMods);
        _modLists.Add(_statWarhMods);
        _modLists.Add(_gstaffMods);
        _modLists.Add(_bandosBookMods);

        _model = new HitChance(_DEFAULTCMBLVL, _DEFAULTWEAPONTIER);
        _view = GameObject.FindGameObjectWithTag(_VIEWTAG).GetComponent<HitChanceView>();
    }

    //  Methods

    public void Setup()
    {
        //  Create list of data to setup all UI elements
        //  MAKE SURE KEPT IN PROPER ORDER MANUALLY - SETUP SIMPLY ITERATES THROUGH EACH LIST
        List<HitChanceUISetupData> setupData = new();
        setupData.Add(new HitChanceIFData(SetCombatLevel, _DEFAULTCMBLVL));
        setupData.Add(new HitChanceIFData(SetWeaponAccTier, _DEFAULTWEAPONTIER));
        setupData.Add(new HitChanceDDData(SetAttackStyle, AttackType.GetAttackStyles()));
        setupData.Add(new HitChanceDDData(SetPrayer, _prayerMods.OptionNames()));
        setupData.Add(new HitChanceDDData(SetPotion, _potionMods.OptionNames()));
        setupData.Add(new HitChanceDDData(SetAura, _auraMods.OptionNames()));
        setupData.Add(new HitChanceDDData(SetNihil, _nihilMods.OptionNames()));
        setupData.Add(new HitChanceDDData(SetScrimshaw, _scrimMods.OptionNames()));
        setupData.Add(new HitChanceDDData(ToggleReaper, _reaperMods.OptionNames()));
        setupData.Add(new HitChanceDDData(ToggleQuake, _quakeMods.OptionNames()));
        setupData.Add(new HitChanceDDData(ToggleStatWarh, _statWarhMods.OptionNames()));
        setupData.Add(new HitChanceDDData(ToggleGuthixStaff, _gstaffMods.OptionNames()));
        setupData.Add(new HitChanceDDData(ToggleBandosBook, _bandosBookMods.OptionNames()));
        
        //  Setup the UI elements
        _view.Setup(setupData);
    }

    public void CalculateHitChance(BossCombatData bossCombatData)
    {
        this._currentSubBoss = bossCombatData;

        //  Create player from data to feed into boss attack method
        AttackingPlayer ap = new(new Weapon(_model.AttackStyle, _model.WeaponAccTier),
            _model.BoostedCombatLevel(),
            _model.AccuracyMod,
            _model.AffinityMod);

        double val = _currentSubBoss.HitChance(ap) + _model.HitChanceMod.Modifier;
        _view.SetHitChanceText(val.ToString("N2") + "%");
    }

    private void AddEffect(AbsEffect effect)
    {
        switch (effect.EffectType)
        {
            case Enums.EffectTypes.Level:
                _levelMod += (LevelModifier)effect;
                break;
            case Enums.EffectTypes.Accuracy:
                _accMod += (AccuracyModifier)effect;
                break;
            case Enums.EffectTypes.HitChance:
                _hcMod += (HitChanceModifier)effect;
                break;
            case Enums.EffectTypes.Affinity:
                _affinityMod += (AffinityModifier)effect;
                break;
            default:
                break;
        }
    }

    private void DetermineEffects()
    {
        //  Reset all effects
        _accMod = new AccuracyModifier();
        _levelMod = new LevelModifier();
        _hcMod = new HitChanceModifier();
        _affinityMod = new AffinityModifier();

        foreach(ModifierList list in _modLists)
        {
            foreach(AbsEffect effect in list.ActiveEffects())
            {
                AddEffect(effect);
            }
        }

        //  Set each modifier and calculate hit chance
        _model.AccuracyMod = _accMod;
        _model.LvlMod = _levelMod;
        _model.HitChanceMod = _hcMod;
        _model.AffinityMod = _affinityMod;
        CalculateHitChance(_currentSubBoss);
    }

    private void SetCombatLevel(string value)
    {
        int.TryParse(value, out int num);

        if (num < 1)
        {
            PopupManager.Instance.ShowNotification($"Value must be from 1-99.");
            _view.ResetCombatLevelText(_model.CombatLevel + "");
        }
        else
        {
            _model.CombatLevel = num;
            CalculateHitChance(_currentSubBoss);
        }
    }

    private void SetWeaponAccTier(string value)
    {
        int.TryParse(value, out int num);

        if (num < 1)
        {
            PopupManager.Instance.ShowNotification($"Value must be from 1-99.");
            _view.ResetWeaponAccuracyText(_model.WeaponAccTier + "");
        }
        else
        {
            _model.WeaponAccTier = num;
            CalculateHitChance(_currentSubBoss);
        }
    }

    private void SetAttackStyle(int value)
    {
        _model.AttackStyle = ((Enums.AttackStyles)(++value));       //  ++ used as the first value is None which is not used in the Dropdown
        CalculateHitChance(_currentSubBoss);
    }

    private void SetPrayer(int value)
    {
        _prayerMods.SetActive(value);
        DetermineEffects();
    }

    private void SetPotion(int value)
    {
        _potionMods.SetActive(value);
        DetermineEffects();
    }

    private void SetAura(int value)
    {
        _auraMods.SetActive(value);
        DetermineEffects();
    }

    private void SetNihil(int value)
    {
        _nihilMods.SetActive(value);
        DetermineEffects();
    }

    private void SetScrimshaw(int value)
    {
        _scrimMods.SetActive(value);
        DetermineEffects();
    }

    private void ToggleReaper(int value)
    {
        _reaperMods.SetActive(value);
        DetermineEffects();
    }

    private void ToggleQuake(int value)
    {
        _quakeMods.SetActive(value);
        DetermineEffects();
    }

    private void ToggleStatWarh(int value)
    {
        _statWarhMods.SetActive(value);
        DetermineEffects();
    }

    private void ToggleGuthixStaff(int value)
    {
        _gstaffMods.SetActive(value);
        DetermineEffects();
    }

    private void ToggleBandosBook(int value)
    {
        _bandosBookMods.SetActive(value);
        DetermineEffects();
    }
}
