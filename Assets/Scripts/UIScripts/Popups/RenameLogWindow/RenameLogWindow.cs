using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RenameLogWindow : MonoBehaviour
{
    public static RenameLogWindow Instance { get { return _instance; } }

    private static RenameLogWindow _instance;
    [SerializeField] private GameObject _thisWindow;
    [SerializeField] private Button _renameButton;
    [SerializeField] private Button _closeButton;
    [SerializeField] private InputField _renameInputField;

    private RenameLogWindow()
    {
        _instance = this;
    }

    //  Unity functions

    private void Awake()
    {
        _renameButton.onClick.AddListener(RenameLog);
        _closeButton.onClick.AddListener(CloseButton_OnClick);
    }

    private void OnDestroy()
    {
        _renameButton.onClick.AddListener(RenameLog);
        _closeButton.onClick.RemoveAllListeners();
    }

    public void Update()
    {
        if(EventSystem.current.currentSelectedGameObject == _renameInputField.gameObject)
        {
            if (Input.GetKeyDown(KeyCode.Return))
                RenameLog();
        }
    }

    //  Custom functions

    public void OpenWindow()
    {
        WindowState.currentState = WindowState.states.RenameLog;
        _thisWindow.SetActive(true);
        _renameInputField.Select();
    }

    private void RenameLog()
    {
        short bossId = CacheManager.currentBoss.bossID;
        string oldName = CacheManager.currentLog;
        string newName = _renameInputField.text;

        if (string.IsNullOrEmpty(newName))
        {
            InputWarningWindow.Instance.OpenWindow("Please enter a value!");
            return;
        }
        else
        {
            char[] nameCharArr = newName.ToCharArray();
            nameCharArr[0] = char.ToUpper(nameCharArr[0]);
            newName = new string(nameCharArr);

            if(DataController.Instance.bossLogsDictionary.ContainsLogName(bossId, newName))
            {
                InputWarningWindow.Instance.OpenWindow($"A log called {newName} already exists for {CacheManager.currentBoss.bossName}!  Please enter a different value!");
                _renameInputField.text = "";
                return;
            }
            else
            {
                DataController.Instance.bossLogsDictionary.RenameLog(CacheManager.currentBoss.bossID, in oldName, in newName);
                CloseButton_OnClick();
            }
        }
    }

    //  UI events

    private void CloseButton_OnClick()
    {
        WindowState.currentState = WindowState.states.None;
        _thisWindow.SetActive(false);
    }
}
