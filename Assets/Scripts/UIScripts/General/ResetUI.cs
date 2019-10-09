using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResetUI : MonoBehaviour
{
    [SerializeField]
    private GameObject[] m_ObjectsToDisable;
    [SerializeField]
    private InputField[] m_InputFieldsToReset;

    private void OnEnable()
    {
        foreach(GameObject obj in m_ObjectsToDisable)
        {
            obj.gameObject.SetActive(false);
        }

        foreach(InputField input in m_InputFieldsToReset)
        {
            input.text = "";
        }
    }
}
