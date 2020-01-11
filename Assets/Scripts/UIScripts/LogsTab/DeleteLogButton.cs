using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteLogButton : MonoBehaviour
{
    public void OnClick()
    {
        //  This boss has no current logs
        if(DataController.dataController.BossLogsDictionaryClass.GetBossLogNamesList(
            DataController.dataController.CurrentBoss).Count == 0)
        {
            EventManager.manager.InputWarningOpen("There are currently no logs for this boss to delete!");
        }
        else
        {
            ProgramState.CurrentState = ProgramState.states.DeleteLog;
            EventManager.manager.ConfirmOpen("Are you sure you want to delete this log?");
        }
    }
}
