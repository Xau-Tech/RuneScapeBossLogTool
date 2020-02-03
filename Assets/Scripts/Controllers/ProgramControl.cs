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

    [SerializeField]
    private GameObject dataController;
    [SerializeField]
    private GameObject uiController;

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


        //  Set to windowed mode at 1280x720 resolution by default
        //Screen.fullScreen = false;
        Screen.SetResolution(1280, 720, false);

        ProgramState.CurrentState = ProgramState.states.Setup;

        //  Perform setup
        EarlySetup();
    }

    private void EarlySetup()
    {
        dataController.SetActive(true);
        uiController.SetActive(true);

        DataController.Instance.Setup();

        //  Everything is now loaded so we can set the program to our default startup tab (drops tab)
        //  And call the bossdropdownvaluechanged event to load the rest of our data into UI
        //UIController.Instance.OnToolbarDropButtonClicked();
        //EventManager.Instance.BossDropdownValueChanged();
    }

    public void LateSetup()
    {
        UIController.Instance.OnToolbarDropButtonClicked();
        EventManager.Instance.BossDropdownValueChanged();
    }

    public void Update()
    {
        //Debug.Log("Program state: " + ProgramState.CurrentState);
        //Debug.Log("Popup state: " + PopupState.currentState);
        //Debug.Log("Drop log: " + DataController.Instance.CurrentDropTabLog);
        //Debug.Log("Log log(???): " + DataController.Instance.CurrentLogTabLog);
        //Debug.Log("Current boss: " + DataController.Instance.CurrentBoss);

        if(Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("saved");
            DataController.Instance.SaveBossLogData();
        }
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
