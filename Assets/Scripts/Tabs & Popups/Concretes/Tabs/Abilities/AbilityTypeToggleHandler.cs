using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

/// <summary>
/// Handles the toggles for each AbilityType
/// I wanted the user to be able to turn each type on and off and to keep things more modular
/// </summary>
public class AbilityTypeToggleHandler : MonoBehaviour
{
    //  Properties & fields

    public Action<List<AbilityInfo.AbilityTypeCategory>> UpdateFilterAction
    {
        set
        {
            //  If the action hasn't been set, set it and fire the event (this should be happening when the tab is first loaded)
            if (m_UpdateFilterAction == null)
            {
                m_UpdateFilterAction = value;
                Toggles_OnValueChanged(true); 
            }
        }
    }

    [SerializeField] private Toggle m_BasicsToggle, m_ThreshToggle, m_UltToggle, m_SpecToggle;
    private Dictionary<Toggle, AbilityInfo.AbilityTypeCategory> m_ToggleToAbilTypeDict;
    private Action<List<AbilityInfo.AbilityTypeCategory>> m_UpdateFilterAction;

    //  Monobehavior methods

    private void Awake()
    {
        //  Create a simple dictionary to link toggles to their respective AbilityType
        m_ToggleToAbilTypeDict = new();
        m_ToggleToAbilTypeDict.Add(m_BasicsToggle, AbilityInfo.AbilityTypeCategory.Basic);
        m_ToggleToAbilTypeDict.Add(m_ThreshToggle, AbilityInfo.AbilityTypeCategory.Threshold);
        m_ToggleToAbilTypeDict.Add(m_UltToggle, AbilityInfo.AbilityTypeCategory.Ultimate);
        m_ToggleToAbilTypeDict.Add(m_SpecToggle, AbilityInfo.AbilityTypeCategory.Spec);
    }

    private void OnEnable()
    {
        m_BasicsToggle.onValueChanged.AddListener(Toggles_OnValueChanged);
        m_ThreshToggle.onValueChanged.AddListener(Toggles_OnValueChanged);
        m_UltToggle.onValueChanged.AddListener(Toggles_OnValueChanged);
        m_SpecToggle.onValueChanged.AddListener(Toggles_OnValueChanged);
    }

    private void OnDisable()
    {
        m_BasicsToggle.onValueChanged.RemoveAllListeners();
        m_ThreshToggle.onValueChanged.RemoveAllListeners();
        m_UltToggle.onValueChanged.RemoveAllListeners();
        m_SpecToggle.onValueChanged.RemoveAllListeners();
    }

    //  Other methods

    /// <summary>
    /// Creates a List of AbilityType enums used to create a Criteria filter object
    /// </summary>
    /// <returns>List of AbilityTypes</returns>
    private List<AbilityInfo.AbilityTypeCategory> GenerateAbilityTypeList()
    {
        List<AbilityInfo.AbilityTypeCategory> retList = new();

        foreach(var kvp in m_ToggleToAbilTypeDict)
        {
            if (kvp.Key.isOn)                                   //  Check if the toggle is checked
            {
                retList.Add(m_ToggleToAbilTypeDict[kvp.Key]);   //  If so, add the respective AbilityType to the list
            }
        }

        return retList;
    }

    //  UI events

    /// <summary>
    /// Creates a List of AbilityTypes to include in the filter and sends to the AbilityTab for processing via an Action
    /// </summary>
    /// <param name="flag">State of the toggle - currently unused but the component requires the flag for its Event listener</param>
    private void Toggles_OnValueChanged(bool flag)
    {
        m_UpdateFilterAction?.Invoke(GenerateAbilityTypeList());
    }
}
