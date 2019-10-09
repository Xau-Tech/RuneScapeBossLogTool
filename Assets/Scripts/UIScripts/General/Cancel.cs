using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cancel : MonoBehaviour
{
    [SerializeField]
    GameObject m_CurrentWindowToClose;

    public void OnClick()
    {
        m_CurrentWindowToClose.SetActive(false);
        UIController.uicontroller.m_ClickBlocker.SetActive(false);

        switch (ProgramState.CurrentState)
        {
            case ProgramState.states.Drops:
                UIController.uicontroller.m_DropsPanel.SetActive(true);
                break;
            case ProgramState.states.Logs:
                UIController.uicontroller.m_LogsPanel.SetActive(true);
                break;
            case ProgramState.states.Setup:
                UIController.uicontroller.m_SetupPanel.SetActive(true);
                break;
            default:
                break;
        }
    }
}
