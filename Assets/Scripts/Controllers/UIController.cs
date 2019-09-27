using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//  Handle references to all UI objects
public class UIController : MonoBehaviour
{
    public static UIController uicontroller;
    public Dropdown m_BossListDropdown;
    public Dropdown m_ItemListDropdown;
    public InputField m_ItemAmountInputField;
    public DropListController DropListController { get { return m_DropListController; } }
    public Button m_RemoveDropButton;
    public Text m_DropsValueText;



    [SerializeField]
    private DropListController m_DropListController;



    private void Awake()
    {
        if (uicontroller == null)
        {
            DontDestroyOnLoad(gameObject);
            uicontroller = this;
        }
        else if (uicontroller != this)
        {
            Destroy(gameObject);
        }
    }



    private void OnEnable()
    {
        EventManager.OnBossListDropdownChanged += OnBossListValueChanged;
        EventManager.OnAddButtonClicked += OnAddButtonClicked;
    }


    private void OnDisable()
    {
        EventManager.OnBossListDropdownChanged -= OnBossListValueChanged;
        EventManager.OnAddButtonClicked -= OnAddButtonClicked;
    }


    // If the boss is changed, reset the ItemAmountInputField text and re-generate the drop list UI
    private void OnBossListValueChanged()
    {
        m_ItemAmountInputField.text = "";
        m_DropListController.GenerateList();
    }


    //  Run when the Add button is clicked in the Drops tab to add a drop to the list
    private void OnAddButtonClicked()
    {
        m_ItemAmountInputField.text = "";
    }


    
    public void Setup()
    {
        //  Add the boss names to the BossListDropdown in the Drops tab
        m_BossListDropdown.AddOptions(DataController.dataController.BossInfoList.GetBossNames());
    }


    //  Add all the items to the ItemListDropdown in the Drops tab
    public void PopulateItemDropdown()
    {
        m_ItemListDropdown.ClearOptions();

        m_ItemListDropdown.AddOptions(DataController.dataController.ItemListClass.GetItemNames());
    }


    //  Return string of the current selected value in the BossListDropdown
    public string GetCurrentBoss()
    {
        return m_BossListDropdown.options[m_BossListDropdown.value].text;
    }
}
