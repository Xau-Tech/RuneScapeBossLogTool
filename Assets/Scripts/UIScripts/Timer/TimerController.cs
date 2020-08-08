using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  Controller for timer mvc system
public class TimerController
{
    private TimerDisplay view;

    public TimerController(TimerDisplay view)
    {
        this.view = view;
        EventManager.Instance.onTimerUpdated += view.SetText;
    }
}
