using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.UI;
using UnityEngine.Events;
using GoogleSheetsToUnity;

public class ProgramControl : MonoBehaviour
{
    public static ProgramControl Instance;

    public bool ConfirmQuit { set { m_ConfirmQuit = value; } }

    [SerializeField]
    private GameObject dataController;
    [SerializeField]
    private GameObject uiController;
    private bool m_ConfirmQuit;

    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        m_ConfirmQuit = false;
        Application.wantsToQuit += QuitCheck;


        //  Set to windowed mode at 1280x720 resolution by default
        //Screen.fullScreen = false;
        Screen.SetResolution(1280, 720, false);

        ProgramState.CurrentState = ProgramState.states.Setup;

        //  Perform setup
        EarlySetup();
    }

    private void OnDisable()
    {
        Application.wantsToQuit -= QuitCheck;
    }

    private void EarlySetup()
    {
        dataController.SetActive(true);
        uiController.SetActive(true);

        DataController.Instance.Setup();
    }

    public void LateSetup()
    {
        UIController.Instance.OnToolbarDropButtonClicked();
        EventManager.Instance.BossDropdownValueChanged();
        PopupState.currentState = PopupState.states.None;
    }

    private bool QuitCheck()
    {
        ProgramState.CurrentState = ProgramState.states.Exit;

        if (m_ConfirmQuit)
            return true;

        string exitConfirmText = "";

        //  Check if there is active droplist data
        if (DataController.Instance.DropList.data.Count != 0)
        {
            exitConfirmText += "You have an open list of drops!\n";
        }
        //  Check if there are unsaved changes
        if (DataController.Instance.HasUnsavedData)
            exitConfirmText += "You have unsaved data!\n";

        //  If there is text to confirm exit, prompt user for choice
        if (exitConfirmText != "")
        {
            exitConfirmText += "Are you sure you want to exit?";
            EventManager.Instance.ConfirmOpen(exitConfirmText);
            return false;
        }
        else
            return true;
    }

    private bool QuitCheck()
    {
        ProgramState.CurrentState = ProgramState.states.Exit;

        if (m_ConfirmQuit)
            return true;

        string exitConfirmText = "";

        //  Check if there is active droplist data
        if (DataController.Instance.DropList.data.Count != 0)
        {
            exitConfirmText += "You have an open list of drops!\n";
        }
        //  Check if there are unsaved changes
        if (DataController.Instance.HasUnsavedData)
            exitConfirmText += "You have unsaved data!\n";

        //  If there is text to confirm exit, prompt user for choice
        if (exitConfirmText != "")
        {
            exitConfirmText += "Are you sure you want to exit?";
            EventManager.Instance.ConfirmOpen(exitConfirmText);
            return false;
        }
        else
            return true;
    }

    public void Update()
    {
        //Debug.Log("Program state: " + ProgramState.CurrentState);
        //Debug.Log("Popup state: " + PopupState.currentState);
        //Debug.Log("Drop log: " + DataController.Instance.CurrentDropTabLog);
        //Debug.Log("Log log(???): " + DataController.Instance.CurrentLogTabLog);
        //Debug.Log("Current boss: " + DataController.Instance.CurrentBoss);

        //  ctrl+s to save
<<<<<<< HEAD
        if (Input.GetKey(KeyCode.LeftControl) && PopupState.currentState == PopupState.states.None)
=======
        if (Input.GetKey(KeyCode.LeftControl))
>>>>>>> 81378409feb942c55bf32e3907413a10d6ce64b5
        {
            if (Input.GetKeyDown(KeyCode.S))
                DataController.Instance.SaveBossLogData();
        }
        else if (Input.GetKeyDown(KeyCode.U))
            PopupState.currentState = PopupState.states.Saving;
    }

    //  Exit the program
    public void CloseProgram()
    {
        Application.Quit();
    }
}

//  Program states for each tab
public class ProgramState
{
    public enum states { Drops, Logs, Setup, AddToLog, AddNewLog, DeleteLog, Exit};

    public static states CurrentState;
}
