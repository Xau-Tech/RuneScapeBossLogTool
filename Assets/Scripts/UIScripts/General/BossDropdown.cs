using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossDropdown : MonoBehaviour
{
    private Dropdown m_ThisDropdown;

    private void Awake()
    {
        m_ThisDropdown = this.gameObject.GetComponent<Dropdown>();
    }

    private void OnEnable()
    {
        EventManager.Instance.onBossInfoLoaded += PopulateDropdown;
    }

    private void OnDisable()
    {
        EventManager.Instance.onBossInfoLoaded -= PopulateDropdown;
    }

    private void PopulateDropdown()
    {
        m_ThisDropdown.ClearOptions();
        m_ThisDropdown.AddOptions(DataController.Instance.BossInfoList.GetBossNames());
        DataController.Instance.CurrentBoss = m_ThisDropdown.options[m_ThisDropdown.value].text;
    }

    public void OnValueChanged()
    {
        DataController.Instance.CurrentBoss = m_ThisDropdown.options[m_ThisDropdown.value].text;
        EventManager.Instance.BossDropdownValueChanged();
    }
}
