using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Popup for any general notifications which don't require user choice to continue
/// </summary>
public class NotificationPopup : AbstractPopup
{
    //  Properties & fields
    public override Enums.PopupStates AssociatedPopupState { get { return Enums.PopupStates.Notification; } }

    [SerializeField] private Text _messageText;
    [SerializeField] private Button _okButton;

    //  Monobehavior methods
    private void Awake()
    {
        _okButton.onClick.AddListener(ClosePopup);
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

    public override bool OpenPopup(string message)
    {
        bool success = OpenPopup();

        if (success)
        {
            _messageText.text = message;
            _okButton.Select();
        }

        return success;
    }

    public override bool OpenPopup()
    {
        return base.OpenPopup();
    }

    protected override void ClosePopup()
    {
        base.ClosePopup();
    }
}
