using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Serialization;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Concurrent;

public class testscript : MonoBehaviour
{
    [SerializeField] private Dropdown bossDropdown;
    [SerializeField] private Drop logDropdown;
    [SerializeField] private Button button;

    private void Awake()
    {
        
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