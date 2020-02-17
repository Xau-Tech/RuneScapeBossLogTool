using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResetItemInputField : MonoBehaviour
{
    private InputField m_InputField;

    private void Awake()
    {
        m_InputField = GetComponent<InputField>();
    }

    private void OnEnable()
    {
        EventManager.Instance.onBossDropdownValueChanged += OnReset;
        EventManager.Instance.onItemDropdownValueChanged += OnReset;
        EventManager.Instance.onUIReset += OnReset;
    }

    private void OnDisable()
    {
        EventManager.Instance.onBossDropdownValueChanged -= OnReset;
        EventManager.Instance.onItemDropdownValueChanged -= OnReset;
        EventManager.Instance.onUIReset -= OnReset;
    }

    private void OnReset()
    {
        m_InputField.text = "";
    }
}
