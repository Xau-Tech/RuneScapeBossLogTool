using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupAndWindowManager : MonoBehaviour
{
    [SerializeField]
    private GameObject m_InputWarningPopup;
    [SerializeField]
    private GameObject m_ConfirmWindow;
    [SerializeField]
    private GameObject m_ErrorPopup;

    private void Awake()
    {
        PopupState.currentState = PopupState.states.None;
    }

    private void OnEnable()
    {
        EventManager.Instance.onInputWarningOpen += InputWarningOpen;
        EventManager.Instance.onConfirmOpen += ConfirmOpen;
        EventManager.Instance.onErrorOpen += ErrorOpen;
    }

    private void OnDisable()
    {
        EventManager.Instance.onInputWarningOpen -= InputWarningOpen;
        EventManager.Instance.onConfirmOpen -= ConfirmOpen;
        EventManager.Instance.onErrorOpen -= ErrorOpen;
    }

    private void ConfirmOpen(string _message)
    {
        UIController.Instance.ClickBlocker.SetActive(true);
        m_ConfirmWindow.SetActive(true);

        Text t = m_ConfirmWindow.GetComponentInChildren<Text>();
        t.text = _message;
    }

    private void InputWarningOpen(string _message)
    {
        PopupState.currentState = PopupState.states.Warning;

        UIController.Instance.ClickBlocker.SetActive(true);
        m_InputWarningPopup.SetActive(true);

        Text t = m_InputWarningPopup.GetComponentInChildren<Text>();
        t.text = _message;
    }

    private void ErrorOpen(string _message)
    {
        PopupState.currentState = PopupState.states.Error;

        UIController.Instance.ClickBlocker.SetActive(true);
        m_ErrorPopup.SetActive(true);

        Text t = m_ErrorPopup.GetComponentInChildren<Text>();
        t.text = _message;
    }
}
public class PopupState
{
    public enum states { None, Warning, Confirm, Error, Loading, Saving };
    private static states m_CurrentState;

    public static states currentState
    {
        get { return m_CurrentState; }
        set
        {
            //  State is not save and not load
            if(currentState != states.Saving && currentState != states.Loading)
            {
                //  New state is save or load
                if (value == states.Loading || value == states.Saving)
                {
                    //input restriction start
                    UIController.Instance.InputRestrictStart(value.ToString() + "...");
                }
            }
            //  State is save or load
            else if (currentState == states.Saving || currentState == states.Loading)
            {
                //  New state is neither save or load
                if (value != states.Loading && value != states.Saving)
                {
                    //input restrict end
                    UIController.Instance.InputRestrictEnd();
                }
            }

            m_CurrentState = value;
        }
    }
}
