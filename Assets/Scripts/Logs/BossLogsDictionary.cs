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
        data = new Dictionary<short, BossLogList>();
    }

    public bool hasUnsavedData { get; set; }

    private Dictionary<short, BossLogList> data { get; set; }

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
    public void Add(in short bossID, in BossLogList logList)
    {
        data.Add(bossID, logList);
    }

    //  Adds a new empty log to the BossLogList value associated with bossName key
    public void AddLog(in short bossID, in string logName)
    {
        BossLogList bossLogList;

        if ((bossLogList = GetBossLogList(in bossID)).Exists(logName))
        {
            Debug.Log($"Trying to add a log name that already exists for this boss!\n{bossID} - {logName}");
            return;
        }

        bossLogList.Add(new BossLog(bossID, logName));
        hasUnsavedData = true;
    }

    public void RemoveLog(in short bossID, in string logName)
    {
        GetBossLogList(bossID).Remove(logName);
        hasUnsavedData = true;
        EventManager.Instance.LogDeleted();
    }

    //  Rename a log using bossName key, existingLogName to find BossLog in dictionary Values (BossLogList)
    public void RenameLog(in short bossID, in string existingLogName, in string newLogName)
    {
        GetBossLogList(in bossID).Find(existingLogName).logName = newLogName;
        hasUnsavedData = true;
        EventManager.Instance.LogRename(in newLogName);
    }

    //  Returns the list of all logs for a boss by name
    public BossLogList GetBossLogList(in short bossID)
    {
        BossLogList list = new BossLogList();
        data.TryGetValue(bossID, out list);
        return list;
    }

    //  Returns a sorted list of strings of all log names for a boss by name
    public List<string> GetBossLogNamesList(in short bossID)
    {
        BossLogList bossLogList = GetBossLogList(bossID);
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
    public bool ContainsLogName(in short bossID, in string logName)
    {
        return GetBossLogList(in bossID).Exists(logName);
    }

    //  Wrapper for Dictionary.ContainsKey
    public bool ContainsKey(in short bossID)
    {
        return data.ContainsKey(bossID);
    }

    //  Wrapper for List.FindIndex
    private int IndexOfLog(in short bossID, in string logName)
    {
        return GetBossLogList(in bossID).FindIndex(logName);
    }

    //  Return the specified log
    private BossLog GetBossLog(in short bossID, in string logName)
    {
        if (!ContainsLogName(in bossID, in logName))
            throw new System.Exception($"Log cannot be found!");

        return GetBossLogList(in bossID).Find(logName);
    }

    //  Update specified log with passed information
    public void AddToLog(in short bossID, in string logName, in BossLog newData, in ItemSlotList itemSlotList)
    {
        BossLog bossLog = GetBossLogList(in bossID).Find(logName);

        Debug.Log($"Before add: {bossLog.ToString()}");
        Debug.Log($"Adding: {newData.ToString()}");

        bossLog += newData;

        Debug.Log($"After add: {bossLog.ToString()}");

        AddToRareItemList(in bossLog, in itemSlotList);

        hasUnsavedData = true;
    }

    //  Adds to passed list
    private void AddToRareItemList(in BossLog bossLog, in ItemSlotList itemSlotList)
    {
        bossLog.AddToRareItemList(in itemSlotList);
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

    public void Load(in List<short> bossIDList)
    {
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
                        data = (Dictionary<short, BossLogList>)bf.Deserialize(file);
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
        CheckAndAddNewBosses(in bossIDList);

        Save();
    }

    private void CheckAndAddNewBosses(in List<short> bossIDList)
    {
        //  Go through each boss in the info list
        for (int i = 0; i < bossIDList.Count; ++i)
        {
            //  Check if the boss hasn't been added to the dictionary
            if (!ContainsKey(bossIDList[i]))
            {
                //  Add the name and a new list to the dictionary
                Debug.Log(DataController.Instance.bossInfoDictionary.GetBossByID(bossIDList[i]).bossName + " added");
                Add(bossIDList[i], new BossLogList(bossIDList[i]));
            }
        }
    }

    public void PrintAllTotals()
    {
        if (data.Count == 0)
            Debug.Log($"Dictionary is empty");
        else
        {
            string output = "BossLogsDictionary:";
            foreach (BossLogList value in data.Values)
            {
                LogDataStruct data = value.GetBossTotalsData();
                output += $"\nBossLog [ Kills: {data.kills}, Time: {data.time}, Value: {data.loot}, Boss: {DataController.Instance.bossInfoDictionary.GetBossByID(value.bossID).bossName} ]";
            }

            Debug.Log(output);
        }
    }

    public void PrintLogNames()
    {
        List<string> logs = GetBossLogNamesList(CacheManager.currentBoss.bossID);

        for(int i = 0; i < logs.Count; ++i)
        {
            Debug.Log($"Log Name: {logs[i]}");
        }
    }
}
