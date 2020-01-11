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
    public Button m_ToolbarLogsButton;
    public Button m_ToolbarSetupButton;
    public Text m_TimerText;
    public Text m_TimerStartStopButtonText;
    public Text m_KillcountText;
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
    private ColorBlock m_SelectedTabColorblock;
    private ColorBlock m_UnselectedTabColorblock;


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

    public void Setup()
    {
        m_DropsPanel.SetActive(true);
        m_LogsPanel.SetActive(true);
        m_SetupPanel.SetActive(true);
        //  Add the boss names to the BossListDropdown in the Drops tab
        PopulateBossDropdowns();
        DataController.dataController.CurrentBoss = m_Drops_BossDropdown.options[m_Drops_BossDropdown.value].text;

        m_LogsPanel.SetActive(false);
        m_SetupPanel.SetActive(false);

        EventManager.manager.BossDropdownValueChanged();

        m_SelectedTabColorblock = m_ToolbarDropsButton.colors;
        m_SelectedTabColorblock.normalColor = m_SelectedTabColorblock.selectedColor;
        m_UnselectedTabColorblock = m_ToolbarLogsButton.colors;

        OnToolbarDropButtonClicked();
    }


    private void PopulateBossDropdowns()
    {
        List<string> names = DataController.dataController.BossInfoList.GetBossNames();

        m_Drops_BossDropdown.AddOptions(names);
        m_Logs_BossDropdown.AddOptions(names);
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

        //  Set proper colors
        m_ToolbarDropsButton.colors = m_SelectedTabColorblock;
        m_ToolbarLogsButton.colors = m_UnselectedTabColorblock;
        m_ToolbarSetupButton.colors = m_UnselectedTabColorblock;

        //  Set app state
        ProgramState.CurrentState = ProgramState.states.Drops;

        DataController.dataController.CurrentBoss = m_Drops_BossDropdown.options[m_Drops_BossDropdown.value].text;

        EventManager.manager.TabSwitched();
    }


    public void OnToolbarLogButtonClicked()
    {
        //  Set proper panel active states
        m_DropsPanel.SetActive(false);
        m_LogsPanel.SetActive(true);
        m_SetupPanel.SetActive(false);

        //  Set proper colors
        m_ToolbarDropsButton.colors = m_UnselectedTabColorblock;
        m_ToolbarLogsButton.colors = m_SelectedTabColorblock;
        m_ToolbarSetupButton.colors = m_UnselectedTabColorblock;

        //  Set app state
        ProgramState.CurrentState = ProgramState.states.Logs;

        DataController.dataController.CurrentBoss = m_Logs_BossDropdown.options[m_Logs_BossDropdown.value].text;

        EventManager.manager.TabSwitched();
    }


    public void OnToolbarSetupButtonClicked()
    {
        //  Set proper panel active states
        m_DropsPanel.SetActive(false);
        m_LogsPanel.SetActive(false);
        m_SetupPanel.SetActive(true);

        //  Set proper colors
        m_ToolbarDropsButton.colors = m_UnselectedTabColorblock;
        m_ToolbarLogsButton.colors = m_UnselectedTabColorblock;
        m_ToolbarSetupButton.colors = m_SelectedTabColorblock;

        //  Set app state
        ProgramState.CurrentState = ProgramState.states.Setup;

        EventManager.manager.TabSwitched();
    }
}
