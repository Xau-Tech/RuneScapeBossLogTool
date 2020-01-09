using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//  Handle references to all UI objects
public class UIController : MonoBehaviour
{
    public static UIController uicontroller;
    public Dropdown m_ItemListDropdown;
    public InputField m_ItemAmountInputField;
    public Button m_RemoveDropButton;
    public Text m_DropsValueText;
    public GameObject m_DropsPanel;
    public GameObject m_LogsPanel;
    public GameObject m_SetupPanel;
    public Button m_ToolbarDropsButton;
    public Text m_TimerText;
    public Text m_TimerStartStopButtonText;
    public Text m_KillcountText;
    public Dropdown m_LogListDropdown;
    public GameObject m_ClickBlocker;
    public RawImage m_NewLogWindow;
    public EventSystem m_EventSystem;

    public DropListController DropListController { get { return m_DropListController; } }
    public bool HasFinishedLoading { get { return m_HasFinishedInitialLoading; } }

    private bool m_HasFinishedInitialLoading;

    [SerializeField]
    private DropListController m_DropListController;
    [SerializeField]
    private Dropdown m_Drops_BossDropdown, m_Logs_BossDropdown;


    private void Awake()
    {
        m_HasFinishedInitialLoading = false;

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
        m_HasFinishedInitialLoading = true;
    }



    private void OnEnable()
    {
        //EventManager.manager.onBossDropdownValueChanged += PopulateLogDropdown;
        //EventManager.manager.onLogAdded += PopulateLogDropdown;
    }


    private void OnDisable()
    {
        //EventManager.manager.onBossDropdownValueChanged -= PopulateLogDropdown;
        //EventManager.manager.onLogAdded -= PopulateLogDropdown;
    }

    public void Setup()
    {
        m_DropsPanel.SetActive(true);
        m_LogsPanel.SetActive(true);
        m_SetupPanel.SetActive(true);
        //  Add the boss names to the BossListDropdown in the Drops tab
        PopulateBossDropdowns();

        m_LogsPanel.SetActive(false);
        m_SetupPanel.SetActive(false);

        EventManager.manager.BossDropdownValueChanged();
    }


    private void PopulateBossDropdowns()
    {
        List<string> names = DataController.dataController.BossInfoList.GetBossNames();

        m_Drops_BossDropdown.AddOptions(names);
        m_Logs_BossDropdown.AddOptions(names);
    }


    //  Return string of the current selected value in the BossListDropdown
    public string GetCurrentBoss()
    {
        if (ProgramState.CurrentState == ProgramState.states.Drops)
            return m_Drops_BossDropdown.options[m_Drops_BossDropdown.value].text;
        else if (ProgramState.CurrentState == ProgramState.states.Logs)
            return m_Logs_BossDropdown.options[m_Logs_BossDropdown.value].text;
        else
            return null;
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

        EventManager.manager.TabSwitched();
    }


    public void OnToolbarLogButtonClicked()
    {
        //  Set proper panel active states
        m_DropsPanel.SetActive(false);
        m_LogsPanel.SetActive(true);
        m_SetupPanel.SetActive(false);

        //  Set app state
        ProgramState.CurrentState = ProgramState.states.Logs;

        EventManager.manager.TabSwitched();
    }


    public void OnToolbarSetupButtonClicked()
    {
        //  Set proper panel active states
        m_DropsPanel.SetActive(false);
        m_LogsPanel.SetActive(false);
        m_SetupPanel.SetActive(true);

        //  Set app state
        ProgramState.CurrentState = ProgramState.states.Setup;

        EventManager.manager.TabSwitched();
    }
}
