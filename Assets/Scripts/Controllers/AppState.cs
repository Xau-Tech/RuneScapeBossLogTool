using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// State class the overall application
/// </summary>
public static class AppState
{
    //  Properties & fields
    public static Enums.TabStates TabState
    {
        get { return _tabState; }
        set
        {
            _tabState = value;
            Debug.Log($"New tab state is {value.ToString()}");
        }
    }
    public static Enums.PopupStates PopupState
    {
        get { return _popupState; }
        set
        {
            _popupState = value;
            Debug.Log($"New popup state is {value.ToString()}");
        }
    }
    public static Enums.ProgramStates ProgramState
    {
        get { return _programState; }
        set
        {
            //  Previous state was loading and new state is set
            if(_programState == Enums.ProgramStates.Loading && value != Enums.ProgramStates.Loading)
            {
                _programState = value;
                ApplicationController.Instance.SetInputBlocker(false, "");
            }
            //  Program is loading
            else if(value == Enums.ProgramStates.Loading)
            {
                _programState = value;
                ApplicationController.Instance.SetInputBlocker(true, $"{value.ToString()}...");
            }
            else
            {
                _programState = value;
            }

            Debug.Log($"New program state is {value.ToString()}");
        }
    }
    public static Enums.DataStates DataState
    {
        get { return _dataState; }
        set
        {
            if(_dataState == Enums.DataStates.None)
            {
                //  New state is either saving or loading
                if(value != Enums.DataStates.None)
                {
                    //  Turn on input restriction
                    ApplicationController.Instance.SetInputBlocker(true, $"{value.ToString()}...");
                }
            }
            //  Program is in saving or loading state
            else if(_dataState == Enums.DataStates.Saving || _dataState == Enums.DataStates.Loading)
            {
                //  New state none
                if(value == Enums.DataStates.None)
                {
                    //  Turn off input restriction
                    ApplicationController.Instance.SetInputBlocker(false, "");
                }
            }

            _dataState = value;
            Debug.Log($"New data state is {value.ToString()}");
        }
    }

    private static Enums.ProgramStates _programState;
    private static Enums.DataStates _dataState;
    private static Enums.TabStates _tabState;
    private static Enums.PopupStates _popupState;

    //  Methods
    public static List<string> GetTabStates()
    {
        return new List<string> { Enums.TabStates.Drops.ToString(), Enums.TabStates.Logs.ToString(), Enums.TabStates.Setup.ToString(), Enums.TabStates.BossInfo.ToString() };
    }
}
