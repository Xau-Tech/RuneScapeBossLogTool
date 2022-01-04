using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Popup window for anything that requires a user to confirm or deny some action
/// </summary>
public class ConfirmPopup : AbstractPopup
{
    //  Properties & fields
    public override Enums.PopupStates AssociatedPopupState { get { return Enums.PopupStates.Confirm; } }

    [SerializeField] private Text _messageText;
    [SerializeField] private Button _okButton;
    [SerializeField] private Button _cancelButton;
    private bool _hasChoiceBeenMade = false;
    private bool _userChoice;

    //  Monobehavior methods

    private void Awake()
    {
        _okButton.onClick.AddListener(OkButton_OnClick);
        _cancelButton.onClick.AddListener(CancelButton_OnClick);
    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
    }

    //  Methods

    public new async Task<bool> OpenPopup(string message)
    {
        bool success = OpenPopup();
        bool userChoice = false;

        if (success)
        {
            _messageText.text = message;
            userChoice = await Task.Run(() => GetUserChoice());
        }

        ClosePopup();
        return userChoice;
    }

    public override bool OpenPopup()
    {
        return base.OpenPopup();
    }

    protected override void ClosePopup()
    {
        _hasChoiceBeenMade = false;
        _userChoice = false;
        base.ClosePopup();
    }

    private bool GetUserChoice()
    {
        while (!_hasChoiceBeenMade)
        {
            Thread.Sleep(100);
        }

        return _userChoice;
    }

    private void OkButton_OnClick()
    {
        _userChoice = true;
        _hasChoiceBeenMade = true;
    }

    private void CancelButton_OnClick()
    {
        _userChoice = false;
        _hasChoiceBeenMade = true;
    }
}
