using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResetInputFields : MonoBehaviour
{
    [SerializeField] private InputField[] m_InputFields;

    private void OnEnable()
    {
        for(int i = 0; i < m_InputFields.Length; ++i)
        {
            m_InputFields[i].text = "";
        }
    }
}
