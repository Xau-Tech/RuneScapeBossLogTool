using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using System;
using Newtonsoft.Json;

[System.Serializable]
public class BossLogsDictionary
{
    public BossLogsDictionary()
    {
        data = new Dictionary<string, BossLogList>();
    }

    public bool hasUnsavedData { get; set; }

    public Dictionary<string, BossLogList> data { get; set; }

    //  Get the proper file to load/save log data
    public static string BossLogsFile(in string rsVersion)
    {
        //  RSVersion is set to RS3
        if (rsVersion.ToLower().CompareTo("rs3") == 0)
        {
            //  editor only testing file
            if (Application.isEditor)
                return Application.persistentDataPath + "/testRS3Data.dat";
            //  real file
            else
                return Application.persistentDataPath + "/RS3Data.dat";
        }
        //  RSVersion is set to OSRS
        else
        {
            //  editor only testing file
            if (Application.isEditor)
                return Application.persistentDataPath + "/testOSRSData.dat";
            //  real file
            else
                return Application.persistentDataPath + "/OSRSData.dat";
        }
     }

    //  Wrapper for Dictionary.Add to add a new KVP
    public void Add(in string bossName, in BossLogList logList)
    {
        data.Add(bossName, logList);
    }

    //  Adds a new empty log to the BossLogList value associated with bossName key
    public void AddLog(in string bossName, in string logName)
    {
        BossLogList bossLogList;

        if ((bossLogList = GetBossLogList(in bossName)).Exists(logName))
        {
            Debug.Log($"Trying to add a log name that already exists for this boss!\n{bossName} - {logName}");
            return;
        }

        bossLogList.Add(logName);
        hasUnsavedData = true;
    }

    public void RemoveLog(in string bossName, in string logName)
    {
        GetBossLogList(bossName).RemoveAll(logName);
        hasUnsavedData = true;
        EventManager.Instance.LogDeleted();
    }

    //  Rename a log using bossName key, existingLogName to find BossLog in dictionary Values (BossLogList)
    public void RenameLog(in string bossName, in string existingLogName, in string newLogName)
    {
        GetBossLogList(in bossName).Find(existingLogName).logName = newLogName;
        hasUnsavedData = true;
        EventManager.Instance.LogRename(in newLogName);
    }

    //  Returns the list of all logs for a boss by name
    public BossLogList GetBossLogList(in string bossName)
    {
        BossLogList list = new BossLogList();
        data.TryGetValue(bossName, out list);
        return list;
    }

    //  Returns a sorted list of strings of all log names for a boss by name
    public List<string> GetBossLogNamesList(in string bossName)
    {
        BossLogList bossLogList = GetBossLogList(bossName);
        List<string> returnList = new List<string>();

        //  Return placeholder text if there are no logs for the searched key
        if(bossLogList.Count == 0)
        {
            returnList.Add($"-Add New Log-");
            return returnList;
        }

        foreach (BossLog data in bossLogList)
        {
            returnList.Add(data.logName);
        }

        returnList.Sort();
        return returnList;
    }

    //  Check if a boss has a log with passed value as its name
    public bool ContainsLogName(in string bossName, in string logName)
    {
        return GetBossLogList(in bossName).Exists(logName);
    }

    //  Wrapper for Dictionary.ContainsKey
    public bool ContainsKey(in string bossName)
    {
        return data.ContainsKey(bossName);
    }

    //  Wrapper for List.FindIndex
    private int IndexOfLog(in string bossName, in string logName)
    {
        return GetBossLogList(in bossName).FindIndex(logName);
    }

    //  Return the specified log
    private BossLog GetBossLog(in string bossName, in string logName)
    {
        if (!ContainsLogName(in bossName, in logName))
            throw new System.Exception($"Log cannot be found!");

        return GetBossLogList(in bossName).Find(logName);
    }

    //  Update specified log with passed information
    public void AddToLog(in string bossName, in string logName, in BossLog newData, in DropList dropList)
    {
        BossLog bossLog = GetBossLogList(in bossName).Find(logName);

        Debug.Log($"Before add: {bossLog.ToString()}");
        Debug.Log($"Adding: {newData.ToString()}");

        bossLog += newData;

        Debug.Log($"After add: {bossLog.ToString()}");

        AddToRareItemList(in bossLog, in dropList);

        hasUnsavedData = true;
    }

