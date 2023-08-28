using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PerksView : MonoBehaviour
{
    [SerializeField] private Dropdown m_PreciseRankDropdown;
    [SerializeField] private Dropdown m_EquilibriumRankDropdown;

    private void OnEnable()
    {
        m_PreciseRankDropdown.onValueChanged.AddListener(PreciseRankDropdown_OnValueChanged);
        m_EquilibriumRankDropdown.onValueChanged.AddListener(EquilibriumRankDropdown_OnValueChanged);
    }

    private void OnDisable()
    {
        m_PreciseRankDropdown.onValueChanged.RemoveAllListeners();
        m_EquilibriumRankDropdown.onValueChanged.RemoveAllListeners();
    }

    private void PreciseRankDropdown_OnValueChanged(int value)
    {
        Player_Ability.Instance.Perks.PreciseRank = (byte)value;
    }

    private void EquilibriumRankDropdown_OnValueChanged(int value)
    {
        Player_Ability.Instance.Perks.EquilibriumRank = (byte)value;
    }
}