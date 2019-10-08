using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillcountIncrement : MonoBehaviour
{
    private int m_Killcount;

    private void Awake()
    {
        m_Killcount = 0;
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
        UIController.uicontroller.m_KillcountText.text = "Killcount: " + m_Killcount;
    }
}
