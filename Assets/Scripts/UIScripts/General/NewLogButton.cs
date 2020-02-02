﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewLogButton : MonoBehaviour
{
    [SerializeField]
    private GameObject m_NewLogWindow;

    public void OnClick()
    {
        ProgramState.CurrentState = ProgramState.states.AddNewLog;
        UIController.Instance.ClickBlocker.SetActive(true);
        m_NewLogWindow.SetActive(true);
    }
}