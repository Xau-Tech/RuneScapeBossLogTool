using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class to track all data & UI for the setup tab
/// </summary>
public class SetupTab : AbstractTab
{
    //  Properties & fields
    public override Enums.TabStates AssociatedTabState { get { return Enums.TabStates.Setup; } }

    [SerializeField] private SetupView _setupView;
    [SerializeField] private Button _newSetupButton;
    [SerializeField] private Button _deleteSetupButton;
    [SerializeField] private ControlsView _controlsView;
    [SerializeField] private Dropdown _setupDropdown;
    [SerializeField] private InputField _instanceCostField;
    [SerializeField] private InputField _chargeDrainInputField;
    [SerializeField] private InputField _usernameInputField;
    [SerializeField] private InputField _prayerInputField;
    [SerializeField] private InputField _smithingInputField;
    [SerializeField] private Dropdown _combatIntensityDropdown;
    private Setup _setup;
    private readonly string _USERNAMEPREFSKEY = "Username";

    //  Monobehavior methods

    private void Awake()
    {
        _setupDropdown.onValueChanged.AddListener(SetupDropdown_OnValueChanged);
        _newSetupButton.onClick.AddListener(NewSetupButton_OnClick);
        _deleteSetupButton.onClick.AddListener(DeleteSetupButton_OnClick);
        _instanceCostField.onEndEdit.AddListener(SetInstanceCost);
        _chargeDrainInputField.onEndEdit.AddListener(ChargeDrainInputField_OnEndEdit);
        _usernameInputField.onEndEdit.AddListener(UsernameInputField_OnEndEdit);
        _prayerInputField.onEndEdit.AddListener(PrayerInputField_OnEndEdit);
        _smithingInputField.onEndEdit.AddListener(SmithingInputField_OnEndEdit);
        _combatIntensityDropdown.AddOptions(CombatIntensity.GetCombatIntensityNames());
        _combatIntensityDropdown.onValueChanged.AddListener(CombatIntensityDropdown_OnValueChanged);
    }

    private void Start()
    {
        //  Fill dropdown
        _setupDropdown.AddOptions(ApplicationController.Instance.Setups.GetNames());
        SetupDropdown_OnValueChanged(0);
    }

    protected override void OnEnable()
    {
        EventManager.Instance.onSetupItemAdded += AddQuantityOfSetupItem;
        base.OnEnable();
    }

    private void OnDisable()
    {
        EventManager.Instance.onSetupItemAdded -= AddQuantityOfSetupItem;
    }

    //  Methods

    public void SwitchSetup(Setup newSetup)
    {
        _controlsView.ShowBoth();

        Debug.Log("New setup is " + newSetup.SetupName);
        _setup = newSetup;
        base.CurrentSetup = _setup;
        ApplicationController.Instance.CurrentSetup = _setup;

        //  Set UI for all collections
        RefreshAllItems();

        //  Reset instance, intensity, charge drain UI
        _setupView.Display(newSetup);

        _setup.Player.Equipment.DetermineCost();
        DisplaySetupCost();

        _controlsView.ShowInventory();
    }

    /// <summary>
    /// Adds a quantity of setup items to the passed collection
    /// </summary>
    /// <param name="setupItem">Item to be added</param>
    /// <param name="quantity">Quantity to be added</param>
    /// <param name="collectionType">Collection type</param>
    /// <param name="itemSlotCategory">Category type</param>
    /// <param name="startIndex">Index to start adding if multiple of an unstackable item</param>
    public void AddQuantityOfSetupItem(SetupItem setupItem, uint quantity, Enums.SetupCollections collectionType, Enums.ItemSlotCategory itemSlotCategory, int startIndex)
    {
        //  If item is stackable, set the proper slot to passed item and quantity
        if (setupItem.IsStackable)
        {
            AddSetupItem(setupItem, quantity, collectionType, itemSlotCategory, startIndex);
        }
        else
        {
            //  Not stackable with quantity 1
            if (quantity == 1)
            {
                AddSetupItem(setupItem, 1, collectionType, itemSlotCategory, startIndex);
            }
            else if (ControlsView.Instance.WithdrawNotes() && quantity > 1)
            {
                AddSetupItem(setupItem, quantity, collectionType, itemSlotCategory, startIndex);
            }
            else
            {
                //  Not stackable with quantity over 1
                List<int> emptySlots = new List<int>();

                switch (collectionType)
                {
                    case Enums.SetupCollections.Inventory:
                        emptySlots = _setup.Player.Inventory.GetEmptySlots(startIndex, (int)quantity);
                        break;
                    case Enums.SetupCollections.Prefight:
                        emptySlots = _setup.Player.Prefight.GetEmptySlots(startIndex, (int)quantity);
                        break;
                    case Enums.SetupCollections.BoB:
                        emptySlots = _setup.Player.BeastOfBurden.GetEmptySlots(startIndex, (int)quantity);
                        break;
                    default:
                        emptySlots = null;
                        break;
                }

                foreach(int i in emptySlots)
                {
                    AddSetupItem(setupItem, 1, collectionType, itemSlotCategory, i);
                }
            }
        }
    }

