using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//  Class for UI events
public class EventManager : MonoBehaviour
{
    private static EventManager instance = new EventManager();

    public static EventManager Instance
    {
        get
        {
            return instance;
        }
    }

    //  Call when the user has added data to a log
    public event Action onLogUpdated;
    public void LogUpdated()
    {
        if(onLogUpdated != null)
        {
            onLogUpdated();
            Debug.Log($"LogUpdated event");
        }
    }

    //  Event for renaming a log
    public event Action<string> onLogRename;
    public void LogRename(in string newLogName)
    {
        if(onLogRename != null)
        {
            onLogRename(newLogName);
            Debug.Log($"LogRename event");
        }
    }

    //  Call when the user clicks to change to a new tab
    public event Action onTabChanged;
    public void TabChanged()
    {
        if(onTabChanged != null)
        {
            onTabChanged();
            Debug.Log($"TabChanged event");
        }
    }

    //  Call when the user changes the version in options to change out bossdictionary
    public event Action onRSVersionChanged;
    public void RSVersionChanged()
    {
        if(onRSVersionChanged != null)
        {
            onRSVersionChanged();
            Debug.Log("RSVersionChanged event");
        }
    }

    public event Action onTimerUpdated;
    public void TimerUpdated()
    {
        if(onTimerUpdated != null)
        {
            onTimerUpdated();
        }
    }

    public event Action onKillcountUpdated;
    public void KillcountUpdated()
    {
        if(onKillcountUpdated != null)
        {
            onKillcountUpdated();
        }
    }

    public event Action onBossDropdownValueChanged;
    public void BossDropdownValueChanged()
    {
        if (onBossDropdownValueChanged != null)
        {
            DataState.CurrentState = DataState.states.Loading;
            onBossDropdownValueChanged();
            Debug.Log("BossDropdownChanged event");
        }
    }

    public event Action<string> onLogAdded;
    public void LogAdded(in string logName)
    {
        if (onLogAdded != null)
        {
            onLogAdded(logName);
            Debug.Log("LogAdded event");
        }
    }

    public event Action onLogDeleted;
    public void LogDeleted()
    {
        if(onLogDeleted != null)
        {
            onLogDeleted();
            Debug.Log($"LogDeleted event");
        }
    }

    public event Action onDropListModified;
    public void DropListModified()
    {
        if (onDropListModified != null)
        {
            onDropListModified();
            Debug.Log("DropListModified event");
        }
    }

    public event Action onItemsLoaded;
    public void ItemsLoaded()
    {
        if (onItemsLoaded != null)
        {
            onItemsLoaded();
            Debug.Log("ItemsLoaded event");
        }
    }

    public event Action onBossInfoLoaded;
    public void BossInfoLoaded()
    {
        if(onBossInfoLoaded != null)
        {
            onBossInfoLoaded();
            Debug.Log("BossInfoLoaded event");
        }
    }

    public event Action onOptionUISetup;
    public void OptionUISetup()
    {
        if(onOptionUISetup != null)
        {
            onOptionUISetup();
            Debug.Log("OptionUISetup event");
        }
    }

    public event Action onOptionsUpdated;
    public void OptionsUpdated()
    {
        if(onOptionsUpdated != null)
        {
            onOptionsUpdated();
            Debug.Log("OptionsUpdated event");
        }
    }

    public event Action onLogsLoaded;
    public void LogsLoaded()
    {
        if(onLogsLoaded != null)
        {
            onLogsLoaded();
            Debug.Log($"LogsLoaded event");
        }
    }

    public event Action onLogsSaved;
    public void LogsSaved()
    {
        if(onLogsSaved != null)
        {
            onLogsSaved();
            Debug.Log($"LogsSaved event");
        }
    }

    public event Action onDataLoaded;
    public void DataLoaded()
    {
        if(onDataLoaded != null)
        {
            onDataLoaded();
            Debug.Log($"DataLoaded event");
        }
    }

    //  Setup events    //

    public event Action<string> onNewUsernameEntered;
    public void NewUsernameEntered(in string value)
    {
        if(onNewUsernameEntered != null)
        {
            onNewUsernameEntered(value);
            Debug.Log($"NewUsernameEntered event");
        }
    }
}
