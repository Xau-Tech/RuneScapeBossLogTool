using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogTripButton : MonoBehaviour
{
    [SerializeField]
    private GameObject m_AddToLogWindow;

    public void OnClick()
    {
        //  Make sure a log exists for this boss first
        if (DataController.Instance.BossLogsDictionary.GetBossLogNamesList(
            DataController.Instance.CurrentBoss).Count == 0)
        {
            EventManager.Instance.InputWarningOpen("You must create a log for this boss before attempting to log this trip!");
        }
        //  Continue if log exists
        else
        {
            ProgramState.CurrentState = ProgramState.states.AddToLog;
            UIController.Instance.ClickBlocker.SetActive(true);
            m_AddToLogWindow.SetActive(true);
        }
    }
}
