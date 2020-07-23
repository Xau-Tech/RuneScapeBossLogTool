using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//  Script to swap between the program's tabs - ie Drops, Logs, Setup
public class TabChanger : MonoBehaviour
{
    private enum Tabs { Drops, Logs, Setup };
    [SerializeField] private GameObject dropsPanel;
    [SerializeField] private GameObject logsPanel;
    [SerializeField] private GameObject setupPanel;

    private void SelectNewTab()
    {
        dropsPanel.SetActive(false);
        logsPanel.SetActive(false);
        setupPanel.SetActive(false);
    }

    private bool IsClickedTabActive(in Tabs tabClicked)
    {
        if (tabClicked.ToString().CompareTo(ProgramState.CurrentState.ToString()) == 0)
            return true;
        else
            return false;
    }

    public void SelectDropsTab()
    {
        if (IsClickedTabActive(Tabs.Drops))
            return;

        SelectNewTab();

        dropsPanel.SetActive(true);
        ProgramState.CurrentState = ProgramState.states.Drops;
        EventManager.Instance.TabChanged();
    }

    public void SelectLogsTab()
    {
        if (IsClickedTabActive(Tabs.Logs))
            return;

        SelectNewTab();

        logsPanel.SetActive(true);
        ProgramState.CurrentState = ProgramState.states.Logs;
        EventManager.Instance.TabChanged();
    }

    public void SelectSetupTab()
    {
        if (IsClickedTabActive(Tabs.Setup))
            return;

        SelectNewTab();

        setupPanel.SetActive(true);
        ProgramState.CurrentState = ProgramState.states.Setup;
        EventManager.Instance.TabChanged();
    }
}
