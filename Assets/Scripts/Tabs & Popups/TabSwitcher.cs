using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Enable buttons to switch between application tabs
/// </summary>
public class TabSwitcher : MonoBehaviour
{
    //  Properties & fields
    [SerializeField] private Button _dropsButton;
    [SerializeField] private Button _logsButton;
    [SerializeField] private Button _setupButton;
    [SerializeField] private Button _bossInfoButton;
    [SerializeField] private Button _abilitiesButton;
    [SerializeField] private Sprite _selectedButtonSprite;
    [SerializeField] private Sprite _unselectedButtonSprite;
    private List<Button> _buttonList = new List<Button>();

    //  Monobehavior methods

    private void Awake()
    {
        _buttonList.Add(_dropsButton);
        _buttonList.Add(_logsButton);
        _buttonList.Add(_setupButton);
        _buttonList.Add(_bossInfoButton);
        _buttonList.Add(_abilitiesButton);

        _dropsButton.onClick.AddListener(SelectDropsTab);
        _logsButton.onClick.AddListener(SelectLogsTab);
        _setupButton.onClick.AddListener(SelectSetupTab);
        _bossInfoButton.onClick.AddListener(SelectBossInfoTab);
        _abilitiesButton.onClick.AddListener(SelectAbilitiesTab);
    }

    //  Custom methods

    private void SelectDropsTab()
    {
        if (IsClickedTabActive(Enums.TabStates.Drops))
        {
            return;
        }
        else
        {
            ApplicationController.Instance.PanelStack.SwitchTabs(Enums.TabStates.Drops);
            SelectButton(_dropsButton);
        }
    }

    private void SelectLogsTab()
    {
        if (IsClickedTabActive(Enums.TabStates.Logs))
        {
            return;
        }
        else
        {
            ApplicationController.Instance.PanelStack.SwitchTabs(Enums.TabStates.Logs);
            SelectButton(_logsButton);
        }
    }

    private void SelectSetupTab()
    {
        if (IsClickedTabActive(Enums.TabStates.Setup))
        {
            return;
        }
        else
        {
            ApplicationController.Instance.PanelStack.SwitchTabs(Enums.TabStates.Setup);
            SelectButton(_setupButton);
        }
    }

    private void SelectBossInfoTab()
    {
        if (IsClickedTabActive(Enums.TabStates.BossInfo))
        {
            return;
        }
        else
        {
            ApplicationController.Instance.PanelStack.SwitchTabs(Enums.TabStates.BossInfo);
            SelectButton(_bossInfoButton);
        }
    }

    private void SelectAbilitiesTab()
    {
        if (IsClickedTabActive(Enums.TabStates.Abilities))
        {
            return;
        }
        else
        {
            ApplicationController.Instance.PanelStack.SwitchTabs(Enums.TabStates.Abilities);
            SelectButton(_abilitiesButton);
        }
    }

    private void SelectButton(Button buttonToSelect)
    {
        foreach(Button button in _buttonList)
        {
            if (button == buttonToSelect)
                button.GetComponent<Image>().sprite = _selectedButtonSprite;
            else
                button.GetComponent<Image>().sprite = _unselectedButtonSprite;
        }
    }

    private bool IsClickedTabActive(Enums.TabStates tabClicked)
    {
        return tabClicked == AppState.TabState;
    }
}
