using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DefaultSelected : MonoBehaviour
{
    [SerializeField]
    private Selectable m_FirstSelected;

    private void OnEnable()
    {
        if(m_FirstSelected != null)
            m_FirstSelected.Select();
    }
}
