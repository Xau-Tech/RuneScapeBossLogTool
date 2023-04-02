using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// Model view controller for the entire application
/// </summary>
public class ApplicationController : MonoBehaviour
{
    //  Properties & fields
    public static ApplicationController Instance { get { return _instance; } }
    public static OptionsController OptionController;
    public PanelStack PanelStack { get; } = new PanelStack();
    public float CanvasScale { get { return _view.CanvasScale; } }
    public BossInfoDictionary BossInfo { get { return _model.BossInfo; } }
    public BossLogDictionary BossLogs { get { return _model.BossLogs; } }
    public SetupDictionary Setups { get { return _model.Setups; } }
    public BossInfo CurrentBoss
    {
        get { return _model.CurrentBoss; }
        set
        {
            Debug.Log($"Current boss set to {value.BossName}");
            _model.CurrentBoss = value;
        }
    }
    public BossLog CurrentLog
    {
        get { return _model.CurrentLog; }
        set
        {
            Debug.Log($"Current log set to {value.logName}");
            _model.CurrentLog = value;
        }
    }
    public Setup CurrentSetup { get; set; }
    public float TimeElapsed { get { return _view.Timer.TimeElapsed; } }
    public static readonly string SHEETID = "17VtPcaQpCihEwABkID8xec40kD7Ijak5gcRhgDPaEhY";
    public bool HasUnsavedData { set { _model.HasUnsavedData = value; } get { return _model.HasUnsavedData; } }

    private static ApplicationController _instance = null;
    private AppModel _model = new AppModel();
    private AppView _view;
    private bool _wantsToQuit = false;
    private readonly string _versionSheet = "VersionInfo";

    //  Monobehavior methods

    private async void Awake()
    {
        //  Only debug in editor
        #if UNITY_EDITOR
                Debug.unityLogger.logEnabled = true;
        #else
            Debug.unityLogger.logEnabled = false;
        #endif

        if (_instance == null)
        {
            DontDestroyOnLoad(gameObject);
            _instance = this;
            _instance._view = GetComponent<AppView>();
            Application.wantsToQuit += Exit;

            AppState.ProgramState = Enums.ProgramStates.Loading;

            await Setup();

            //  Set current tab to drops
            AppState.ProgramState = Enums.ProgramStates.Running;
            PanelStack.SwitchTabs(Enums.TabStates.Drops);

            //  Check for newer version
            GoogleSheetsToUnity.SpreadsheetManager.ReadPublicSpreadsheet(new GoogleSheetsToUnity.GSTU_Search(SHEETID, _versionSheet), VersionCheckCallback);
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }

    private async void Update()
    {
        //  Only do update checks if the program state is set to running, data is loaded, and there are no open popup windows
        if (AppState.ProgramState == Enums.ProgramStates.Running && 
            AppState.DataState == Enums.DataStates.None &&
            AppState.PopupState == Enums.PopupStates.None)
        {
            if (Input.GetKeyDown(KeyCode.F12))
                OptionController.OpenOptions();
            else if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.S))
                await Save();

            if (_view.Timer.IsRunning)
            {
                _view.Timer.UpdateTime();
                _view.SetTimerText();
            } 
        }
    }

    //  Custom methods

    private async Task<string> Setup()
    {
        //  Set up options
        _view.OptionsView.gameObject.SetActive(true);
        OptionController = new OptionsController(new Options(), _view.OptionsView);
        await OptionController.Setup();
        _view.OptionsView.gameObject.SetActive(false);

        //  Set up panel stack
        PanelStack.Setup(_view.GetMainTabs());

        //  Data setup
        string rsVersion = OptionController.GetOptionValue(Enums.OptionNames.RSVersion);
        await _model.Setup(rsVersion);

        return "MVC setup done";
    }

    public async Task<string> Save()
    {
        //  Set data state to saving
        AppState.DataState = Enums.DataStates.Saving;

        _model.Save();
        _view.UpdateSaveText();

        //  Set data state back to normal
        AppState.DataState = Enums.DataStates.None;

        return "Done saving";
    }
    
    public void SetInputBlocker(bool active, string text)
    {
        if (active)
            _view.OpenInputBlocker(text);
        else
            _view.CloseInputBlocker();
    }

    private async void VersionCheckCallback(GoogleSheetsToUnity.GstuSpreadSheet ss)
    {
        string newestVersion = ss["A1"].value;
        string newVersionUrl = ss["B1"].value;

        if(Application.version.CompareTo(newestVersion) == -1)
        {
            bool choice = await PopupManager.Instance.ShowConfirm($"Version {newestVersion} is now available!  Would you like to be taken to the download link?");

            if (choice)
                Application.OpenURL(newVersionUrl);
        }
    }

    public void ForceExit()
    {
        Debug.Log("Force exit called");
        _wantsToQuit = true;
        Application.Quit();
    }

    public bool Exit()
    {
        AppState.ProgramState = Enums.ProgramStates.Exiting;

        if (_wantsToQuit)
            return true;

        if (HasUnsavedData)
        {
            ExitConfirm();
            return false;
        }
        else
        {
            return true;
        }
    }

    private async Task ExitConfirm()
    {
        _wantsToQuit = await PopupManager.Instance.ShowConfirm("There is unsaved data!  Are you sure you want to exit?");

        if (_wantsToQuit)
            Application.Quit();
        else
            AppState.ProgramState = Enums.ProgramStates.Running;
    }
}
