using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KillcountIncrement : MonoBehaviour
{
    private int m_Killcount;

    [SerializeField]
    private Text m_KillcountText;

    public string Killcount { get { return m_Killcount.ToString(); } }

    private void Awake()
    {
        m_Killcount = 0;
    }

    private void OnEnable()
    {
        EventManager.Instance.onBossDropdownValueChanged += ResetKillcount;
        EventManager.Instance.onUIReset += ResetKillcount;
    }

    private void OnDisable()
    {
        EventManager.Instance.onBossDropdownValueChanged -= ResetKillcount;
        EventManager.Instance.onUIReset -= ResetKillcount;
    }

    public void IncrementKillcount()
    {
        m_Killcount++;
        UpdateKillcountText();
    }

    public void ResetKillcount()
    {
        m_Killcount = 0;
        UpdateKillcountText();
    }

    private void UpdateKillcountText()
    {
        m_KillcountText.text = "Killcount: " + m_Killcount;
    }
}
