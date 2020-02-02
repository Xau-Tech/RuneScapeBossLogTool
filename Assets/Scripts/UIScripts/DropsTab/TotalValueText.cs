using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TotalValueText : MonoBehaviour
{
    private Text m_ThisText;

    private void Awake()
    {
        m_ThisText = GetComponent<Text>();
    }

    private void OnEnable()
    {
        EventManager.Instance.onDropListGenerated += UpdateText;
    }

    private void OnDisable()
    {
        EventManager.Instance.onDropListGenerated -= UpdateText;
    }

    private void UpdateText()
    {
        m_ThisText.text = "Total value: " + DataController.Instance.DropList.GetTotalValue().ToString("#,#0")
             + " gp";
    }
}
