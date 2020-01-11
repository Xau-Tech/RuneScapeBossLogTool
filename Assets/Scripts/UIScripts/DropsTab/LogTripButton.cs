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
        if (DataController.dataController.BossLogsDictionaryClass.GetBossLogNamesList(
            DataController.dataController.CurrentBoss).Count == 0)
        {
            EventManager.manager.InputWarningOpen("You must create a log for this boss before attempting to log this trip!");
        }
        //  Continue if log exists
        else
        {
            ProgramState.CurrentState = ProgramState.states.AddToLog;
            UIController.uicontroller.m_ClickBlocker.SetActive(true);
            m_AddToLogWindow.SetActive(true);
        }
    }
}
