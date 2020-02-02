using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteLogButton : MonoBehaviour
{
    public void OnClick()
    {
        //  This boss has no current logs
        if(DataController.Instance.BossLogsDictionary.GetBossLogNamesList(DataController.Instance.CurrentBoss).Count == 0)
        {
            EventManager.Instance.InputWarningOpen("There are currently no logs to delete!");
        }
        else
        {
            ProgramState.CurrentState = ProgramState.states.DeleteLog;
            EventManager.Instance.ConfirmOpen("Are you sure you want to delete this log?");
        }
    }
}
