using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OKButton : MonoBehaviour
{
    private Button m_ThisButton;

    private void Awake()
    {
        m_ThisButton = GetComponent<Button>();
        m_ThisButton.onClick.AddListener(CloseProgram);
    }

    private void CloseProgram() 
    {
        ProgramControl.Instance.CloseProgram();
    }
}
