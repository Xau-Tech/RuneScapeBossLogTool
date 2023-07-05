using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

/// <summary>
/// Script for handling UI elements and events of the Abilities tab
/// </summary>
public class AbilityTab : AbstractTab
{
    //  Properties & fields

    public override Enums.TabStates AssociatedTabState { get { return Enums.TabStates.Abilities; } }

    [SerializeField] private Dropdown m_CombatStyleDropdown, m_WeaponStyleDropdown;
    [SerializeField] private AbilityTypeToggleHandler m_AbilityTypeHandler;
    private AbilityTypeCriteria m_AbilityTypeCriteria;
    private WeaponStyleCriteria m_WeaponStyleCriteria;
    private CombatStyleCriteria m_CombatStyleCriteria;

    //  Monobehaviors

    private void Awake()
    {
        //  Instantiate AbilityInfo and use to populate UI
        AbilityInfo abilInfo = new();
        m_CombatStyleDropdown.AddOptions(abilInfo.CombatStyles);
        m_WeaponStyleDropdown.AddOptions(abilInfo.WeaponStyles);

        //  Set criteria to starting states
        m_WeaponStyleCriteria = new((AbilityInfo.WeaponStyle)m_WeaponStyleDropdown.value);
        m_CombatStyleCriteria = new((AbilityInfo.CombatStyle)m_CombatStyleDropdown.value);

        //  Enable script that handles AbilityType toggles and set up its callback Action
        m_AbilityTypeHandler.enabled = true;
        m_AbilityTypeHandler.UpdateFilterAction = AbilTypeToggleHandler_OnUpdate;
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        m_CombatStyleDropdown.onValueChanged.AddListener(CombatStyleDropdown_OnValueChanged);
        m_WeaponStyleDropdown.onValueChanged.AddListener(WeaponStyleDropdown_OnValueChanged);
    }

    private void OnDisable()
    {
        m_CombatStyleDropdown.onValueChanged.RemoveAllListeners();
        m_WeaponStyleDropdown.onValueChanged.RemoveAllListeners();
    }

    //  TESTING FUNCTION
    private void ShowAbilities(List<Ability> abilList)
    {
        string message = DateTime.Now.ToShortTimeString();

        foreach(Ability a in abilList)
        {
            message += $"\n{a}";
        }

        Debug.Log(message);
    }

    //  Other methods

    /// <summary>
    /// Creates a new AbilityTypeCriteria and triggers the new filter to update.
    /// Passed as an Action to the AbilityTypeToggleHandler script
    /// </summary>
    /// <param name="abilTypeList">List of AbilityTypes used to create the Criteria object</param>
    private void AbilTypeToggleHandler_OnUpdate(List<AbilityInfo.AbilityTypeCategory> abilTypeList)
    {
        m_AbilityTypeCriteria = new(abilTypeList);
        GenerateAndPrintFilteredAbils();
    }

    private void GenerateAndPrintFilteredAbils()
    {
        //  TODO
        //  TEMPORARY WHILE PRINTING TEXT TO DEBUG
        //  LATER CHANGE TO UPDATE UI (No damage recalculation needed since just changes in which abils are shown)
        AndCriteria<Ability> cmbAndWeaponCriteria = new(m_CombatStyleCriteria, m_WeaponStyleCriteria);
        AndCriteria<Ability> terminalCriteria = new(cmbAndWeaponCriteria, m_AbilityTypeCriteria);

        List<Ability> abilList = AbilityDict.Instance.GetFilteredAbilities(terminalCriteria);
        ShowAbilities(abilList);
    }

    //  UI events

    private void CombatStyleDropdown_OnValueChanged(int value)
    {
        m_CombatStyleCriteria = new((AbilityInfo.CombatStyle)value);
        GenerateAndPrintFilteredAbils();
    }

    private void WeaponStyleDropdown_OnValueChanged(int value)
    {
        m_WeaponStyleCriteria = new((AbilityInfo.WeaponStyle)value);
        GenerateAndPrintFilteredAbils();
    }
}