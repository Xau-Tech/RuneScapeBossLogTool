using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//  Class for UI events
public class EventManager : MonoBehaviour
{
    public static EventManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public event Action onBossDropdownValueChanged;
    public void BossDropdownValueChanged()
    {
        if (onBossDropdownValueChanged != null)
        {
            onBossDropdownValueChanged();
            Debug.Log("BossDropdownChanged event");
        }
    }

    public event Action<string, int> onAddItemButtonClicked;
    public void AddItemButtonClicked(string _item, int _amount)
    {
        if (onAddItemButtonClicked != null)
        {
            onAddItemButtonClicked(_item, _amount);
            Debug.Log("AddItemButton event");
        }
    }

    public event Action onItemDropdownValueChanged;
    public void ItemDropdownValueChanged()
    {
        if (onItemDropdownValueChanged != null)
        {
            onItemDropdownValueChanged();
            Debug.Log("ItemDropdownChanged event");
        }
    }

    public event Action onLogAdded;
    public void LogAdded()
    {
        if (onLogAdded != null)
        {
            onLogAdded();
            Debug.Log("LogAdded event");
        }
    }

    public event Action onRemoveDropClicked;
    public void RemoveDropClicked()
    {
        if (onRemoveDropClicked != null)
        {
            onRemoveDropClicked();
            Debug.Log("RemoveDrop event");
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

    public event Action onLogsPopulated;
    public void LogsPopulated()
    {
        if (onLogsPopulated != null)
        {
            onLogsPopulated();
            Debug.Log("LogsPopulated event");
        }
    }

    public event Action onTabSwitched;
    public void TabSwitched()
    {
        if (onTabSwitched != null)
        {
            onTabSwitched();
            Debug.Log("TabSwitched event");
        }
    }

    public event Action onLogDropdownValueChanged;
    public void LogDropDownValueChanged()
    {
        if (onLogDropdownValueChanged != null)
        {
            onLogDropdownValueChanged();
            Debug.Log("LogDropdownChanged event");
        }
    }

    public event Action<string> onInputWarningOpen;
    public void InputWarningOpen(string _message)
    {
        if (onInputWarningOpen != null)
        {
            onInputWarningOpen(_message);
            Debug.Log("InputWarningOpen event");
        }
    }

    public event Action<string> onErrorOpen;
    public void ErrorOpen(string _message)
    {
        if(onErrorOpen != null)
        {
            onErrorOpen(_message);
            Debug.Log("ErrorOpen event");
        }
    }

    public event Action<string> onConfirmOpen;
    public void ConfirmOpen(string _message)
    {
        if (onConfirmOpen != null)
        {
            onConfirmOpen(_message);
            Debug.Log("ConfirmOpen event");
        }
    }

    public event Action onLogDeleted;
    public void LogDeleted()
    {
        if (onLogDeleted != null)
        {
            onLogDeleted();
            Debug.Log("LogDeleted event");
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

    public event Action onDropListGenerated;
    public void DropListGenerated()
    {
        if(onDropListGenerated != null)
        {
            onDropListGenerated();
            Debug.Log("DropListGenerated event");
        }
    }
}
