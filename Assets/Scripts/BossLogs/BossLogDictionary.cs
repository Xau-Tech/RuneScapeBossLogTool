using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// Dictionary for all user-logged data regarding boss kills, loot, time, rares, etc
/// </summary>
[System.Serializable]
public class BossLogDictionary
{
    //  Properties & fields
    public bool hasUnsavedData { get; set; }

    private Dictionary<short, BossLogList> data { get; set; }
    public string _filePath = "";

    //  Constructor
    public BossLogDictionary()
    {
        data = new Dictionary<short, BossLogList>();
    }

    //  Methods

    public void DetermineFilePath(string rsVersion)
    {
        if (rsVersion.ToLower().CompareTo("rs3") == 0)
        {
            if (Application.isEditor)
                _filePath = Application.persistentDataPath + "/testRS3Data.dat";
            else
                _filePath = Application.persistentDataPath + "/RS3Data.dat";
        }
        else
        {
            if (Application.isEditor)
                _filePath = Application.persistentDataPath + "/testOSRSData.dat";
            else
                _filePath = Application.persistentDataPath + "/OSRSData.dat";
        }
    }

    public void Add(short bossId, BossLogList logList)
    {
        data.Add(bossId, logList);
    }

    public void AddEmptyLog(short bossId, string logName)
    {
        BossLogList bossLogList;

        if((bossLogList = GetBossLogList(bossId)).Exists(logName))
        {
            Debug.Log($"Trying to add a log name that already exists for this boss!\n{bossId} - {logName}");
            return;
        }

        bossLogList.Add(new BossLog(bossId, logName));
        hasUnsavedData = true;
    }

    //  Update specified log with passed information
    public void AddToLog(short bossId, string logName, BossLog newData, ItemSlotList itemSlotList)
    {
        BossLog bossLog = GetBossLogList(bossId).Find(logName);

        Debug.Log($"Before add: {bossLog.ToString()}");
        Debug.Log($"Adding: {newData.ToString()}");
        bossLog += newData;
        Debug.Log($"After add: {bossLog.ToString()}");
        AddToRareItemList(bossLog, itemSlotList);
        hasUnsavedData = true;
    }

    private void AddToRareItemList(BossLog bossLog, ItemSlotList itemSlotList)
    {
        bossLog.AddToRareItemList(itemSlotList);
    }

    public void RemoveLog(short bossId, string logName)
    {
        GetBossLogList(bossId).Remove(logName);
        hasUnsavedData = true;
    }

    public void RenameLog(short bossId, string oldLogName, string newLogName)
    {
        GetBossLogList(bossId).Find(oldLogName).logName = newLogName;
        hasUnsavedData = true;
    }

    public BossLogList GetBossLogList(short bossId)
    {
        data.TryGetValue(bossId, out BossLogList bll);
        return bll;
    }

    public List<string> GetSortedLogList(short bossId)
    {
        BossLogList bll = GetBossLogList(bossId);
        List<string> nameList = new List<string>();

        //  Return placeholder text if there are no logs for the searched key
        if(bll.Count == 0)
        {
            nameList.Add("-Add New Log-");
            return nameList;
        }
        else
        {
            foreach(BossLog log in bll)
            {
                nameList.Add(log.logName);
            }

            nameList.Sort();
            return nameList;
        }
    }

    public bool ContainsLog(short bossId, string logName)
    {
        return GetBossLogList(bossId).Exists(logName);
    }

    public bool ContainsKey(short bossId)
    {
        return data.ContainsKey(bossId);
    }

    private int FindIndex(short bossId, string logName)
    {
        return GetBossLogList(bossId).FindIndex(logName);
    }

    public BossLog GetBossLog(short bossId, string logName)
    {
        if (!ContainsLog(bossId, logName))
        {
            Debug.Log("Returning empty log");
            return new BossLog();
        }
        else
        {
            BossLogList bll = GetBossLogList(bossId);
            return bll.Find(logName);
        }
    }

    //  Clear the passed setup from any logs that had been using it
    public void ClearDeletedSetup(string setupName)
    {
        foreach(var kvp in data)
        {
            foreach(BossLog log in kvp.Value)
            {
                if (string.IsNullOrEmpty(log.setupName))
                    continue;

                if (log.setupName.ToLower().CompareTo(setupName.ToLower()) == 0)
                    log.setupName = "";
            }
        }
    }

    public void UpdateRenamedSetup(string oldSetupName, string newSetupName)
    {
        foreach(var kvp in data)
        {
            foreach(BossLog log in kvp.Value)
            {
                if (string.IsNullOrEmpty(log.setupName))
                    continue;

                if (log.setupName.ToLower().CompareTo(oldSetupName.ToLower()) == 0)
                    log.setupName = newSetupName;
            }
        }
    }

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();

        using (FileStream file = File.Create(_filePath))
        {
            bf.Serialize(file, data);
        }

        EventManager.Instance.LogsSaved();
        PrintAllTotals();
        hasUnsavedData = false;
    }

    public async Task<string> Load(List<short> bossIdList)
    {
        if (File.Exists(_filePath))
        {
            using (FileStream file = File.Open(_filePath, FileMode.Open))
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
                    catch(Exception e)
                    {
                        Debug.LogException(e);
                    }
                }
            }

            Debug.Log("BossLogs loaded from file");
        }
        //  File doesn't exist
        else
        {
            using (FileStream file = File.Create(_filePath)) { }
            Debug.Log("BossLogs file created");
        }

        //  Make sure dictionary has kvps for each of the bosses
        CheckAndAddNewBosses(bossIdList);
        Save();
        return "BossLogDictionary load done";
    }

    private void CheckAndAddNewBosses(List<short> bossIdList)
    {
        for(int i = 0; i < bossIdList.Count; ++i)
        {
            //  Check if the boss hasn't been added to the dictionary
            if (!ContainsKey(bossIdList[i]))
            {
                //  Add the name and a new list to the dictionary
                Debug.Log(ApplicationController.Instance.BossInfo.GetName(bossIdList[i]) + " added");
                Add(bossIdList[i], new BossLogList(bossIdList[i]));
            }
        }
    }

    public void PrintAllTotals()
    {
        if(data.Count == 0)
        {
            Debug.Log("Dictionary is empty");
        }
        else
        {
            string message = "BossLogsDictionary:";
            foreach(BossLogList bll in data.Values)
            {
                LogDataStruct data = bll.CalculateTotals();
                message += $"\nBossLog [ Kills: {data.Kills}, Time: {data.Time}, Value: {data.Loot}, Boss: {ApplicationController.Instance.BossInfo.GetName(bll.bossID)} ]";

                foreach(BossLog bl in bll)
                    message += $"\nLog: {bl.logName}, Setup: {bl.setupName}";
            }

            Debug.Log(message);
        }
    }
}
