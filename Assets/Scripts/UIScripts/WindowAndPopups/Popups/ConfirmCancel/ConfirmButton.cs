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
        if(ProgramState.CurrentState == ProgramState.states.DeleteLog)
        {
            DataController.dataController.BossLogsDictionaryClass.RemoveLog(DataController.dataController.CurrentBoss,
                DataController.dataController.CurrentLogTabLog);

            m_CloseWindow.Close();

            EventManager.manager.LogDeleted();
        }
    }
}
