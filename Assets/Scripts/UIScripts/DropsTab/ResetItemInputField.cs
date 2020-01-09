using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResetItemInputField : MonoBehaviour
{
    [SerializeField]
    private InputField m_InputField;

    private void OnEnable()
    {
        EventManager.manager.onBossDropdownValueChanged += OnReset;
        EventManager.manager.onAddItemButtonClicked += OnReset;
        EventManager.manager.onItemDropdownValueChanged += OnReset;
    }

    private void OnDisable()
    {
        EventManager.manager.onBossDropdownValueChanged -= OnReset;
        EventManager.manager.onAddItemButtonClicked -= OnReset;
        EventManager.manager.onItemDropdownValueChanged -= OnReset;
    }

    private void OnReset()
    {
        m_InputField.text = "";
    }
}
