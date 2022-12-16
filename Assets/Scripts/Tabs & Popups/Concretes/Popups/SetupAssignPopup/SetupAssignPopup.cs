using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Popup to allow users to assign a Setup to BossLog(s)
/// </summary>
public class SetupAssignPopup : AbstractPopup
{
    //  Properties & fields
    public override Enums.PopupStates AssociatedPopupState { get { return Enums.PopupStates.AssignSetup; } }

    private string _setupName;
    private List<LogToggleInfo> _logToggleInfoList;
    [SerializeField] private Text _selectedSetupText;
    [SerializeField] private Button _enterButton;
    [SerializeField] private Button _cancelButton;
    [SerializeField] private BossLogsScrollList _bossLogsDisplay;

    //  Monobehavior methods

    private void Awake()
    {
        _enterButton.onClick.AddListener(EnterButton_OnClick);
        _cancelButton.onClick.AddListener(ClosePopup);
    }

    protected override void OnEnable()
    {
        _logToggleInfoList = new List<LogToggleInfo>();
        base.OnEnable();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
    }

    //  Methods

    public override bool OpenPopup(string message)
    {
        //pass setup name as argument and do any UI setup
        bool successfulOpen = base.OpenPopup();

        _setupName = message;
        _selectedSetupText.text = $"Select the boss log(s) to assign the {_setupName} setup to below:";
        _bossLogsDisplay.Setup(_setupName, LogDisplayToggleCallback);

        return successfulOpen;
    }

    protected override void ClosePopup()
    {
        //  Clear display for next time
        _bossLogsDisplay.Clear();
        base.ClosePopup();
    }

    private void EnterButton_OnClick()
    {
        //do any error checking
        //save any changed data

        foreach(LogToggleInfo lti in _logToggleInfoList)
        {
            Debug.Log($"Value toggled for {lti.BossName}:{lti.LogName}");

            //  Check current linked setup for referenced boss log
            short bossId = ApplicationController.Instance.BossInfo.GetId(lti.BossName);
            BossLog assocBossLog = ApplicationController.Instance.BossLogs.GetBossLog(bossId, lti.LogName);
            string previousSetupName = assocBossLog.setupName;

            //  If there was no previous setup linked, assign this one
            if (string.IsNullOrEmpty(previousSetupName))
            {
                assocBossLog.setupName = _setupName;
            }
            //  If boss log already had a setup linked
            else
            {
                //  If setups are the same, re-set setup to empty as if it was changed it must've been unchecked
                //  If setups are different, set to the new setup name, as it must've previously been linked to a different setup
                if (previousSetupName.CompareTo(_setupName) == 0)
                    assocBossLog.setupName = "";
                else
                    assocBossLog.setupName = _setupName;
            }
        }

        //  Mark BossLog info as changed
        ApplicationController.Instance.BossLogs.hasUnsavedData = true;

        //  Close
        ClosePopup();
    }

    private void LogDisplayToggleCallback(string bossName, string logName)
    {
        int toggleIndex = _logToggleInfoList.FindIndex(lti => lti.BossName == bossName && lti.LogName == logName);

        //  Index will be -1 if not found
        //  Add to list if not found, remove if found
        if (toggleIndex == -1)
        {
            LogToggleInfo lti;
            lti.BossName = bossName;
            lti.LogName = logName;
            _logToggleInfoList.Add(lti);
        }
        else
        {
            _logToggleInfoList.RemoveAt(toggleIndex);
        }
    }

    public struct LogToggleInfo
    {
        public string BossName;
        public string LogName;
    }
}
