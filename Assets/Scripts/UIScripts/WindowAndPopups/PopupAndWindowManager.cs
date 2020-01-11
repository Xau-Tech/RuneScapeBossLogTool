using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupAndWindowManager : MonoBehaviour
{
    [SerializeField]
    private GameObject m_InputWarningPopup;
    [SerializeField]
    private GameObject m_ConfirmPopup;

    private void Awake()
    {
        PopupState.currentState = PopupState.states.None;
    }

    private void OnEnable()
    {
        EventManager.manager.onInputWarningOpen += InputWarningOpen;
        EventManager.manager.onConfirmOpen += ConfirmOpen;
    }

    private void OnDisable()
    {
        EventManager.manager.onInputWarningOpen -= InputWarningOpen;
        EventManager.manager.onConfirmOpen -= ConfirmOpen;
    }

    private void ConfirmOpen(string _message)
    {
        UIController.uicontroller.m_ClickBlocker.SetActive(true);
        m_ConfirmPopup.SetActive(true);

        Text t = m_ConfirmPopup.GetComponentInChildren<Text>();
        t.text = _message;
    }

    private void InputWarningOpen(string _message)
    {
        PopupState.currentState = PopupState.states.Warning;

        UIController.uicontroller.m_ClickBlocker.SetActive(true);
        m_InputWarningPopup.SetActive(true);

        Text t = m_InputWarningPopup.GetComponentInChildren<Text>();
        t.text = _message;
    }
}
public class PopupState
{
    public enum states { None, Warning, Confirm, Error };
    public static states currentState;
}
