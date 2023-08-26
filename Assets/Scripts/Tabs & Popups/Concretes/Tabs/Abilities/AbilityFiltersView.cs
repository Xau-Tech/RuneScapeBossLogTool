using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

/// <summary>
/// Handles the toggles for each AbilityType
/// I wanted the user to be able to turn each type on and off and to keep things more modular
/// </summary>
public class AbilityFiltersView : MonoBehaviour
{
    [SerializeField] private Dropdown m_CombatStyleDropdown, m_WeaponStyleDropdown;
    [SerializeField] private Toggle m_BasicsToggle, m_ThreshToggle, m_UltToggle, m_SpecToggle;
    [SerializeField] private Text m_CombatSkillLevelText;
    private Dictionary<Toggle, AbilityInfo.AbilityTypeCategory> m_ToggleToAbilTypeDict;
    private WeaponStyleCriteria m_WeaponStyleCriteria;
    private CombatStyleCriteria m_CombatStyleCriteria;

    //  Monobehavior methods

    private void Awake()
    {
        //  Instantiate AbilityInfo and use to populate UI
        AbilityInfo abilInfo = new();
        m_CombatStyleDropdown.AddOptions(abilInfo.CombatStyles);
        m_WeaponStyleDropdown.AddOptions(abilInfo.WeaponStyles);
        CombatSkillLevelText_Update(m_CombatStyleDropdown.options[0].text);

        //  Set criteria to starting states
        m_WeaponStyleCriteria = new((AbilityInfo.WeaponStyle)m_WeaponStyleDropdown.value);
        m_CombatStyleCriteria = new((AbilityInfo.CombatStyle)m_CombatStyleDropdown.value);

        //  Create a simple dictionary to link toggles to their respective AbilityType
        m_ToggleToAbilTypeDict = new();
        m_ToggleToAbilTypeDict.Add(m_BasicsToggle, AbilityInfo.AbilityTypeCategory.Basic);
        m_ToggleToAbilTypeDict.Add(m_ThreshToggle, AbilityInfo.AbilityTypeCategory.Threshold);
        m_ToggleToAbilTypeDict.Add(m_UltToggle, AbilityInfo.AbilityTypeCategory.Ultimate);
        m_ToggleToAbilTypeDict.Add(m_SpecToggle, AbilityInfo.AbilityTypeCategory.Spec);
    }

    private void OnEnable()
    {
        m_CombatStyleDropdown.onValueChanged.AddListener(CombatStyleDropdown_OnValueChanged);
        m_WeaponStyleDropdown.onValueChanged.AddListener(WeaponStyleDropdown_OnValueChanged);
        m_BasicsToggle.onValueChanged.AddListener(AbilityTypeToggles_OnValueChanged);
        m_ThreshToggle.onValueChanged.AddListener(AbilityTypeToggles_OnValueChanged);
        m_UltToggle.onValueChanged.AddListener(AbilityTypeToggles_OnValueChanged);
        m_SpecToggle.onValueChanged.AddListener(AbilityTypeToggles_OnValueChanged);
    }

    private void OnDisable()
    {
        m_CombatStyleDropdown.onValueChanged.RemoveAllListeners();
        m_WeaponStyleDropdown.onValueChanged.RemoveAllListeners();
        m_BasicsToggle.onValueChanged.RemoveAllListeners();
        m_ThreshToggle.onValueChanged.RemoveAllListeners();
        m_UltToggle.onValueChanged.RemoveAllListeners();
        m_SpecToggle.onValueChanged.RemoveAllListeners();
    }

    /// <summary>
    /// Creates a List of AbilityType enums used to create a Criteria filter object
    /// </summary>
    /// <returns>List of AbilityTypes</returns>
    private List<AbilityInfo.AbilityTypeCategory> GenerateAbilityTypeList()
    {
        List<AbilityInfo.AbilityTypeCategory> retList = new();

        foreach(var kvp in m_ToggleToAbilTypeDict)
        {
            if (kvp.Key.isOn)
            {
                retList.Add(m_ToggleToAbilTypeDict[kvp.Key]);
            }
        }

        return retList;
    }

    private void CombatStyleDropdown_OnValueChanged(int value)
    {
        m_CombatStyleCriteria = new((AbilityInfo.CombatStyle)value);
        CombatSkillLevelText_Update(m_CombatStyleDropdown.options[value].text);
        EventManager.Instance.AbilityInputChanged();
    }

    private void WeaponStyleDropdown_OnValueChanged(int value)
    {
        m_WeaponStyleCriteria = new((AbilityInfo.WeaponStyle)value);
        EventManager.Instance.AbilityInputChanged();
    }

    private void AbilityTypeToggles_OnValueChanged(bool flag)
    {
        EventManager.Instance.AbilityInputChanged();
    }

    private void CombatSkillLevelText_Update(string combatStyle)
    {
        m_CombatSkillLevelText.text = $"{combatStyle} Lvl:";
    }

    public List<Ability> GenerateCurrentAbils()
    {
        AbilityTypeCriteria atc = new(GenerateAbilityTypeList());

        AndCriteria<Ability> cmbAndWeaponCriteria = new(m_CombatStyleCriteria, m_WeaponStyleCriteria);
        AndCriteria<Ability> terminalCriteria = new(cmbAndWeaponCriteria, atc);

        return AbilityDict.Instance.GetFilteredAbilities(terminalCriteria);
    }
}