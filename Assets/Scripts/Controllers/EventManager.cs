using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles application events
/// </summary>
public class EventManager : MonoBehaviour
{
    //  Properties & fields
    public static EventManager Instance { get { return _instance; } }

    private static EventManager _instance = new EventManager();

    //  Events & actions

    public event Action onSetupItemPricesLoaded;
    public void SetupItemPricesLoaded()
    {
        onSetupItemPricesLoaded?.Invoke();
    }

    public event Action onRSVersionChanged;
    public void RSVersionChanged()
    {
        onRSVersionChanged?.Invoke();
    }

    public event Action onOptionUISetup;
    public void OptionUISetup()
    {
        onOptionUISetup?.Invoke();
    }

    public event Action onOptionsUpdated;
    public void OptionsUpdated()
    {
        onOptionsUpdated?.Invoke();
    }

    public event Action onBossItemsLoaded;
    public void BossItemsLoaded()
    {
        onBossItemsLoaded?.Invoke();
    }

    public event Action onLogDeleted;
    public void LogDeleted()
    {
        onLogDeleted?.Invoke();
    }

    public event Action<string> onLogRename;
    public void LogRename(string newName)
    {
        onLogRename?.Invoke(newName);
    }

    public event Action onLogsSaved;
    public void LogsSaved()
    {
        onLogsSaved?.Invoke();
    }

    public event Action<SetupItem, uint, Enums.SetupCollections, Enums.ItemSlotCategory, int> onSetupItemAdded;
    public void SetupItemAdded(SetupItem setupItem, uint quantity, Enums.SetupCollections collection, Enums.ItemSlotCategory itemSlot, int slotId)
    {
        onSetupItemAdded?.Invoke(setupItem, quantity, collection, itemSlot, slotId);
    }
}