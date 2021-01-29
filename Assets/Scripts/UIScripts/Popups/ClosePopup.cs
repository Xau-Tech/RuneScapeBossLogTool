using UnityEngine;
using UnityEngine.UI;

//  Generic script to close any window considered a popup
public class ClosePopup : MonoBehaviour
{
    [SerializeField] private GameObject thisWindow;
    private Button thisButton;

    private void Awake()
    {
        thisButton = GetComponent<Button>();

        if (!thisButton)
            throw new System.Exception($"ClosePopup.cs is not attached to a button gameobject!");
        else
            thisButton.onClick.AddListener(Close);
    }

    private void Close()
    {
        PopupState.CurrentState = PopupState.states.None;
        thisWindow.SetActive(false);
    }
}
