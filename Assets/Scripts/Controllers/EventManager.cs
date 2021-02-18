using UnityEngine;
using System;
using System.Threading.Tasks;

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

    public event Action onSetupUIFilled;
    public void SetupUIFilled()
    {
        onSetupUIFilled?.Invoke();
    }

    public event Action onUILoaded;
    public void UILoaded()
    {
        onUILoaded?.Invoke();
    }

    public event Action onSetupItemDictionaryLoaded;
    public void SetupItemDictionaryLoaded()
    {
        onSetupItemDictionaryLoaded?.Invoke();
    }

    public event Action onSetupDeleted;
    public void SetupDeleted()
    {
        onSetupDeleted?.Invoke();
    }

    public event Action<string> onSetupAdded;
    public void SetupAdded(in string name)
    {
        onSetupAdded?.Invoke(name);
    }

    public event Action onSmithingUpdated;
    public void SmithingUpdated()
    {
        onSmithingUpdated?.Invoke();
    }

    public event Action<SetupItem, int> onEquipmentAdded;
    public void EquipmentAdded(in SetupItem setupItem, in int slotID)
    {
        onEquipmentAdded?.Invoke(setupItem, slotID);
    }

    public event Action<SetupItem, uint, int> onInventoryItemAdded;
    public void InventoryItemAdded(in SetupItem setupItem, in uint quantity, in int slotID)
    {
        onInventoryItemAdded?.Invoke(setupItem, quantity, slotID);
    }

    public event Action<SetupItem, uint, int> onPrefightItemAdded;
    public void PrefightItemAdded(in SetupItem setupItem, uint quantity, int slotID)
    {
        onPrefightItemAdded?.Invoke(setupItem, quantity, slotID);
    }

    public event Action<SetupItem, uint, int> onBeastOfBurdenItemAdded;
    public void BeastOfBurdenItemAdded(in SetupItem setupItem, in uint quantity, in int slotID)
    {
        onBeastOfBurdenItemAdded?.Invoke(setupItem, quantity, slotID);
    }

    public event Action<GameObject> onPointerEnterSetupItemSubMenu;
    public void PointerEnterSetupItemSubMenu(in GameObject obj)
    {
        onPointerEnterSetupItemSubMenu?.Invoke(obj);
    }

    //  Call when the user has added data to a log
    public event Action onLogUpdated;
    public void LogUpdated()
    {
        onLogUpdated?.Invoke();
    }

    //  Event for renaming a log
    public event Action<string> onLogRename;
    public void LogRename(in string newLogName)
    {
        onLogRename?.Invoke(newLogName);
    }

    //  Call when the user clicks to change to a new tab
    public event Action onTabChanged;
    public void TabChanged()
    {
        onTabChanged?.Invoke();
    }

    //  Call when the user changes the version in options to change out bossdictionary
    public event Action onRSVersionChanged;
    public void RSVersionChanged()
    {
        onRSVersionChanged?.Invoke();
    }

    public event Action onTimerUpdated;
    public void TimerUpdated()
    {
        onTimerUpdated?.Invoke();
    }

    public event Action onKillcountUpdated;
    public void KillcountUpdated()
    {
        onKillcountUpdated?.Invoke();
    }

    public event Action onBossDropdownValueChanged;
    public void BossDropdownValueChanged()
    {
        if (onBossDropdownValueChanged != null)
        {
            DataState.CurrentState = DataState.states.Loading;
            onBossDropdownValueChanged();
        }
    }

    public event Action<string> onLogAdded;
    public void LogAdded(in string logName)
    {
        onLogAdded?.Invoke(logName);
    }

    public event Action onLogDeleted;
    public void LogDeleted()
    {
        onLogDeleted?.Invoke();
    }

    public event Action<int> onDropListModified;
    public void DropListModified(int itemID)
    {
        onDropListModified?.Invoke(itemID);
    }

    public event Action onItemsLoaded;
    public void ItemsLoaded()
    {
        onItemsLoaded?.Invoke();
    }

    public event Action onBossInfoLoaded;
    public void BossInfoLoaded()
    {
        onBossInfoLoaded?.Invoke();
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

    public event Action onLogsLoaded;
    public void LogsLoaded()
    {
        onLogsLoaded?.Invoke();
    }

    public event Action onLogsSaved;
    public void LogsSaved()
    {
        onLogsSaved?.Invoke();
    }

    public event Action onDataLoaded;
    public void DataLoaded()
    {
        onDataLoaded?.Invoke();
    }

    //  Setup events    //

    public event Func<string, Task> onNewUsernameEntered;
    public void NewUsernameEntered(in string value)
    {
        onNewUsernameEntered?.Invoke(value);
    }
}
