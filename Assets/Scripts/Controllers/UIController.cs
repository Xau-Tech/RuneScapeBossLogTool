using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//  Handle UI control
public class UIController : MonoBehaviour
{
    public static UIController Instance;

    public DropListController DropListController { get { return m_DropListController; } }
    public Button RemoveDropButton { get { return m_RemoveDropButton; } }
    public GameObject DropsPanel { get { return m_DropsPanel; } }
    public GameObject LogsPanel { get { return m_LogsPanel; } }
    public GameObject ClickBlocker { get { return m_ClickBlocker; } }

    [SerializeField] private Button m_RemoveDropButton;
    [SerializeField] private GameObject m_DropsPanel;
    [SerializeField] private GameObject m_LogsPanel;
    [SerializeField] private GameObject m_SetupPanel;
    [SerializeField] private Button m_ToolbarDropsButton;
    [SerializeField] private Button m_ToolbarLogsButton;
    [SerializeField] private Button m_ToolbarSetupButton;
    [SerializeField] private GameObject m_ClickBlocker;
    [SerializeField] private GameObject m_InputRestrictPanel;
    [SerializeField]
    private DropListController m_DropListController;
    [SerializeField]
    private Dropdown m_Drops_BossDropdown, m_Logs_BossDropdown;
    [SerializeField] Text m_SaveText;
    [SerializeField] Sprite[] m_LoadSprites;

    private ColorBlock m_SelectedTabColorblock;
    private ColorBlock m_UnselectedTabColorblock;


    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        PopupState.currentState = PopupState.states.Loading;
        
        m_DropsPanel.SetActive(false);
        m_LogsPanel.SetActive(false);
        m_SetupPanel.SetActive(false);

        m_DropsPanel.SetActive(true);
        m_LogsPanel.SetActive(true);
        m_SetupPanel.SetActive(true);
        m_SelectedTabColorblock = m_ToolbarDropsButton.colors;
        m_SelectedTabColorblock.normalColor = m_SelectedTabColorblock.selectedColor;
        m_UnselectedTabColorblock = m_ToolbarLogsButton.colors;
    }

    public void InputRestrictStart(string _text)
    {
        Image img = m_InputRestrictPanel.GetComponent<Image>();

        if (ProgramState.CurrentState == ProgramState.states.Setup)
        {
            img.sprite = m_LoadSprites[Random.Range(0, m_LoadSprites.Length - 1)];
            img.color = Color.white;
        }
        else
        {
            img.sprite = null;
            Color clr = Color.white;
            clr.a = (100f / 255f);
            img.color = clr;
        }
        
        m_InputRestrictPanel.SetActive(true);

        Text t = m_InputRestrictPanel.GetComponentInChildren<Text>();
        t.text = _text;
    }

    public void InputRestrictEnd()
    {
        m_InputRestrictPanel.SetActive(false);
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

        DataController.Instance.CurrentBoss = m_Drops_BossDropdown.options[m_Drops_BossDropdown.value].text;

        EventManager.Instance.TabSwitched(DataController.Instance.CurrentDropTabLog);
    }

    public void OnToolbarLogButtonClicked()
    {
        //  Log time on switch from Drops tab so timer updates accurately when switched back to later
        if (ProgramState.CurrentState == ProgramState.states.Drops)
            TimerScript.Instance.TimeAtSwitch = Time.time;

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

        DataController.Instance.CurrentBoss = m_Logs_BossDropdown.options[m_Logs_BossDropdown.value].text;

        EventManager.Instance.TabSwitched(DataController.Instance.CurrentLogTabLog);
    }


    public void OnToolbarSetupButtonClicked()
    {
        //  Log time on switch from Drops tab so timer updates accurately when switched back to later
        if (ProgramState.CurrentState == ProgramState.states.Drops)
            TimerScript.Instance.TimeAtSwitch = Time.time;

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

        EventManager.Instance.TabSwitched("");
    }

    public void UpdateSaveText()
    {
        m_SaveText.text = "Saved at: " + System.DateTime.Now.ToString("T");
    }
}
