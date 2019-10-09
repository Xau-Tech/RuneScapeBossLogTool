using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NewWindowButton : MonoBehaviour
{
    [SerializeField]
    private GameObject m_NewWindowToOpen;

    public void OnClick()
    {
        UIController.uicontroller.m_ClickBlocker.SetActive(true);
        m_NewWindowToOpen.SetActive(true);
    }
}
