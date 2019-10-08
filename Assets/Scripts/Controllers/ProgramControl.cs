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
    public string CurrentBoss { get { return m_CurrentBoss; } }


    private string m_CurrentBoss;


    private void Awake()
    {
        if(controller == null)
        {
            DontDestroyOnLoad(gameObject);
            controller = this;
        }
        else if(controller != this)
        {
            Destroy(gameObject);
        }

        //  Set to windowed mode at 1280x720 resolution by default
        Screen.fullScreen = false;
        Screen.SetResolution(1280, 720, false);

        //  Set drop panel to default active on load
        UIController.uicontroller.OnToolbarDropButtonClicked();

        //  Wait for data to load
        StartCoroutine(CheckIfDataLoaded());

        //  Perform setup
        DataController.dataController.LoadBossInfo();
        Setup();
        DataController.dataController.LoadBossLogData();
    }



    private void OnEnable()
    {
        EventManager.OnBossListDropdownChanged += OnBossListValueChanged;
    }


    private void OnDisable()
    {
        EventManager.OnBossListDropdownChanged -= OnBossListValueChanged;
    }


    //  Update current boss when the selected boss is changed in the Drops tab
    private void OnBossListValueChanged()
    {
        m_CurrentBoss = UIController.uicontroller.GetCurrentBoss();
    }


    //  Coroutine to check and wait for data to load in the DataController
    private IEnumerator CheckIfDataLoaded()
    {
        while(!DataController.dataController.HasFinishedLoading)
        {
            yield return null;
        }
    }


    private void Setup()
    {
        UIController.uicontroller.Setup();


        //  Set BossDropdown to first value so value doesn't have to be changed twice 
        //  to properly load items for first option
        DataController.dataController.OnBossListValueChanged();
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
}

//  Program states for each tab
public class ProgramState
{
    public enum states { Drops, Logs, Setup };
    public static states CurrentState;
}
