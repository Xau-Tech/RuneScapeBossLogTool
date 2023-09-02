using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityResultButton : MonoBehaviour
{
    [SerializeField] private Color m_SortedFieldHighlight;
    [SerializeField] private Text m_AbilityNameText;
    [SerializeField] private Text m_MinDamageText;
    [SerializeField] private Text m_MaxDamageText;
    [SerializeField] private Text m_MinDpsText;
    [SerializeField] private Text m_MaxDpsText;
    [SerializeField] private Image m_CritCapImage;

    public void Set(AbilityResult abilResult)
    {
        m_AbilityNameText.text = abilResult.Name;
        m_MinDamageText.text = $"\tMin: {abilResult.Min}";
        m_MaxDamageText.text = $"Max: {abilResult.Max}";
        m_MinDpsText.text = $"\tMin Dps: {abilResult.MinDps.ToString("N0")}";
        m_MaxDpsText.text = $"Max Dps: {abilResult.MaxDps.ToString("N0")}";
        m_CritCapImage.enabled = abilResult.CritCapped;
    }
}
