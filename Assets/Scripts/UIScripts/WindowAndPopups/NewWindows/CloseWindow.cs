using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseWindow : MonoBehaviour
{
    [SerializeField]
    private GameObject m_ThisWindow;

    public void Close()
    {
        UIController.uicontroller.m_ClickBlocker.SetActive(false);
        m_ThisWindow.SetActive(false);

        if (UIController.uicontroller.m_DropsPanel.activeSelf)
            ProgramState.CurrentState = ProgramState.states.Drops;
        else if (UIController.uicontroller.m_LogsPanel.activeSelf)
            ProgramState.CurrentState = ProgramState.states.Logs;
        else
            ProgramState.CurrentState = ProgramState.states.Setup;
    }
}
