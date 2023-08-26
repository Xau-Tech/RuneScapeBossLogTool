using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrayerView : MonoBehaviour
{
    [SerializeField] private Dropdown m_PrayerDropdown;

    private void Awake()
    {
        m_PrayerDropdown.AddOptions(Player_Ability.Instance.Prayer.GetPrayerNames());
    }

    private void OnEnable()
    {
        m_PrayerDropdown.onValueChanged.AddListener(PrayerDropdown_OnValueChanged);
    }

    private void OnDisable()
    {
        m_PrayerDropdown.onValueChanged.RemoveAllListeners();
    }

    private void PrayerDropdown_OnValueChanged(int value)
    {
        string newPrayer = m_PrayerDropdown.options[value].text;
        Player_Ability.Instance.Prayer.SetPrayer(newPrayer);
    }
}