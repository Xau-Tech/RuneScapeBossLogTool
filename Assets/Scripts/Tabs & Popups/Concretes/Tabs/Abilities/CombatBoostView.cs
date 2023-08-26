using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatBoostView : MonoBehaviour
{
    [SerializeField] private Dropdown m_PotionDropdown;
    [SerializeField] private Dropdown m_AuraDropdown;

    private void Awake()
    {
        m_PotionDropdown.AddOptions(Player_Ability.Instance.CombatLevel.GetPotionOptions());
        m_AuraDropdown.AddOptions(Player_Ability.Instance.CombatLevel.GetAuraOptions());
    }

    private void OnEnable()
    {
        m_PotionDropdown.onValueChanged.AddListener(PotionDropdown_OnValueChanged);
        m_AuraDropdown.onValueChanged.AddListener(AuraDropdown_OnValueChanged);
    }

    private void OnDisable()
    {
        m_PotionDropdown.onValueChanged.RemoveAllListeners();
        m_AuraDropdown.onValueChanged.RemoveAllListeners();
    }

    private void PotionDropdown_OnValueChanged(int value)
    {
        string potionName = m_PotionDropdown.options[value].text;
        Player_Ability.Instance.CombatLevel.ApplyPotion(potionName);
    }

    private void AuraDropdown_OnValueChanged(int value)
    {
        string auraName = m_AuraDropdown.options[value].text;
        Player_Ability.Instance.CombatLevel.ApplyAura(auraName);
    }
}