    /// <summary>
    /// Adds an item to one slot (quantity is for stackable items)
    /// </summary>
    /// <param name="setupItem">The item to be added</param>
    /// <param name="quantity">The quantity to be added</param>
    /// <param name="collectionType">Collection type</param>
    /// <param name="itemSlotCategory">Item slot type</param>
    /// <param name="index">Slot number to add to</param>
    private void AddSetupItem(SetupItem setupItem, uint quantity, Enums.SetupCollections collectionType, Enums.ItemSlotCategory itemSlotCategory, int index)
    {
        //Debug.Log($"AddSetupItem [ Item: {setupItem.ItemName}, Quantity: {quantity}, Collection: {collectionType.ToString()}, ItemSlotCategory: {itemSlotCategory.ToString()}, Index: {index} ]");
        switch (collectionType)
        {
            case Enums.SetupCollections.Inventory:
                _setup.Player.Inventory.SetItemAtIndex(setupItem, quantity, index);
                break;
            case Enums.SetupCollections.Equipment:
                switch (itemSlotCategory)
                {
                    case Enums.ItemSlotCategory.Mainhand:
                        {
                            _setup.Player.Equipment.SetItemAtIndex(setupItem, 1, index);

                            //  Unequip offhand if mainhand is a twohand weapon
                            if (setupItem.GetItemCategory() == Enums.SetupItemCategory.TwoHand)
                            {
                                _setup.Player.Equipment.Offhand = General.NullItem();
                                _setupView.Display(General.NullItem(), 1, Equipment.OFFHANDINDEX, Enums.SetupCollections.Equipment);
                            }

                            break;
                        }
                    case Enums.ItemSlotCategory.Offhand:
                        {
                            //  Unequip mainhand if it is a twohand weapon
                            if (_setup.Player.Equipment.Mainhand.GetItemCategory() == Enums.SetupItemCategory.TwoHand)
                            {
                                _setup.Player.Equipment.Mainhand = General.NullItem();
                                _setupView.Display(General.NullItem(), 1, Equipment.MAINHANDINDEX, Enums.SetupCollections.Equipment);
                            }

                            _setup.Player.Equipment.SetItemAtIndex(setupItem, 1, index);
                            break;
                        }
                    default:
                        _setup.Player.Equipment.SetItemAtIndex(setupItem, 1, index);
                        break;
                }
                break;
            case Enums.SetupCollections.Prefight:
                _setup.Player.Prefight.SetItemAtIndex(setupItem, quantity, index);
                break;
            case Enums.SetupCollections.BoB:
                _setup.Player.BeastOfBurden.SetItemAtIndex(setupItem, quantity, index);
                break;
            default:
                break;
        }

        DisplaySetupCost();
        _setupView.Display(setupItem, quantity, index, collectionType);
        ApplicationController.Instance.HasUnsavedData = true;
    }

    public void SetInstanceCost(string text)
    {
        if(int.TryParse(text, out int newCost) && newCost >= 0)
        {
            _setup.InstanceCost = newCost;
            DisplaySetupCost();
            ApplicationController.Instance.HasUnsavedData = true;
        }
        else
        {
            _instanceCostField.text = _setup.InstanceCost.ToString();
        }
    }

    private void DisplaySetupCost()
    {
        _setupView.Display(_setup.TotalCost, _setup.Player.Equipment.TotalCost, _setup.Player.Inventory.TotalCost, _setup.Player.Prefight.TotalCost, _setup.Player.BeastOfBurden.TotalCost);
    }

    /// <summary>
    /// Update UI for every setup item collection
    /// </summary>
    private void RefreshAllItems()
    {
        RefreshCollection(Enums.SetupCollections.Inventory);
        RefreshCollection(Enums.SetupCollections.Equipment);
        RefreshCollection(Enums.SetupCollections.Prefight);
        RefreshCollection(Enums.SetupCollections.BoB);
    }

    private void RefreshCollection(Enums.SetupCollections collType)
    {
        List<ItemSlot> slots;

        switch (collType)
        {
            case Enums.SetupCollections.Inventory:
                slots = _setup.Player.Inventory.GetData();
                break;
            case Enums.SetupCollections.Equipment:
                slots = _setup.Player.Equipment.GetData();
                break;
            case Enums.SetupCollections.Prefight:
                slots = _setup.Player.Prefight.GetData();
                break;
            case Enums.SetupCollections.BoB:
                slots = _setup.Player.BeastOfBurden.GetData();
                break;
            default:
                slots = new List<ItemSlot>();
                break;
        }

        for(int i = 0; i < slots.Count; ++i)
        {
            _setupView.Display((SetupItem)slots[i].Item, slots[i].Quantity, i, collType);
        }
    }

