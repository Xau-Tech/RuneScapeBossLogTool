using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using System.Diagnostics;

/// <summary>
/// Script for handling UI elements and events of the Abilities tab
/// </summary>
public class AbilityTab : AbstractTab
{
    public override Enums.TabStates AssociatedTabState { get { return Enums.TabStates.Abilities; } }

    [SerializeField] private AbilityFiltersView m_AbilityFiltersView;
    [SerializeField] private AbilityScrollList m_AbilityScrollList;
    [SerializeField] private Dropdown m_SortOptionsDropdown;
    private AbilityResultSortOptions m_SortOptions;
    private IComparer<AbilityResult> m_CurrentSortOption;
    private AbilityResults m_CachedAbilResults;

    private void Awake()
    {
        m_CachedAbilResults = new();
        m_SortOptions = new();
        m_SortOptionsDropdown.AddOptions(m_SortOptions.SortOptions);
        m_CurrentSortOption = m_SortOptions.GetComparer(0);
        CalculateDamages();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        EventManager.Instance.onAbilityInputChanged += CalculateDamages;
        m_SortOptionsDropdown.onValueChanged.AddListener(SortOptionsDropdown_OnValueChanged);
    }

    private void OnDisable()
    {
        EventManager.Instance.onAbilityInputChanged -= CalculateDamages;
        m_SortOptionsDropdown.onValueChanged.RemoveAllListeners();
    }

    private void CalculateDamages()
    {
        Stopwatch sw = Stopwatch.StartNew();

        List<Ability> abilList = m_AbilityFiltersView.GenerateCurrentAbils();
        DamageCalculationChain chain = new();
        AbilityResults abilResults = new();

        foreach (Ability abil in abilList)
        {
            DamageCalcPassthrough dcp = chain.CalculateDamage(in abil);

            abilResults.Add(new AbilityResult(abil.Name, dcp.Min, dcp.Min + dcp.Var, abil.Length));
        }

        SortAndDisplayDamages(abilResults);

        sw.Stop();
        UnityEngine.Debug.Log("Damage calculation & display time: " + sw.Elapsed);
    }

    private void SortAndDisplayDamages(AbilityResults abilResults)
    {
        abilResults.Sort(m_CurrentSortOption);
        m_CachedAbilResults = abilResults;
        m_AbilityScrollList.Display(abilResults);
    }

    private void SortOptionsDropdown_OnValueChanged(int value)
    {
        m_CurrentSortOption = m_SortOptions.GetComparer(value);
        SortAndDisplayDamages(m_CachedAbilResults);
    }
}