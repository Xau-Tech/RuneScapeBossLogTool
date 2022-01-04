using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Handles all UI for objects in the application
/// </summary>
public class OptionsView : AbstractPopup
{
    //  Properties & fields
    public override Enums.PopupStates AssociatedPopupState { get { return Enums.PopupStates.Options; } }

    private static GameObject _thisWindow;
    private static Dictionary<string, GameObject> _objectDictionary = new Dictionary<string, GameObject>();
    [SerializeField] private Button _xButton;
    [SerializeField] private Button _cancelButton;
    [SerializeField] private Button _saveButton;
    [SerializeField] private Dropdown[] _dropDownArray;
    [SerializeField] private Toggle[] _toggleArray;

    //  Monobehavior methods

    private void Awake()
    {
        _xButton.onClick.AddListener(ClosePopup);
        _cancelButton.onClick.AddListener(ClosePopup);
        _saveButton.onClick.AddListener(SaveOptions);
    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
    }

    //  Methods

    public override bool OpenPopup()
    {
        return base.OpenPopup();
    }

    protected override void ClosePopup()
    {
        EventManager.Instance.OptionUISetup();
        base.ClosePopup();
    }

    private void SaveOptions()
    {
        EventManager.Instance.OptionsUpdated();
    }

    public async Task Setup()
    {
        _thisWindow = this.gameObject;

        //  Populate our dictionary
        for(int i = 0; i < _dropDownArray.Length; ++i)
        {
            Dropdown dropdown = _dropDownArray[i];
            ListOption script = dropdown.GetComponent<ListOption>();
            _objectDictionary.Add(script.Name, dropdown.gameObject);
        }
        for(int i = 0; i < _toggleArray.Length; ++i)
        {
            Toggle toggle = _toggleArray[i];
            ToggleOption script = toggle.GetComponent<ToggleOption>();
            _objectDictionary.Add(script.Name, toggle.gameObject);
        }

        //  See note below
        await DisplaySelectedOptions();
    }

    /*
    * This must be called at minimum one frame after the Monobehavior::Awake() function because of some check(s) Unity
    * does on Toggle UI objects within Awake() that causes them to be null before and up to that point even when set
    * directly within the inspector window
    */
    public async Task DisplaySelectedOptions()
    {
        await Task.Delay((int)uint.MinValue);

        //  Triggers our option UI objects to have the proper value selected from our data class
        EventManager.Instance.OptionUISetup();
    }

    private static GameObject GetOptionObjectByName(string name)
    {
        _objectDictionary.TryGetValue(name, out GameObject go);
        return go;
    }

    //  Returns DisplayOption interface for use in initialization
    public static IDisplayOption GetDisplayInterfaceByOptionName(string name, Enums.OptionTypes optionType)
    {
        if (optionType == Enums.OptionTypes.Dropdown)
            return GetOptionObjectByName(name).GetComponent<ListOption>();
        else if (optionType == Enums.OptionTypes.Toggle)
            return GetOptionObjectByName(name).GetComponent<ToggleOption>();

        return null;
    }

    //  Update option values from UI actions
    public void UpdateOptions(List<GenericOption> optionList)
    {
        //  Iterate through gameobject dictionary
        foreach(KeyValuePair<string, GameObject> kvp in _objectDictionary)
        {
            //  Find the matching option
            GenericOption option = optionList.Find(opt => opt.Name.CompareTo(kvp.Key) == 0);
            string value = "";

            //  Determine value based on option type
            if(option.OptionType == Enums.OptionTypes.Dropdown)
            {
                Dropdown dropdown = kvp.Value.GetComponent<Dropdown>();
                value = dropdown.options[dropdown.value].text;
            }
            else if(option.OptionType == Enums.OptionTypes.Toggle)
            {
                Toggle toggle = kvp.Value.GetComponent<Toggle>();
                value = toggle.isOn.ToString();
            }

            //  If value has been changed and is not an empty string, set and apply changes
            if(value != option.Value && value != "")
            {
                option.Value = value;
                option.Apply();
                Debug.Log($"{option.Name}'s value set to {value}");
            }
        }
    }
}
