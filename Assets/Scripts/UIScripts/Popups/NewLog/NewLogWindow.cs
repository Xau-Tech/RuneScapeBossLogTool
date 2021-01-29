using UnityEngine;
using UnityEngine.UI;

//  Window for user to input a value for a new BossLog
public class NewLogWindow : MonoBehaviour
{
    public NewLogWindow()
    {
        Instance = this;
    }

    public static NewLogWindow Instance;

    [SerializeField] private GameObject thisWindow;
    private InputField newLogField;
    private ProgramState.states previousState;

    private void OnEnable()
    {
        if (!newLogField)
            newLogField = thisWindow.GetComponentInChildren<InputField>();
    }

    //  Opens the window and caches the previous state to return to on close
    public void OpenWindow(in ProgramState.states currentState)
    {
        previousState = currentState;

        WindowState.currentState = WindowState.states.AddLog;
        thisWindow.SetActive(true);

        newLogField.text = "";
        newLogField.Select();
    }

    //  Closes the window and restores the previous state
    public void CloseWindow()
    {
        thisWindow.SetActive(false);
        ProgramState.CurrentState = previousState;
    }
}
