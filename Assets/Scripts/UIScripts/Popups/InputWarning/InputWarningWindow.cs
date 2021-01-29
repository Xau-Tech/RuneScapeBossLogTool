using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//  Window to display information about warnings, improper user inputs, other non-error information
public class InputWarningWindow : MonoBehaviour
{
    public InputWarningWindow()
    {
        Instance = this;
    }

    public static InputWarningWindow Instance;

    [SerializeField] private Text infoText;
    [SerializeField] private GameObject thisWindow;

    private void OnEnable()
    {
        if (!infoText)
            infoText = GetComponentInChildren<Text>();
    }

    //  Opens the window, sets state, text, and pulls selection focus off previous gameobject
    public void OpenWindow(in string text)
    {
        PopupState.CurrentState = PopupState.states.Warning;
        thisWindow.SetActive(true);
        infoText.text = text;
        EventSystem.current.SetSelectedGameObject(null);
    }
}
