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
    public static ProgramControl controller;

    private void Awake()
    {
        if (controller == null)
        {
            DontDestroyOnLoad(gameObject);
            controller = this;
        }
        else if (controller != this)
        {
            Destroy(gameObject);
        }


        //  Set to windowed mode at 1280x720 resolution by default
        Screen.fullScreen = false;
        Screen.SetResolution(1280, 720, false);

        //  Wait for controllers to load
        StartCoroutine(CheckIfControllersLoaded());

        //  Perform setup
        DataController.dataController.Setup();

        Setup();
    }

    //  Coroutine to check and wait for data to load in the DataController
    private IEnumerator CheckIfControllersLoaded()
    {
        while(!DataController.dataController.HasFinishedLoading || !UIController.uicontroller.HasFinishedLoading)
        {
            yield return null;
        }
    }


    private void Setup()
    {
        UIController.uicontroller.Setup();
    }


    /*
     * Save and load will be moved to DataController on completion of boss log data structures
     */
    private void Save()
    {
        //BinaryFormatter bf = new BinaryFormatter();
        //FileStream file = File.Create(Application.persistentDataPath + "/bossInfo.dat");

        //bf.Serialize(file, m_BossData);
        //file.Close();
    }

    public void Update()
    {
        Debug.Log(ProgramState.CurrentState);
        Debug.Log(PopupState.currentState);
        Debug.Log("Drop log: " + DataController.dataController.CurrentDropTabLog);
        Debug.Log("Log log(???): " + DataController.dataController.CurrentLogTabLog);
    }
}

//  Program states for each tab
public class ProgramState
{
    public enum states { Drops, Logs, Setup, AddToLog, AddNewLog, DeleteLog};
    public static states CurrentState;
}
