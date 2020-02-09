using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//  Exit application script
public class ExitScript : MonoBehaviour
{
    [SerializeField] private GameObject m_MenuPanel;

    private void Awake()
    {
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(Exit);
    }

    private void Exit()
    {
        m_MenuPanel.SetActive(false);
        ProgramState.CurrentState = ProgramState.states.Exit;
        ProgramControl.Instance.CloseProgram();
    }
}
