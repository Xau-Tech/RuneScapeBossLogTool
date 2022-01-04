using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Abstract class representing any sort of popup window (confirm, input windows, options, notifications, etc)
/// </summary>
public abstract class AbstractPopup : AbstractPanel
{
    //  Properties & fields
    public abstract Enums.PopupStates AssociatedPopupState { get; }

    //  Monobehaviors
    protected virtual void OnEnable()
    {
        SetPopupState(AssociatedPopupState);
    }

    protected virtual void OnDisable()
    {
        SetPopupState(Enums.PopupStates.None);
    }

    //  Custom methods

    public virtual bool OpenPopup()
    {
        return ApplicationController.Instance.PanelStack.OpenPopup(this);
    }

    public virtual bool OpenPopup(string message)
    {
        return false;
    }

    public virtual bool OpenPopup(DropsTab dropTab)
    {
        return ApplicationController.Instance.PanelStack.OpenPopup(this);
    }

    protected virtual void ClosePopup()
    {
        ApplicationController.Instance.PanelStack.ClosePopup(this);
    }

    private void SetPopupState(Enums.PopupStates popupState)
    {
        AppState.PopupState = popupState;
    }
}
