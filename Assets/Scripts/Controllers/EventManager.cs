using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//  Class for UI events
public class EventManager : MonoBehaviour
{
    public static EventManager manager;

    private void Awake()
    {
        if (manager == null)
        {
            DontDestroyOnLoad(gameObject);
            manager = this;
        }
        else if (manager != this)
        {
            Destroy(gameObject);
        }
    }

    public event Action onBossDropdownValueChanged;
    public void BossDropdownValueChanged()
    {
        if (onBossDropdownValueChanged != null)
            onBossDropdownValueChanged();
    }

    public event Action onAddItemButtonClicked;
    public void AddItemButtonClicked()
    {
        if (onAddItemButtonClicked != null)
            onAddItemButtonClicked();
    }

    public event Action onItemDropdownValueChanged;
    public void ItemDropdownValueChanged()
    {
        if (onItemDropdownValueChanged != null)
            onItemDropdownValueChanged();
    }

    public event Action onLogAdded;
    public void LogAdded()
    {
        if (onLogAdded != null)
            onLogAdded();
    }

    public event Action onRemoveDropClicked;
    public void RemoveDropClicked()
    {
        if (onRemoveDropClicked != null)
            onRemoveDropClicked();
    }

    public event Action onItemsLoaded;
    public void ItemsLoaded()
    {
        if (onItemsLoaded != null)
            onItemsLoaded();
    }

    public event Action onLogsPopulated;
    public void LogsPopulated()
    {
        if (onLogsPopulated != null)
            onLogsPopulated();
    }

    public event Action onTabSwitched;
    public void TabSwitched()
    {
        if (onTabSwitched != null)
            onTabSwitched();
    }

    public event Action onLogDropdownValueChanged;
    public void LogDropDownValueChanged()
    {
        if (onLogDropdownValueChanged != null)
            onLogDropdownValueChanged();
    }

    public event Action<string> onInputWarningOpen;
    public void InputWarningOpen(string _message)
    {
        if (onInputWarningOpen != null)
            onInputWarningOpen(_message);
    }

    public event Action<string> onConfirmOpen;
    public void ConfirmOpen(string _message)
    {
        if (onConfirmOpen != null)
            onConfirmOpen(_message);
    }

    public event Action onLogDeleted;
    public void LogDeleted()
    {
        if (onLogDeleted != null)
            onLogDeleted();
    }
}