    //  Adds to passed list
    private void AddToRareItemList(in BossLog bossLog, in DropList dropList)
    {
        bossLog.AddToRareItemList(in dropList);
    }

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();

        using (FileStream file = File.Create(BossLogsFile(ProgramControl.Options.GetOptionValue(RSVersionOption.Name()))))
        {
            bf.Serialize(file, data);
        }

        //TEMPORARY//
        if (!Application.isEditor)
            NewtonSave();

        EventManager.Instance.LogsSaved();
        PrintAllTotals();
        hasUnsavedData = false;
     }

    //  #Temporary#
    private void NewtonSave()
    {
        string filename = Application.persistentDataPath;
        string rsVersion = ProgramControl.Options.GetOptionValue(RSVersionOption.Name());
        filename += "/" + rsVersion + "newtonSave.dat";

        using (FileStream file = File.Create(filename))
        using (StreamWriter sw = new StreamWriter(file))
        {
            sw.Write(JsonConvert.SerializeObject(data, Formatting.Indented));
        }
    }

    public void Load(List<string> bossNames)
    {
        /*bossLogDataPath = BossLogsDictionary.BossLogsFile(ProgramControl.Options.GetOptionValue(RSVersionOption.Name()));

        List<string> bossNames = bossInfoDictionary.GetBossNames();

        //  File already exists
        if (File.Exists(bossLogDataPath))
        {
            //load data
            Debug.Log("file exists");

            FileStream file = File.Open(bossLogDataPath, FileMode.Open);

            //  Try to load our data from the file
            if (file.Length != 0)
            {
                try
                {

                    BinaryFormatter bf = new BinaryFormatter();
                    bossLogsDictionary = (BossLogsDictionary)bf.Deserialize(file);
                }
                catch (Exception e)
                {
                    Debug.Log(e.StackTrace);
                }
            }

            CheckAndAddNewBosses(in bossNames);
            file.Close();

            Debug.Log("file loaded");
        }
        //  File doesn't exist
        else
        {
            Debug.Log("file created");
            FileStream file = File.Create(bossLogDataPath);
            file.Close();

            //  Populate dictionary with boss names
            CheckAndAddNewBosses(in bossNames);
        }

        SaveBossLogData();
        EventManager.Instance.LogsLoaded();
        Debug.Log("loadlog out");*/

        string filePath = BossLogsFile(ProgramControl.Options.GetOptionValue(RSVersionOption.Name()));

        //  File exists
        if(File.Exists(filePath))
        {
            using (FileStream file = File.Open(filePath, FileMode.Open))
            {
                //  Make sure file isn't empty
                if(file.Length != 0)
                {
                    BinaryFormatter bf = new BinaryFormatter();

                    //  Deserialize
                    try
                    {
                        data = (Dictionary<string, BossLogList>)bf.Deserialize(file);
                    }
                    catch (Exception e)
                    {
                        Debug.Log(e.StackTrace);
                    }
                }
            }

            Debug.Log($"BossLogs loaded from file");
        }
        //  File doesn't exist
        else
        {
            using (FileStream file = File.Create(filePath)) { }
            Debug.Log("BossLogs file created");
        }

        //  Make sure Dictionary has KVPs for each of the bosses
        CheckAndAddNewBosses(in bossNames);

        Save();
        EventManager.Instance.LogsLoaded();
    }

    private void CheckAndAddNewBosses(in List<string> bossNames)
    {
        //  Go through each boss in the info list
        for (int i = 0; i < bossNames.Count; ++i)
        {
            //  Check if the boss hasn't been added to the dictionary
            if (!ContainsKey(bossNames[i]))
            {
                //  Add the name and a new list to the dictionary
                Debug.Log(bossNames[i] + " added");
                Add(bossNames[i], new BossLogList(bossNames[i]));
            }
        }
    }

    public void PrintAllTotals()
    {
        if (data.Count == 0)
            Debug.Log($"Dictionary is empty");

        foreach(BossLogList value in data.Values)
        {
            LogDataStruct data = value.GetBossTotalsData();
            Debug.Log($"{value.bossName}\nTotals: [ Kills: {data.kills}, Time: {data.time}, Value: {data.loot} ]");
        }
    }

    public void PrintLogNames()
    {
        List<string> logs = GetBossLogNamesList(CacheManager.currentBoss);

        for(int i = 0; i < logs.Count; ++i)
        {
            Debug.Log($"Log Name: {logs[i]}");
        }
    }
}
