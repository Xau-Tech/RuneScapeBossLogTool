using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Serialization;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using System.Diagnostics;
using UnityEngine.Networking;
using System.Threading.Tasks;
using System;

public class testscript : MonoBehaviour
{
    public SetupItemDB itemDB;

    private void Awake()
    {
        foreach(SetupItem item in itemDB.items)
        {
            if(item is Food)
            {
                ((Food)item).Eat();
            }
        }

        string hiscoreBaseURL = "https://secure.runescape.com/m=hiscore/index_lite.ws?player=";
        string rsn = "Dieyou2000";
        string hiscoreCheck = hiscoreBaseURL + rsn;

        StartCoroutine(FindPlayer(new WWW(hiscoreCheck)));
    }

    private IEnumerator FindPlayer(WWW www)
    {
        yield return www;

        UnityEngine.Debug.Log($"{www.text}");
    }

    private void Update()
    {
        
    }

    private TimeSpan Test(int times, Action action)
    {
        TimeSpan totalTime = new TimeSpan();
        Stopwatch sw = new Stopwatch();

        for(int i = 0; i < times; ++i)
        {
            sw = Stopwatch.StartNew();
            action();
            sw.Stop();
            UnityEngine.Debug.Log($"Run {i}: {sw.Elapsed}");
            totalTime += sw.Elapsed;
        }

        return totalTime;
    }

    /*private void BinarySave()
    {
        BinaryFormatter bf = new BinaryFormatter();

        using (FileStream file = File.Create(Application.persistentDataPath + "/binarySave.dat"))
        {
            bf.Serialize(file, dictionary.data);
        }

        dictionary.PrintAllTotals();
    }

    private void BinaryLoad()
    {
        BinaryFormatter bf = new BinaryFormatter();

        using (FileStream file = File.Open(Application.persistentDataPath + "/binarySave.dat", FileMode.Open))
        {
            dictionary.data = (Dictionary<string, BossLogList>)bf.Deserialize(file);
        }

        dictionary.PrintAllTotals();
    }

    private void NewtonSave()
    {
        using (FileStream file = File.Create(Application.persistentDataPath + "/newtonSave.dat"))
            using(StreamWriter sw = new StreamWriter(file))
        {
            sw.Write(JsonConvert.SerializeObject(dictionary.data));
        }
    }

    private void NewtonLoad()
    {
        //dictionary.data = JsonConvert.DeserializeObject<Dictionary<string, BossLogList>>(File.ReadAllText(Application.persistentDataPath + "/newtonSave.dat"));

        using (FileStream file = File.Open(Application.persistentDataPath + "/newtonSave.dat", FileMode.Open))
        using (StreamReader sr = new StreamReader(file))
        {
            dictionary.data = JsonConvert.DeserializeObject<Dictionary<string, BossLogList>>(sr.ReadToEnd());
        }

        dictionary.PrintAllTotals();
    }

    private void FillDictionary()
    {
        for (int i = 0; i < bosses.Length; ++i)
        {
            dictionary.Add(in bosses[i], new BossLogList(bosses[i]));

            for (int j = 0; j < logNames.Length; ++j)
            {
                dictionary.AddLog(in bosses[i], in logNames[j]);

                for (int k = 0; k < 10; ++k)
                {
                    dictionary.AddToLog(in bosses[i], in logNames[j],
                        new BossLog(bosses[i], logNames[j],
                        (uint)UnityEngine.Random.Range(0, 15), (ulong)UnityEngine.Random.Range(0, 500000), (uint)UnityEngine.Random.Range(300, 4000)),
                        new DropList());
                }
            }
        }
    }*/
}