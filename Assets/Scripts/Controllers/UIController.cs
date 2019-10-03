﻿using System.Collections;
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
    public GameObject m_DropsPanel;
    public GameObject m_LogsPanel;
    public GameObject m_SetupPanel;
    public Button m_ToolbarDropsButton;



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

        m_ToolbarDropsButton.Select();
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


    /*
     * Toolbar Button OnClick functions
     */
     public void OnToolbarDropButtonClicked()
    {
        //  Set proper panel active states
        m_DropsPanel.SetActive(true);
        m_LogsPanel.SetActive(false);
        m_SetupPanel.SetActive(false);

        //  Set app state
        ProgramState.CurrentState = ProgramState.states.Drops;
    }


    public void OnToolbarLogButtonClicked()
    {
        //  Set proper panel active states
        m_DropsPanel.SetActive(false);
        m_LogsPanel.SetActive(true);
        m_SetupPanel.SetActive(false);

        //  Set app state
        ProgramState.CurrentState = ProgramState.states.Logs;
    }


    public void OnToolbarSetupButtonClicked()
    {
        //  Set proper panel active states
        m_DropsPanel.SetActive(false);
        m_LogsPanel.SetActive(false);
        m_SetupPanel.SetActive(true);

        //  Set app state
        ProgramState.CurrentState = ProgramState.states.Setup;
    }
}
