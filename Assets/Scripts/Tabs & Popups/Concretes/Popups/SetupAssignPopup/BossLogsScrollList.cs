using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

/// <summary>
/// The main controlling class for displaying boss logs for use in assigning setups
/// </summary>
public class BossLogsScrollList : MonoBehaviour
{
    //  Properties & fields

    private List<GameObject> _bossDisplays;
    [SerializeField] private GameObject _bossDisplayTemplate;

    //  Monobehaviors

    private void Awake()
    {
        _bossDisplays = new List<GameObject>();
    }

    public void Setup(string setupName, Action<string, string> logToggleCallback)
    {
        foreach(short bossId in ApplicationController.Instance.BossInfo.GetIds())
        {
            List<LogDisplayInfo> logDisplayInfoList = new List<LogDisplayInfo>();
            string bossName = ApplicationController.Instance.BossInfo.GetName(bossId);

            foreach (BossLog bossLog in ApplicationController.Instance.BossLogs.GetBossLogList(bossId))
            {
                LogDisplayInfo ldi;
                ldi.LogName = bossLog.logName;
                ldi.SetupName = bossLog.setupName;

                if (string.IsNullOrEmpty(bossLog.logName))
                {
                    ldi.IsLinkedToSetup = false;
                    //Debug.Log("null or empty boss log setup");
                }
                else
                { 
                    ldi.IsLinkedToSetup = bossLog.setupName == setupName;
                    //Debug.Log("value in boss log setup");
                }

                logDisplayInfoList.Add(ldi);
            }

            GameObject bossDisplay = Instantiate(_bossDisplayTemplate) as GameObject;
            _bossDisplays.Add(bossDisplay);
            bossDisplay.SetActive(true);
            bossDisplay.GetComponent<BossDisplay>().Setup(bossName, logDisplayInfoList, logToggleCallback);
            bossDisplay.transform.SetParent(_bossDisplayTemplate.transform.parent, false);
        }
    }

    public void Clear()
    {
        GetComponent<ScrollRect>().verticalScrollbar.value = 1.0f;

        foreach(var bd in _bossDisplays)
        {
            Destroy(bd);
        }
    }

    public struct LogDisplayInfo
    {
        public string LogName;
        public string SetupName;
        public bool IsLinkedToSetup;
    }
}
