using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class to track all data & UI for the bossinfo tab
/// </summary>
public class BossInfoTab : AbstractTab
{
    //  Properties & fields

    public override Enums.TabStates AssociatedTabState { get { return Enums.TabStates.BossInfo; } }

    [SerializeField] private Dropdown _bossDropdown;
    [SerializeField] private BossInfoView _bossInfoView;
    [SerializeField] private Button _leftArrowButton;
    [SerializeField] private Button _rightArrowButton;
    private HitChanceController _hitChanceController;
    private List<BossCombatData> _subBossList = new List<BossCombatData>();
    private BossCombatData _currentSubBoss { get { return _subBossList[_subBossIndex]; } }
    private sbyte _subBossIndex = 0;

    //  Monobehavior methods

    private void Awake()
    {
        _bossDropdown.ClearOptions();
        _bossDropdown.AddOptions(ApplicationController.Instance.BossInfo.GetOrderedBossNames());
        _bossDropdown.onValueChanged.AddListener(BossDropdown_OnValueChanged);

        _leftArrowButton.onClick.AddListener(LeftArrowButton_OnClick);
        _rightArrowButton.onClick.AddListener(RightArrowButton_OnClick);

        //  Set up hit chance controller
        _hitChanceController = new HitChanceController();
        _hitChanceController.Setup();

        BossDropdown_OnValueChanged(0);
    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    //  Methods

    private void UpdateArrowUI()
    {
        //  Set left arrow button
        if (_subBossIndex == 0)
            _leftArrowButton.interactable = false;
        else
            _leftArrowButton.interactable = true;

        //  Set right arrow button
        if (_subBossIndex == _subBossList.Count - 1)
            _rightArrowButton.interactable = false;
        else
            _rightArrowButton.interactable = true;
    }

    //  UI events

    private void BossDropdown_OnValueChanged(int value)
    {
        string newBossName = _bossDropdown.options[value].text;
        base.CurrentBoss = ApplicationController.Instance.BossInfo.GetBoss(newBossName);
        _subBossIndex = 0;
        _subBossList = base.CurrentBoss.CombatDataList;

        _bossInfoView.DisplayBossCombatInfo(_currentSubBoss);
        UpdateArrowUI();
        _hitChanceController.CalculateHitChance(_currentSubBoss);
    }

    private void LeftArrowButton_OnClick()
    {
        --_subBossIndex;
        _bossInfoView.DisplayBossCombatInfo(_subBossList[_subBossIndex]);
        UpdateArrowUI();
        _hitChanceController.CalculateHitChance(_currentSubBoss);
    }

    private void RightArrowButton_OnClick()
    {
        ++_subBossIndex;
        _bossInfoView.DisplayBossCombatInfo(_subBossList[_subBossIndex]);
        UpdateArrowUI();
        _hitChanceController.CalculateHitChance(_currentSubBoss);
    }
}
