using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseWindow : MonoBehaviour
{
    [SerializeField]
    private GameObject m_ThisWindow;

    public void Close()
    {
        UIController.Instance.ClickBlocker.SetActive(false);
        m_ThisWindow.SetActive(false);

        if (UIController.Instance.DropsPanel.activeSelf)
            ProgramState.CurrentState = ProgramState.states.Drops;
        else if (UIController.Instance.LogsPanel.activeSelf)
            ProgramState.CurrentState = ProgramState.states.Logs;
        else
            ProgramState.CurrentState = ProgramState.states.Setup;
    }
}
