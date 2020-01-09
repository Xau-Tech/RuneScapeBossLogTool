using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogDropdownPopulate : MonoBehaviour
{
    private Dropdown m_ThisDropdown;

    private void Awake()
    {
        m_ThisDropdown = this.gameObject.GetComponent<Dropdown>();
    }

    private void OnEnable()
    {
        EventManager.manager.onBossDropdownValueChanged += PopulateDropdown;
        EventManager.manager.onLogAdded += PopulateDropdown;
        EventManager.manager.onTabSwitched += PopulateDropdown;
    }

    private void OnDisable()
    {
        EventManager.manager.onBossDropdownValueChanged -= PopulateDropdown;
        EventManager.manager.onLogAdded -= PopulateDropdown;
        EventManager.manager.onTabSwitched -= PopulateDropdown;
    }

    private void PopulateDropdown()
    {
        m_ThisDropdown.ClearOptions();

        List<string> logNames;

        if ((logNames = DataController.dataController.BossLogsDictionaryClass.
            GetBossLogNamesList(UIController.uicontroller.GetCurrentBoss())).Count == 0)
        {
            logNames.Add("-Create new log-");
        }

        m_ThisDropdown.AddOptions(logNames);

        EventManager.manager.LogsPopulated();
    }
}
