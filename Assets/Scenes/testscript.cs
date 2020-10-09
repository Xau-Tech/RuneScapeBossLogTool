using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class testscript : MonoBehaviour
{
    private void Awake()
    {
        List<ItemSlot> slots = new List<ItemSlot>(28);
        Debug.Log(slots.Count);
        for(int i = 0; i < 28; ++i)
        {
            slots.Add(new ItemSlot());
        }
        Debug.Log(slots.Count);

        slots[4].item = new Item(4, "rawr", 69);
        Debug.Log($"{slots[4].item.price}");
    }

    /*private TimeSpan Test(int times, Action action)
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

    private void BinarySave()
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