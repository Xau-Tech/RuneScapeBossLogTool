using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewLogEnter : MonoBehaviour
{
    private CloseWindow m_CloseWindowScript;
    [SerializeField] private InputField m_LogNameInputField;

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
        string input = m_LogNameInputField.text;

        //  User has not entered a value
        if(input == "")
        {
            EventManager.Instance.InputWarningOpen("Please enter a value!");
            return;
        }

        //  Check if this log name already exists for this boss
        List<string> names = DataController.Instance.BossLogsDictionary.GetBossLogNamesList(DataController.Instance.CurrentBoss);
        foreach(string name in names)
        {
            if(name.ToLower().CompareTo(input.ToLower()) == 0)
            {
                EventManager.Instance.InputWarningOpen("This log name already exists!");
                return;
            }
        }

        // Update log data and repopulate log list
        BossLog newLog = new BossLog(input, DataController.Instance.CurrentBoss);

        DataController.Instance.BossLogsDictionary.GetBossLogList(DataController.Instance.CurrentBoss).Add(newLog);

        //  Close this window and clear the text for next time
        m_CloseWindowScript.Close();

        //  Repopulate the log dropdown
        EventManager.Instance.LogAdded();
    }
}
