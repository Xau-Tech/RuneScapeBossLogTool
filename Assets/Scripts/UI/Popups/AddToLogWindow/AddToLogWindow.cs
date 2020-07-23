using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//  Window used to add data to an existing BossLog
public class AddToLogWindow : MonoBehaviour
{
    public AddToLogWindow()
    {
        Instance = this;
    }

    public static AddToLogWindow Instance;

    [SerializeField] private GameObject thisWindow;
    [SerializeField] private InputField killsInputField;
    [SerializeField] private InputField timeInputField;
    [SerializeField] private InputField lootInputField;
    [SerializeField] private Dropdown timeDropdown;
    private ProgramState.states previousState;

    public void OpenWindow(in ProgramState.states currentState)
    {
        previousState = currentState;

        WindowState.currentState = WindowState.states.AddToLog;
        thisWindow.SetActive(true);

        FillValues();
        killsInputField.Select();
        StartCoroutine(InputFieldCaretToEnd());
    }

    private IEnumerator InputFieldCaretToEnd()
    {
        yield return new WaitForSeconds(float.MinValue);
        killsInputField.MoveTextEnd(false);
    }

    //  Fills UI values according to respective data
    private void FillValues()
    {
        killsInputField.text = Killcount.killcount.ToString();
        lootInputField.text = DataController.Instance.dropList.TotalValue().ToString();

        //  Timer is not 0 - fill from timer and set dropdown to seconds
        if(Timer.time.TotalSeconds != 0)
        {
            timeInputField.text = Mathf.FloorToInt((float)Timer.time.TotalSeconds).ToString();
            timeDropdown.value = 0;
        }
        //  Timer is 0 - Set to 0 and set dropdown to minutes
        else
        {
            timeInputField.text = "0";
            timeDropdown.value = 1;
        }
    }

    public void CloseWindow()
    {
        thisWindow.SetActive(false);
        ProgramState.CurrentState = previousState;
    }
}
