using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Script for the window used for any popup that requires a user input into a field
/// </summary>
public class InputPopup : AbstractPopup
{
    //  Properties & fields

    public override Enums.PopupStates AssociatedPopupState { get { return _popupState; } }
    public readonly static byte ADDLOG = 0;
    public readonly static byte RENAMELOG = 1;
    public readonly static byte ADDSETUP = 2;

    private Enums.PopupStates _popupState;
    [SerializeField] private Text _titleText;
    [SerializeField] private InputField _inputField;
    [SerializeField] private Button _enterButton;
    [SerializeField] private Button _cancelButton;
    private Action _enterButtonAction;
    private bool _validInput = false;
    private string _userChoice = "";

    //  Monobehaviors

    private void Awake()
    {
        _enterButton.onClick.AddListener(EnterButton_OnClick);
        _cancelButton.onClick.AddListener(ClosePopup);
        _inputField.GetComponent<IFOnEndEnter>().EndEditAction = EnterButton_OnClick;
    }

    protected override void OnEnable()
    {
        _inputField.text = "";
        _validInput = false;
        _userChoice = "";
        base.OnEnable();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
    }

    //  Methods

    /// <summary>
    /// Open the input popup with the specificed option
    /// </summary>
    /// <param name="option">The InputPopup class contains public static fields to use as parameters for the option</param>
    /// <returns>The value entered by the user </returns>
    public async Task<string> OpenPopup(byte option)
    {
        //  Match the option to a state for this input popup
        switch (option)
        {
            case 0:
                _popupState = Enums.PopupStates.AddLog;
                break;
            case 1:
                _popupState = Enums.PopupStates.RenameLog;
                break;
            case 2:
                _popupState = Enums.PopupStates.AddSetup;
                break;
            default:
                throw new System.Exception("An improper value has been passed to this function");
        }

        //  Try to open
        bool success = base.OpenPopup();

        //  Popup successfully opened
        if (success)
        {
            SetupPopup();
            await Task.Run(() => ValidUserInput());
            base.ClosePopup();
            Debug.Log($"User choice was - {_userChoice}");
            return _userChoice;
        }
        else
        {
            return "";
        }
    }

    protected override void ClosePopup()
    {
        _userChoice = "";
        _validInput = true;
    }

    private bool ValidUserInput()
    {
        while (!_validInput)
        {
            Thread.Sleep(100);
        }

        return true;
    }

    //  Setup the UI & functionality depending on how this input popup was opened
    private void SetupPopup()
    {
        //  UI changes
        switch (_popupState)
        {
            case Enums.PopupStates.AddLog:
                AddLogSetup();
                break;
            case Enums.PopupStates.RenameLog:
                RenameLogSetup();
                break;
            case Enums.PopupStates.AddSetup:
                AddSetupSetup();
                break;
            default:
                throw new System.Exception("An improper value has been passed to this function");
        }

        _inputField.Select();
    }

    private void AddLogSetup()
    {
        //  Set UI
        _titleText.text = "Add New Log";

        //  Set enter button action
        _enterButtonAction = () =>
        {
            //  Check for valid input
            string newLogName = _inputField.text;

            if (string.IsNullOrEmpty(newLogName))
            {
                PopupManager.Instance.ShowNotification("You must enter some name for your new boss log!");
                return;
            }

            //  Capitalize first letter - lowercase the rest
            if (newLogName.Length == 1)
                newLogName = newLogName.ToUpper();
            else
                newLogName = char.ToUpper(newLogName[0]) + newLogName.Substring(1);

            //  Check for a matching log
            if (ApplicationController.Instance.BossLogs.ContainsLog(ApplicationController.Instance.CurrentBoss.BossId, newLogName))
            {
                PopupManager.Instance.ShowNotification($"A log with this name already exists for this boss!");
                return;
            }

            //  Add a new log with this name
            ApplicationController.Instance.BossLogs.AddEmptyLog(ApplicationController.Instance.CurrentBoss.BossId, newLogName);

            //  Mark what the user entered and that a valid value has been entered so this popup returns
            _userChoice = newLogName;
            _validInput = true;
        };
    }

    private void RenameLogSetup()
    {
        //  Set UI
        _titleText.text = "Rename Log";

        //  Set enter button action
        _enterButtonAction = () =>
        {
            //  Check for valid input
            string currentLogName = ApplicationController.Instance.CurrentLog.logName;
            string newLogName = _inputField.text;

            if (string.IsNullOrEmpty(newLogName))
            {
                PopupManager.Instance.ShowNotification("You must enter some name to rename this log to!");
                return;
            }

            //  Capitalize first letter - lowercase the rest
            if (newLogName.Length == 1)
                newLogName = newLogName.ToUpper();
            else
                newLogName = char.ToUpper(newLogName[0]) + newLogName.Substring(1);

            //  Check for a matching log
            if (ApplicationController.Instance.BossLogs.ContainsLog(ApplicationController.Instance.CurrentBoss.BossId, newLogName))
            {
                PopupManager.Instance.ShowNotification($"A log with this name already exists for this boss!");
                return;
            }

            //  Rename the log
            ApplicationController.Instance.BossLogs.RenameLog(ApplicationController.Instance.CurrentBoss.BossId, currentLogName, newLogName);

            //  Mark what the user entered and that a valid value has been entered so this popup returns
            _userChoice = newLogName;
            _validInput = true;
        };
    }

    private void AddSetupSetup()
    {
        //  Set UI
        _titleText.text = "Add New Setup";

        //  Set enter button action
        _enterButtonAction = () =>
        {
            //  Check for valid input
            string currentSetupName = ApplicationController.Instance.CurrentSetup.SetupName;
            string newSetupName = _inputField.text;

            if (string.IsNullOrEmpty(newSetupName))
            {
                PopupManager.Instance.ShowNotification("You must enter some name for your new setup!");
                return;
            }

            //  Capitalize the first letter - lowercase the rest
            if (newSetupName.Length == 1)
                newSetupName = newSetupName.ToUpper();
            else
                newSetupName = char.ToUpper(newSetupName[0]) + newSetupName.Substring(1);

            //  Check for a matching setup name
            if (ApplicationController.Instance.Setups.ContainsKey(newSetupName))
            {
                PopupManager.Instance.ShowNotification($"A setup with this name already exists!");
                return;
            }

            //  Add the new setup
            Player player = ApplicationController.Instance.CurrentSetup.Player;
            ApplicationController.Instance.Setups.Add(newSetupName, new Setup(newSetupName, player));

            //  Mark what the user entered and that a valid value has been entered so this popup returns
            _userChoice = newSetupName;
            _validInput = true;
        };
    }

    private void EnterButton_OnClick()
    {
        _enterButtonAction();
    }

    private void InputField_OnEndEdit(string text)
    {
        EnterButton_OnClick();
    }
}
