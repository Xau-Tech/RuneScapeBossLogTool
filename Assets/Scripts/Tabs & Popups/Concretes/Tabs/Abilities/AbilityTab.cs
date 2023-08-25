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

    private void Awake()
    {
        CalculateDamages();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        EventManager.Instance.onAbilityInputChanged += CalculateDamages;
    }

    private void OnDisable()
    {
        EventManager.Instance.onAbilityInputChanged -= CalculateDamages;
    }

    private void CalculateDamages()
    {
        Stopwatch sw = Stopwatch.StartNew();

        List<Ability> abilList = m_AbilityFiltersView.GenerateCurrentAbils();
        List<AbilityDamageResults> results = new();
        DamageCalculationChain chain = new();

        foreach (Ability abil in abilList)
        {
            DamageCalcPassthrough dcp = chain.CalculateDamage(in abil);
            results.Add(new()
            {
                Name = abil.Name,
                Min = dcp.Min,
                Max = dcp.Min + dcp.Var
            });
        }

        m_AbilityScrollList.Display(results);

        sw.Stop();
        UnityEngine.Debug.Log("Damage calculation & display time: " + sw.Elapsed);
    }
}

public struct AbilityDamageResults
{
    public string Name;
    public double Min;
    public double Max;
}