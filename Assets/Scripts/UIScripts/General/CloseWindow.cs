using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseWindow : MonoBehaviour
{
    [SerializeField]
    GameObject m_CurrentWindowToClose;

    public void OnClick()
    {
        m_CurrentWindowToClose.SetActive(false);
        UIController.uicontroller.m_ClickBlocker.SetActive(false);
    }
}
