using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewLogEnter : MonoBehaviour
{
    [SerializeField]
    private Text m_InputProblemText;
    [SerializeField]
    private GameObject m_InputProblemHighlight;
    private CloseWindow m_CloseWindowScript;

    private void Awake()
    {
        m_CloseWindowScript = GetComponentInParent<CloseWindow>();
    }

    public void EnterPressed()
    {
        if (Input.GetKeyDown(KeyCode.Return))
            OnClick();
    }


    public void OnClick()
    {
        string input = UIController.uicontroller.m_NewLogWindow.GetComponentInChildren<InputField>().text;

        //  User has not entered a value
        if(input == "")
        {
            m_InputProblemText.text = "Please enter a value!";
            m_InputProblemText.gameObject.SetActive(true);
            m_InputProblemHighlight.gameObject.SetActive(true);
            return;
        }

        //  Check if this log name already exists for this boss
        List<string> names = DataController.dataController.BossLogsDictionaryClass.GetBossLogNamesList(
            DataController.dataController.CurrentBoss);
        foreach(string name in names)
        {
            if(name.ToLower().CompareTo(input.ToLower()) == 0)
            {
                m_InputProblemText.text = "This log name already exists!";
                m_InputProblemHighlight.gameObject.SetActive(true);
                m_InputProblemText.gameObject.SetActive(true);
                return;
            }
        }

        // Update log data and repopulate log list
        SingleBossLogData newLog = new SingleBossLogData(input, DataController.dataController.CurrentBoss);

        DataController.dataController.BossLogsDictionaryClass.GetBossLogList
            (DataController.dataController.CurrentBoss).Add(newLog);

        //  Close this window and clear the text for next time
        m_CloseWindowScript.Close();

        //  Repopulate the log dropdown
        EventManager.manager.LogAdded();
    }
}
