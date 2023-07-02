using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AbilityTab : AbstractTab
{
    //  Properties & fields

    public override Enums.TabStates AssociatedTabState { get { return Enums.TabStates.Abilities; } }

    //  Monobehaviors

    protected override void OnEnable()
    {
        base.OnEnable();

        //  TESTING
        
        AbilityTypeCriteria atc = new AbilityTypeCriteria(AbilityInfo.AbilityTypeCategory.Basic);
        List<Ability> basicsList = AbilityDict.Instance.GetFilteredAbilities(atc);
        ShowAbilities("Basic abilities:", basicsList);

        WeaponStyleCriteria wsc = new WeaponStyleCriteria(AbilityInfo.WeaponStyle.Twohand);
        List<Ability> twohList = AbilityDict.Instance.GetFilteredAbilities(wsc);
        ShowAbilities("2h abilities:", twohList);

        AndCriteria<Ability> ac = new AndCriteria<Ability>(new AbilityTypeCriteria(AbilityInfo.AbilityTypeCategory.Ultimate), new WeaponStyleCriteria(AbilityInfo.WeaponStyle.Twohand));
        List<Ability> ulttwoList = AbilityDict.Instance.GetFilteredAbilities(ac);
        ShowAbilities("2h ultimates:", ulttwoList);

        //  TESTING
    }

    //  TESTING FUNCTION
    private void ShowAbilities(string message, List<Ability> abilList)
    {
        Debug.Log(message);

        foreach(Ability a in abilList)
        {
            Debug.Log(a);
        }
    }
}
