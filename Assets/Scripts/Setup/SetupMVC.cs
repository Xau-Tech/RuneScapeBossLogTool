using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using UnityEngine;

//  MVC for the Setup
public class SetupMVC
{
    public SetupMVC(SetupView view)
    {
        model = new Setup("");
        this.view = view;

        //  Sub to events
        EventManager.Instance.onNewUsernameEntered += LoadNewPlayerStatsAsync;
        EventManager.Instance.onSmithingUpdated += DisplaySetupCost;
    }
    ~SetupMVC()
    {
        //  Unsub to events
        EventManager.Instance.onNewUsernameEntered -= LoadNewPlayerStatsAsync;
        EventManager.Instance.onSmithingUpdated -= DisplaySetupCost;
    }

    public string CurrentSetupName { get { return model.SetupName; } }
    public Player Player { get { return model.player; } }
    public float DegradePerHour { get { return model.DegradePerHour; } }
    public float ChargeDrainPerHour { get { return model.ChargeDrainPerHour; } }
    public float ChargeDrainRate { get { return model.ChargeDrainRate; } }
    public int InstanceCost { get { return model.InstanceCost; } }
    public RemoveSetupItemButton RemoveItemButton { get { return view.RemoveItemButton; } }

    private Setup model;
    private SetupView view;

    public void SwitchSetup(in Setup setup)
    {
        Debug.Log("New setup is " + setup.SetupName);
        model = setup;

        //  Set inventory UI
        List<ItemSlot> items = Player.Inventory.GetData();

        for(int i = 0; i < items.Count; ++i)
            EventManager.Instance.InventoryItemAdded(items[i].item as SetupItem, i);

        //  Set equipment UI
        items = Player.Equipment.GetData();

        for (int i = 0; i < items.Count; ++i)
            EventManager.Instance.EquipmentAdded(items[i].item as SetupItem, i);

        //  Reset instance, intensity, charge drain UI
        view.Display(in setup);

        DisplaySetupCost();
    }

    //  Load stats of a player from username asynchronously
    public async Task LoadNewPlayerStatsAsync(string username)
    {
        model.player = await SetupLoader.LoadPlayerStatsAsync(username);
        Debug.Log("Player loaded");
        view.Display(Player);
    }

    public void AddSetupItem(in SetupItem setupItem, in ItemSlotCategories itemSlotCategory, in int index)
    {
        switch (itemSlotCategory)
        {
            case ItemSlotCategories.Inventory:
                Player.Inventory.SetItemAtIndex(in setupItem, index);
                break;
            case ItemSlotCategories.Head:
                Player.Equipment.SetItemAtIndex(in setupItem, index);
                break;
            case ItemSlotCategories.Pocket:
                Player.Equipment.SetItemAtIndex(in setupItem, index);
                break;
            case ItemSlotCategories.Cape:
                Player.Equipment.SetItemAtIndex(in setupItem, index);
                break;
            case ItemSlotCategories.Necklace:
                Player.Equipment.SetItemAtIndex(in setupItem, index);
                break;
            case ItemSlotCategories.Ammunition:
                Player.Equipment.SetItemAtIndex(in setupItem, index);
                break;
            case ItemSlotCategories.Mainhand:
                {
                    Player.Equipment.SetItemAtIndex(in setupItem, index);

                    //  Unequip offhand if mainhand is a twohand weapon
                    if (setupItem.GetItemCategory() == SetupItemCategories.TwoHand)
                        Player.Equipment.Offhand = General.NullItem();

                    break;
                }
            case ItemSlotCategories.Body:
                Player.Equipment.SetItemAtIndex(in setupItem, index);
                break;
            case ItemSlotCategories.Offhand:
                {
                    //  Unequip mainhand if it is a twohand weapon
                    if (Player.Equipment.Mainhand.GetItemCategory() == SetupItemCategories.TwoHand)
                        Player.Equipment.Mainhand = General.NullItem();

                    Player.Equipment.SetItemAtIndex(in setupItem, index);

                    break;
                }
            case ItemSlotCategories.Legs:
                Player.Equipment.SetItemAtIndex(in setupItem, index);
                break;
            case ItemSlotCategories.Gloves:
                Player.Equipment.SetItemAtIndex(in setupItem, index);
                break;
            case ItemSlotCategories.Boots:
                Player.Equipment.SetItemAtIndex(in setupItem, index);
                break;
            case ItemSlotCategories.Ring:
                Player.Equipment.SetItemAtIndex(in setupItem, index);
                break;
            default:
                Debug.Log($"{itemSlotCategory.ToString()} could not be added to!");
                break;
        }

        //  Update price UI
        DisplaySetupCost();
        DataController.Instance.setupDictionary.HasUnsavedData = true;
    }

    public void SetCombatIntensity(in CombatIntensity.CombatIntensityLevels intensityLevel)
    {
        model.SetCombatIntensity(in intensityLevel);
        DisplaySetupCost();
        DataController.Instance.setupDictionary.HasUnsavedData = true;
    }

    public void SetChargeDrainRate(in float chargeDrainRate)
    {
        model.SetChargeDrainRate(in chargeDrainRate);
        DisplaySetupCost();
        DataController.Instance.setupDictionary.HasUnsavedData = true;
    }

    public void SetInstanceCost(in int instanceCost)
    {
        model.InstanceCost = instanceCost;
        DisplaySetupCost();
        DataController.Instance.setupDictionary.HasUnsavedData = true;
    }

    private void DisplaySetupCost()
    {
        view.DisplaySetupCost(model.TotalCost);
    }
}
