using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//  Handle UI control
public class UIController : MonoBehaviour
{
    public static UIController Instance;

    public SetupItemMenuController SetupItemMenu { get { return setupItemMenu.GetComponent<SetupItemMenuController>(); } }

    [SerializeField] private GameObject setupItemMenu;
    [SerializeField] private GameObject dropsPanel;
    [SerializeField] private GameObject logsPanel;
    [SerializeField] private GameObject setupPanel;
    [SerializeField] private TabChanger tabChanger;
    [SerializeField] private GameObject inputRestrictPanel;
    [SerializeField] private Sprite[] loadSprites;
    [SerializeField] private GameObject optionWindow;
    [SerializeField] private Transform bossTotalsWidgetLoc;
    [SerializeField] private Transform logTotalsWidgetLoc;
    [SerializeField] private Dropdown logsTab_BossDropdown;
    [SerializeField] private Dropdown logsTab_LogDropdown;
    [SerializeField] private TimerDisplay timerDisplay;

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

        ResetPanels();
        optionWindow.SetActive(true);

        //  Setup the permanent display UI for our Logs tab
        BossDropdownDisplayLink bossDropdownDisplayLink = logsTab_BossDropdown.GetComponent<BossDropdownDisplayLink>();
        LogDropdownDisplayLink logDropdownDisplayLink = logsTab_LogDropdown.GetComponent<LogDropdownDisplayLink>();

        //  Create widgets
        bossDropdownDisplayLink.LinkAndCreateWidget(bossTotalsWidgetLoc.gameObject);
        logDropdownDisplayLink.LinkAndCreateWidget(logTotalsWidgetLoc.gameObject);

        //  Setup the LogDropdownDisplay to update its bossName when the BossDropdown value in the Logs tab is changed
        bossDropdownDisplayLink.AddValueChangedAction(logDropdownDisplayLink.SetBoss);

        //  Timer setup
        TimerController timerController = new TimerController(timerDisplay);
    }

    public void ResetPanels()
    {
        dropsPanel.SetActive(false);
        logsPanel.SetActive(false);
        setupPanel.SetActive(false);

        dropsPanel.SetActive(true);
        logsPanel.SetActive(true);
        setupPanel.SetActive(true);
    }

    public void LateSetup()
    {
        optionWindow.SetActive(false);
        logsPanel.SetActive(false);
        setupPanel.SetActive(false);

        //  Select the drops tab
        tabChanger.SelectDropsTab();
    }

    //  Fetches our OptionUI script for our OptionController
    public OptionUI GetOptionUIScript()
    {
        OptionUI script = optionWindow.GetComponentInChildren<OptionUI>();

        if (!script)
            throw new System.Exception("You forgot to add the OptionUI.cs script to the option window!");

        return script;
    }

    //  Open UI to prevent input during setup/loading/saving
    public void InputRestrictStart(in string _text)
    {
        Image img = inputRestrictPanel.GetComponent<Image>();

        //  Image to use when application is doing initial setup or switching between RSVersions
        if (ProgramState.CurrentState == ProgramState.states.Loading)
        {
            img.sprite = loadSprites[Random.Range(0, loadSprites.Length - 1)];
            img.color = Color.white;
        }
        //  Image to use when doing any other saving or loading
        else
        {
            img.sprite = null;
            Color clr = Color.white;
            clr.a = (100f / 255f);
            img.color = clr;
        }
        
        inputRestrictPanel.SetActive(true);

        Text t = inputRestrictPanel.GetComponentInChildren<Text>();
        t.text = _text;
    }

    //  Close above UI
    public void InputRestrictEnd()
    {
        inputRestrictPanel.SetActive(false);
    }

    public void OnToolbarDropButtonClicked()
    {
        //  Set proper panel active states
        dropsPanel.SetActive(true);
        logsPanel.SetActive(false);
        setupPanel.SetActive(false);

        //  Set app state
        ProgramState.CurrentState = ProgramState.states.Drops;
    }
}
