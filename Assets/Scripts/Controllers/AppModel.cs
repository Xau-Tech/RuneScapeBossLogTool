using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using GoogleSheetsToUnity;

/// <summary>
/// Model for ApplicationMVC
/// </summary>
public class AppModel : MonoBehaviour
{
    //  Properties & fields
    public BossInfoDictionary BossInfo { get; private set; }
    public BossLogDictionary BossLogs { get; private set; }
    public SetupDictionary Setups { get; private set; }
    public BossInfo CurrentBoss { get; set; }
    public BossLog CurrentLog { get; set; }
    public bool HasUnsavedData {
        get
        {
            if (!BossLogs.hasUnsavedData && !Setups.HasUnsavedData)
                return false;
            else
                return true;
        }
        set
        {
            BossLogs.hasUnsavedData = value;
            Setups.HasUnsavedData = value;
        }
    }

    private bool _setupItemPricesLoaded = false;
    private readonly string _USERNAMEPREFSKEY = "Username";

    //  Constructor
    public AppModel()
    {
        EventManager.Instance.onSetupItemPricesLoaded += SetupItemPricesLoaded;
    }

    ~AppModel()
    {
        EventManager.Instance.onSetupItemPricesLoaded -= SetupItemPricesLoaded;
    }

    //  Custom methods

    public async Task<string> Setup(string rsVersion)
    {
        BossInfo = new BossInfoDictionary();
        BossLogs = new BossLogDictionary();
        Setups = new SetupDictionary();
        List<Task<string>> setupTasks = new();

        //  Do setup necessary before loading
        BossInfo.Setup();
        BossLogs.DetermineFilePath(rsVersion);
        SetupItemDictionary.LoadResource();
            
        //  Await BossInfo load as BossLogDictionary needs data from it
        await BossInfo.Load(rsVersion);

        setupTasks.Add(Task.Run(() => RareItemDB.Load(rsVersion)));
        setupTasks.Add(Task.Run(() => BossLogs.Load(BossInfo.GetIds())));
        setupTasks.Add(Task.Run(() => SetupItemDictionary.LoadItems()));

        //  Wait for everything to finish loading
        await Task.WhenAll(setupTasks);

        //  Await setup item prices loading (must be called from main thread)
        SpreadsheetManager.ReadPublicSpreadsheet(SetupItemDictionary.GetSearch(), SetupItemDictionary.LoadPrices);
        await Task.Run(() => AwaitSetupItemPrices());

        //  Create an empty setup
        ApplicationController.Instance.CurrentSetup = new Setup("");

        //  Check if a username is stored and set+load from that if so
        if (PlayerPrefs.HasKey(_USERNAMEPREFSKEY))
            await ApplicationController.Instance.CurrentSetup.LoadNewPlayerStatsAsync(PlayerPrefs.GetString(_USERNAMEPREFSKEY));

        Setups.Load();

        HasUnsavedData = false;
        return "AppModel setup done";
    }

    public void Save()
    {
        BossLogs.Save();
        Setups.Save();
    }

    private async Task<string> AwaitSetupItemPrices()
    {
        while (!_setupItemPricesLoaded)
        {
            Thread.Sleep(100);
        }

        return "";
    }

    private void SetupItemPricesLoaded()
    {
        _setupItemPricesLoaded = true;
    }
}
