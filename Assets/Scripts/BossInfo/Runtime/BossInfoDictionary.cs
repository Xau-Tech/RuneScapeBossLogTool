using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Serialization;
using UnityEngine;

/// <summary>
/// Dictionary of all the loaded bossinfo objects
/// </summary>
public class BossInfoDictionary
{
    //  Properties & fields
    private Dictionary<short, BossInfo> _bossDictionary;
    private Dictionary<string, short> _nameToIdDictionary;
    private BossListSO _bossListSo;

    private static readonly string BASEPATH = Application.streamingAssetsPath + "/Data/BossInfo/";
    private static readonly string FILENAME = "BossData.xml";

    //  Constructor
    public BossInfoDictionary()
    {
        _bossDictionary = new Dictionary<short, BossInfo>();
        _nameToIdDictionary = new Dictionary<string, short>();
    }

    public void Setup()
    {
        _bossListSo = Resources.Load<BossListSO>("BossData/BossInfoLists");
    }

    public async Task<string> Load(string rsVersion)
    {
        /*string filePath = BASEPATH + rsVersion + FILENAME;
        XmlSerializer serializer = new XmlSerializer(typeof(List<BossDataGlob>));
        BossDataListGlob bossGlobList = new BossDataListGlob();

        //  Deserialize file to list of globs
        using (FileStream fs = new FileStream(filePath, FileMode.Open))
        {
            try
            {
                bossGlobList.BossData = serializer.Deserialize(fs) as List<BossDataGlob>;
            }
            catch (Exception e)
            {

            }
        }

        //  Create run time instances from globs and add to dictionary
        foreach(BossDataGlob bossGlob in bossGlobList.BossData)
        {
            //  Check if matching id already exists
            if (_bossDictionary.ContainsKey(bossGlob.BossId))
            {
                throw new System.Exception($"ERROR: Duplicate Id found for value {bossGlob.BossId}!");
            }
            else
            {
                BossInfo bossInfo = new BossInfo(bossGlob);
                _bossDictionary.Add(bossInfo.BossId, bossInfo);
                _nameToIdDictionary.Add(bossInfo.BossName, bossInfo.BossId);
            }
        }

        Print();
        return "BossInfo load done";*/

        List<BossInfoSO> bossData = _bossListSo.GetBosses(rsVersion);

        for(int i = 0; i < bossData.Count; ++i)
        {
            if (_bossDictionary.ContainsKey(bossData[i].BossId))
                throw new System.Exception($"Duplicate BossID found on ID {bossData[i].BossId}");

            _bossDictionary.Add(bossData[i].BossId, new BossInfo(bossData[i]));
            _nameToIdDictionary.Add(bossData[i].BossName, bossData[i].BossId);
        }

        Resources.UnloadAsset(_bossListSo);
        Print();
        return "BossInfo load done";
    }

    /// <summary>
    /// Returns a sorted list of boss names
    /// </summary>
    public List<string> GetOrderedBossNames()
    {
        List<string> temp = new List<string>();

        foreach(var boss in _bossDictionary.Values)
        {
            temp.Add(boss.BossName);
        }

        temp.Sort();
        return temp;
    }

    /// <summary>
    /// Return boss name from passed id value
    /// </summary>
    public string GetName(short bossId)
    {
        _bossDictionary.TryGetValue(bossId, out BossInfo info);

        if (info != null)
            return info.BossName;
        else
            return "";
    }

    /// <summary>
    /// Return list of all boss ids
    /// </summary>
    public List<short> GetIds()
    {
        List<short> ids = new List<short>();

        foreach(short id in _nameToIdDictionary.Values)
        {
            ids.Add(id);
        }

        return ids;
    }

    /// <summary>
    /// Return a boss id from passed name value
    /// </summary>
    public short GetId(string bossName)
    {
        if (!_nameToIdDictionary.TryGetValue(bossName, out short bossId))
            return -1;
        else
            return bossId;
    }

    /// <summary>
    /// Return a boss object from passed id
    /// </summary>
    public BossInfo GetBoss(short bossId)
    {
        if(!_bossDictionary.ContainsKey(bossId))
        {
            return null;
        }
        else
        {
            _bossDictionary.TryGetValue(bossId, out BossInfo boss);
            return boss;
        }
    }

    /// <summary>
    /// Return a boss object from passed name
    /// </summary>
    public BossInfo GetBoss(string bossName)
    {
        if (!_nameToIdDictionary.ContainsKey(bossName))
            return null;
        else
            return GetBoss(GetId(bossName));
    }

    private void Print()
    {
        string result = "BossInfoDictionary:";
        foreach(var info in _bossDictionary.Values)
        {
            result += $"\n{info.ToString()}";
        }

        Debug.Log(result);
    }
}
