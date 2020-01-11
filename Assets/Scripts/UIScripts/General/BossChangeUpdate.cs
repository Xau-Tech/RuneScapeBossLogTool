using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossChangeUpdate : MonoBehaviour
{
    private Dropdown m_ThisDropdown;

    private void Awake()
    {
        m_ThisDropdown = this.gameObject.GetComponent<Dropdown>();
    }

    public void OnValueChanged()
    {
        DataController.dataController.CurrentBoss = m_ThisDropdown.options[m_ThisDropdown.value].text;
        EventManager.manager.BossDropdownValueChanged();
    }
}
