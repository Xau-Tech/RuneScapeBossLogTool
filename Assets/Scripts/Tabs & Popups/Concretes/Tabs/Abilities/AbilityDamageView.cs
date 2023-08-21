using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityDamageView : MonoBehaviour
{
    private string m_WeaponTierValue;
    private string m_VisibleLevelValue;
    private string m_EquipmentBonusValue;
    [SerializeField] private InputField m_WeaponTierInputField;
    [SerializeField] private InputField m_VisibleLevelInputField;
    [SerializeField] private InputField m_EquipmentBonusInputField;
    [SerializeField] private Toggle m_ReaperCrewToggle;

    private void Awake()
    {
        Player_Ability pa = Player_Ability.Instance;
        m_WeaponTierInputField.text = pa.WeaponDamageTier.ToString();
        m_VisibleLevelInputField.text = pa.BoostedCombatLevel.ToString();
        m_EquipmentBonusInputField.text = pa.EquipmentBonus.ToString();
    }

    private void OnEnable()
    {
        m_WeaponTierInputField.onEndEdit.AddListener(WeaponTierInputField_OnEndEdit);
        m_VisibleLevelInputField.onEndEdit.AddListener(VisibleLevelInputField_OnEndEdit);
        m_EquipmentBonusInputField.onEndEdit.AddListener(EquipmentBonusInputField_OnEndEdit);
        m_ReaperCrewToggle.onValueChanged.AddListener(ReaperCrewToggle_OnValueChanged);
    }

    private void OnDisable()
    {
        m_WeaponTierInputField.onEndEdit.RemoveAllListeners();
        m_VisibleLevelInputField.onEndEdit.RemoveAllListeners();
        m_EquipmentBonusInputField.onEndEdit.RemoveAllListeners();
        m_ReaperCrewToggle.onValueChanged.RemoveAllListeners();
    }

    private void WeaponTierInputField_OnEndEdit(string value)
    {
        int num = int.Parse(value);

        if(num < 1 || num > 120)
        {
            m_WeaponTierInputField.text = m_WeaponTierValue;
        }
        else
        {
            m_WeaponTierValue = value;
            Player_Ability.Instance.WeaponDamageTier = (byte)num;
        }
    }

    private void VisibleLevelInputField_OnEndEdit(string value)
    {
        int num = int.Parse(value);

        if(num < 1)
        {
            m_VisibleLevelInputField.text = m_VisibleLevelValue;
        }
        else
        {
            m_VisibleLevelValue = value;
            Player_Ability.Instance.BoostedCombatLevel = (byte)num;
        }
    }

    private void EquipmentBonusInputField_OnEndEdit(string value)
    {
        int num = int.Parse(value);

        if (num < 0)
        {
            m_EquipmentBonusInputField.text = m_EquipmentBonusValue;
        }
        else
        {
            m_EquipmentBonusValue = value;
            Player_Ability.Instance.EquipmentBonus = (byte)num;
        }
    }

    private void ReaperCrewToggle_OnValueChanged(bool flag)
    {
        int deltaEquipmentBonus = flag ? 12 : -12;
        Player_Ability.Instance.EquipmentBonus += (byte)deltaEquipmentBonus;
    }
}