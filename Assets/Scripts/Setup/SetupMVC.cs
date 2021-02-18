using System.Collections.Generic;
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
        view.ShowInventoryAndBeastOfBurden();

        Debug.Log("New setup is " + setup.SetupName);
        model = setup;

        //  Set UI for all setupitem collections
        AbsItemSlotList items = Player.Inventory;
        items.FillUI();
        items = Player.Equipment;
        items.FillUI();
        items = Player.PrefightItems;
        items.FillUI();
        items = Player.BeastOfBurden;
        items.FillUI();

        //  Reset instance, intensity, charge drain UI
        view.Display(in setup);

        Player.Equipment.DetermineCost();
        DisplaySetupCost();

        EventManager.Instance.SetupUIFilled();
    }

    //  Load stats of a player from username asynchronously
    public async Task LoadNewPlayerStatsAsync(string username)
    {
        model.player = await SetupLoader.LoadPlayerStatsAsync(username);
        view.Display(Player);
    }

    //  Takes all calls to add new items to a Setup
    public void AddQuantityOfSetupItem(in SetupItem setupItem, in uint quantity, in SetupCollections collectionType, in ItemSlotCategories itemSlotCategory, in int startIndex)
    {
        //  If item is stackable, set the proper slot to passed item and quantity
        if (setupItem.isStackable)
        {
            AddSetupItem(in setupItem, in quantity, in collectionType, in itemSlotCategory, in startIndex);
        }
        else
        {
            //  Not stackable with quantity 1, set the proper slot to passed item w/ quantity 1
            if(quantity == 1)
            {
                AddSetupItem(in setupItem, 1, in collectionType, in itemSlotCategory, in startIndex);
            }
            else
            //  Not stackable with quantity other than 1, get a list of empty slots and set each to passed item w/ quantity 1
            {
                List<int> emptySlots;

                switch (collectionType)
                {
                    case SetupCollections.Inventory:
                        emptySlots = Player.Inventory.GetEmptySlots(startIndex, (int)quantity);
                        break;
                    case SetupCollections.Prefight:
                        emptySlots = Player.PrefightItems.GetEmptySlots(startIndex, (int)quantity);
                        break;
                    case SetupCollections.BoB:
                        emptySlots = Player.BeastOfBurden.GetEmptySlots(startIndex, (int)quantity);
                        break;
                    default:
                        emptySlots = null;
                        break;
                }

                foreach(int i in emptySlots)
                {
                    AddSetupItem(in setupItem, 1, in collectionType, in itemSlotCategory, in i);
                }
            }
        }
    }

    private void AddSetupItem(in SetupItem setupItem, in uint quantity, in SetupCollections collectionType, in ItemSlotCategories itemSlotCategory, in int index)
    {
        switch (collectionType)
        {
            case SetupCollections.Inventory:
                Player.Inventory.SetItemAtIndex(in setupItem, quantity, index);
                break;
            case SetupCollections.Equipment:
                switch (itemSlotCategory)
                {
                    case ItemSlotCategories.Head:
                        Player.Equipment.SetItemAtIndex(in setupItem, 1, index);
                        break;
                    case ItemSlotCategories.Pocket:
                        Player.Equipment.SetItemAtIndex(in setupItem, 1, index);
                        break;
                    case ItemSlotCategories.Cape:
                        Player.Equipment.SetItemAtIndex(in setupItem, 1, index);
                        break;
                    case ItemSlotCategories.Necklace:
                        Player.Equipment.SetItemAtIndex(in setupItem, 1, index);
                        break;
                    case ItemSlotCategories.Ammunition:
                        Player.Equipment.SetItemAtIndex(in setupItem, 1, index);
                        break;
                    case ItemSlotCategories.Mainhand:
                        {
                            Player.Equipment.SetItemAtIndex(in setupItem, 1, index);

                            //  Unequip offhand if mainhand is a twohand weapon
                            if (setupItem.GetItemCategory() == SetupItemCategories.TwoHand)
                                Player.Equipment.Offhand = General.NullItem();

                            break;
                        }
                    case ItemSlotCategories.Body:
                        Player.Equipment.SetItemAtIndex(in setupItem, 1, index);
                        break;
                    case ItemSlotCategories.Offhand:
                        {
                            //  Unequip mainhand if it is a twohand weapon
                            if (Player.Equipment.Mainhand.GetItemCategory() == SetupItemCategories.TwoHand)
                                Player.Equipment.Mainhand = General.NullItem();

                            Player.Equipment.SetItemAtIndex(in setupItem, 1, index);

                            break;
                        }
                    case ItemSlotCategories.Legs:
                        Player.Equipment.SetItemAtIndex(in setupItem, 1, index);
                        break;
                    case ItemSlotCategories.Gloves:
                        Player.Equipment.SetItemAtIndex(in setupItem, 1, index);
                        break;
                    case ItemSlotCategories.Boots:
                        Player.Equipment.SetItemAtIndex(in setupItem, 1, index);
                        break;
                    case ItemSlotCategories.Ring:
                        Player.Equipment.SetItemAtIndex(in setupItem, 1, index);
                        break;
                    default:
                        Debug.Log($"{itemSlotCategory.ToString()} could not be added to!");
                        break;
                }
                break;
            case SetupCollections.Prefight:
                Player.PrefightItems.SetItemAtIndex(in setupItem, quantity, index);
                break;
            case SetupCollections.BoB:
                Player.BeastOfBurden.SetItemAtIndex(in setupItem, quantity, index);
                break;
            default:
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
        view.DisplaySetupCost(model.TotalCost, model.player.Equipment.TotalCost, model.player.Inventory.TotalCost, model.player.PrefightItems.TotalCost, model.player.BeastOfBurden.TotalCost);
    }
}
