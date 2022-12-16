using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

/// <summary>
/// Displays the boss name and all log names for use in assigning setups
/// </summary>
public class BossDisplay : MonoBehaviour
{
    //  Properties & fields

    [SerializeField] private Text _bossNameText;
    [SerializeField] private GameObject _logDisplayTemplate;
    [SerializeField] private float _bottomSpacing;
    private List<GameObject> _logDisplayList;
    private float _bossTextHeight;
    private float _thisWidth;
    private float _logDisplayHeight;

    //  Monobehaviors

    private void Awake()
    {
        _logDisplayList = new List<GameObject>();
        RectTransform thisRectTrans = GetComponent<RectTransform>();
        _bossTextHeight = _bossNameText.gameObject.GetComponent<RectTransform>().sizeDelta.y;
        _thisWidth = thisRectTrans.sizeDelta.x;
        _logDisplayHeight = _logDisplayTemplate.GetComponent<RectTransform>().sizeDelta.y;
    }

    //  Methods

    public void Setup(string bossName, List<BossLogsScrollList.LogDisplayInfo> logdisplayInfoList, Action<string, string> logToggleCallback)
    {
        _bossNameText.text = "\t" + bossName + ":";

        float totalHeight = _bossTextHeight + (_logDisplayHeight * logdisplayInfoList.Count) + _bottomSpacing;
        GetComponent<RectTransform>().sizeDelta = new Vector2(_thisWidth, totalHeight);

        foreach (var logDisplayInfo in logdisplayInfoList)
        {
            GameObject logDisplay = Instantiate(_logDisplayTemplate) as GameObject;
            _logDisplayList.Add(logDisplay);
            logDisplay.transform.SetParent(_logDisplayTemplate.transform.parent, false);
            float logDisplayYPos = _bossTextHeight + ((_logDisplayList.Count - 1) * _logDisplayHeight);
            logDisplay.GetComponent<LogDisplay>().Setup(bossName, logDisplayInfo, logDisplayYPos, logToggleCallback);
            logDisplay.SetActive(true);
        }
    }
}
