using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Abstract class representing any main tab of the application (Drops, Logs, Setup, etc)
/// </summary>
public abstract class AbstractTab : AbstractPanel
{
    //  Properties & fields
    protected BossInfo CurrentBoss;
    protected BossLog CurrentLog;
    protected Setup CurrentSetup;

    //  Monobehaviors
    protected virtual void OnEnable()
    {
        AppState.TabState = AssociatedTabState;

        if(this.CurrentBoss != null)
            ApplicationController.Instance.CurrentBoss = this.CurrentBoss;

        if(this.CurrentLog != null)
            ApplicationController.Instance.CurrentLog = this.CurrentLog;

        if(this.CurrentSetup != null)
            ApplicationController.Instance.CurrentSetup = this.CurrentSetup;
    }

    //  Custom methods

    public abstract Enums.TabStates AssociatedTabState { get; }
}