    //  UI events

    private void SetupDropdown_OnValueChanged(int value)
    {
        string setupName = _setupDropdown.options[value].text;
        Setup newSetup;

        if (!ApplicationController.Instance.Setups.TryGetValue(setupName, out newSetup))
            throw new System.Exception($"{setupName} could not be found in the SetupDictionary!");

        SwitchSetup(newSetup);
    }

    private async void NewSetupButton_OnClick()
    {
        string newSetupName = await PopupManager.Instance.ShowInputPopup(InputPopup.ADDSETUP);
        List<string> setupNames = ApplicationController.Instance.Setups.GetNames();

        //  Clear and reload setup dropdown
        _setupDropdown.ClearOptions();
        _setupDropdown.AddOptions(setupNames);

        //  Set value to the newly added setup
        int newSetupIndex = setupNames.FindIndex(setupName => setupName.CompareTo(newSetupName) == 0);
        _setupDropdown.value = newSetupIndex;
        SetupDropdown_OnValueChanged(newSetupIndex);
    }

    private async void DeleteSetupButton_OnClick()
    {
        string setupToDelete = _setupDropdown.options[_setupDropdown.value].text;

        //  Make sure there is a setup to delete
        if (!ApplicationController.Instance.Setups.ContainsKey(setupToDelete))
        {
            PopupManager.Instance.ShowNotification($"There is no setup called {setupToDelete} to delete!");
            return;
        }

        //  Confirm the user wants to delete the setup
        bool delete = await PopupManager.Instance.ShowConfirm($"Are you sure you want to delete the {setupToDelete} setup?");
        if (delete)
        {
            ApplicationController.Instance.Setups.Remove(setupToDelete);
            //  Clear and reload setup dropdown
            _setupDropdown.ClearOptions();
            _setupDropdown.AddOptions(ApplicationController.Instance.Setups.GetNames());

            //  Set to top value
            _setupDropdown.value = 0;
            SetupDropdown_OnValueChanged(0);
        }
    }

    //  Charge drain value updated
    private void ChargeDrainInputField_OnEndEdit(string text)
    {
        float rate;

        if (!float.TryParse(text, out rate))
            return;

        if(rate < 0)
        {
            PopupManager.Instance.ShowNotification($"Charge drain rate must be a positive value!");
            _chargeDrainInputField.text = _setup.ChargeDrainRate.ToString();
            return;
        }

        if (rate != _setup.ChargeDrainRate)
        {
            _setup.SetChargeDrainRate(rate);
            DisplaySetupCost();
            ApplicationController.Instance.HasUnsavedData = true;
        }
    }

    private void CombatIntensityDropdown_OnValueChanged(int value)
    {
        _setup.SetCombatIntensity((Enums.CombatIntensityLevels)value);
        DisplaySetupCost();
        ApplicationController.Instance.HasUnsavedData = true;
    }

    private void PrayerInputField_OnEndEdit(string text)
    {
        if(!TryGetSkillLevel(text, out sbyte level))
        {
            PopupManager.Instance.ShowNotification($"Levels must be an integer greater than 0!");
            _prayerInputField.text = _setup.Player.PrayerLevel.ToString();
        }
        else
        {
            _setup.Player.SetLevel(Enums.SkillName.Prayer, level);
        }
    }

    private void SmithingInputField_OnEndEdit(string text)
    {
        if (!TryGetSkillLevel(text, out sbyte level))
        {
            PopupManager.Instance.ShowNotification($"Levels must be an integer greater than 0!");
            _smithingInputField.text = _setup.Player.SmithingLevel.ToString();
        }
        else
        {
            _setup.Player.SetLevel(Enums.SkillName.Smithing, level);
            DisplaySetupCost();
        }
    }

    private async void UsernameInputField_OnEndEdit(string text)
    {
        if (string.IsNullOrEmpty(text))
            return;

        await _setup.LoadNewPlayerStatsAsync(text);
        _setupView.Display(_setup.Player);
        DisplaySetupCost();
        PlayerPrefs.SetString(_USERNAMEPREFSKEY, text);
        PlayerPrefs.Save();
    }

    private bool TryGetSkillLevel(string text, out sbyte level)
    {
        if(sbyte.TryParse(text, out level))
        {
            if(level < 1)
                return false;
            else
                return true;
        }
        else
        {
            return false;
        }
    }
}