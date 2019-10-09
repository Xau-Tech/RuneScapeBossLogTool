using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectElement : MonoBehaviour
{
    [SerializeField]
    private GameObject m_FirstSelected;

    private void OnEnable()
    {
        if(m_FirstSelected.GetComponent<InputField>() != null)
        {
            m_FirstSelected.GetComponent<InputField>().Select();
        }
    }
}
