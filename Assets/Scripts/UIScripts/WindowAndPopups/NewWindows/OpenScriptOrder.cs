using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenScriptOrder : MonoBehaviour
{
    private DefaultSelected m_DefaultSelectedScript;
    private ResetUI m_ResetUIScript;
    private AutoFillLogData m_AutoFillScript;

    private void Awake()
    {
        m_DefaultSelectedScript = GetComponent<DefaultSelected>();
        m_ResetUIScript = GetComponent<ResetUI>();
        m_AutoFillScript = GetComponent<AutoFillLogData>();
    }

    private void OnEnable()
    {
        if (m_ResetUIScript)
            m_ResetUIScript.enabled = true;
        if (m_AutoFillScript)
            m_AutoFillScript.enabled = true;
        if (m_DefaultSelectedScript)
            m_DefaultSelectedScript.enabled = true;
    }

    private void OnDisable()
    {
        if (m_ResetUIScript)
            m_ResetUIScript.enabled = false;
        if (m_AutoFillScript)
            m_AutoFillScript.enabled = false;
        if (m_DefaultSelectedScript)
            m_DefaultSelectedScript.enabled = false;
    }
}
