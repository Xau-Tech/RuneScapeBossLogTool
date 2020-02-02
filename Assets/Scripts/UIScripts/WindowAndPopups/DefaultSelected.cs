using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DefaultSelected : MonoBehaviour
{
    [SerializeField] private Selectable m_Selectable;

    private void OnEnable()
    {
        if (m_Selectable)
            m_Selectable.Select();
    }
}
