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
            UIController.uicontroller.GetCurrentBoss());
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
        SingleBossLogData newLog = new SingleBossLogData(input, UIController.uicontroller.GetCurrentBoss());
        DataController.dataController.BossLogsDictionaryClass.GetBossLogList
            (UIController.uicontroller.GetCurrentBoss()).Add(newLog);

        //  Repopulate the log dropdown
        UIController.uicontroller.PopulateLogDropdown();

        //  Close this window and clear the text for next time
        UIController.uicontroller.m_ClickBlocker.SetActive(false);
        this.gameObject.transform.parent.gameObject.SetActive(false);
        this.gameObject.transform.parent.GetComponentInChildren<InputField>().text = "";
    }
}
