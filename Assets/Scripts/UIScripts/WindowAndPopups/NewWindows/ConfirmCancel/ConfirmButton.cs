using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfirmButton : MonoBehaviour
{
    private CloseWindow m_CloseWindow;

    private void Awake()
    {
        m_CloseWindow = GetComponentInParent<CloseWindow>();
    }

    public void OnClick()
    {
        //  Delete the selected log
        if (ProgramState.CurrentState == ProgramState.states.DeleteLog)
        {
            DataController.Instance.BossLogsDictionary.RemoveLog
                (DataController.Instance.CurrentBoss, DataController.Instance.CurrentLogTabLog);

            DataController.Instance.HasUnsavedData = true;
            m_CloseWindow.Close();
            EventManager.Instance.LogDeleted();
        }
        //  Exit the program
        else if (ProgramState.CurrentState == ProgramState.states.Exit)
        {
            ProgramControl.Instance.ConfirmQuit = true;
            ProgramControl.Instance.CloseProgram();
        }
    }
}
