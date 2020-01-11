using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosePopup : MonoBehaviour
{
    public void OnClick()
    {
        this.gameObject.SetActive(false);
        PopupState.currentState = PopupState.states.None;

        if(ProgramState.CurrentState == ProgramState.states.Drops ||
            ProgramState.CurrentState == ProgramState.states.Logs ||
            ProgramState.CurrentState == ProgramState.states.Setup)
        {
            UIController.uicontroller.m_ClickBlocker.SetActive(false);
        }
    }
}
