using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// View for ApplicationMVC
/// </summary>
public class AppView : MonoBehaviour
{
    //  Properties & fields
    public OptionsView OptionsView { get { return _optionsView; } }
    public Timer Timer { get { return _timerDisplay.Timer; } }
    public float CanvasScale { get { return _mainCanvas.transform.localScale.x; } }

    [SerializeField] private Canvas _mainCanvas;
    [SerializeField] private Text _saveText;
    [SerializeField] private GameObject _dropsTab;
    [SerializeField] private GameObject _logsTab;
    [SerializeField] private GameObject _setupTab;
    [SerializeField] private GameObject _bossInfoTab;
    [SerializeField] private OptionsView _optionsView;
    [SerializeField] private GameObject _inputRestrictPanel;
    [SerializeField] private Sprite _loadSprite;
    [SerializeField] private TimerDisplay _timerDisplay;

    //  Monobehavior methods

    //  Custom methods

    public List<AbstractTab> GetMainTabs()
    {
        List<AbstractTab> tabs = new List<AbstractTab>();

        tabs.Add(_dropsTab.GetComponent<AbstractTab>());
        tabs.Add(_logsTab.GetComponent<AbstractTab>());
        tabs.Add(_setupTab.GetComponent<AbstractTab>());
        tabs.Add(_bossInfoTab.GetComponent<AbstractTab>());

        return tabs;
    }

    public void UpdateSaveText()
    {
        _saveText.text = $"Saved at: {DateTime.Now.ToString("T")}";
    }

    public void OpenInputBlocker(string text)
    {
        Image img = _inputRestrictPanel.GetComponent<Image>();
        Text t = _inputRestrictPanel.GetComponentInChildren<Text>();
        t.text = text;

        //  Application is loading resources when opening
        if(AppState.ProgramState == Enums.ProgramStates.Loading)
        {
            img.sprite = _loadSprite;
            img.color = Color.white;
        }
        else if(AppState.ProgramState == Enums.ProgramStates.Running)
        {
            img.sprite = null;
            Color clr = Color.white;
            clr.a = (100f / 255f);
            img.color = clr;
        }

        _inputRestrictPanel.gameObject.SetActive(true);
    }

    public void CloseInputBlocker()
    {
        _inputRestrictPanel.gameObject.SetActive(false);
    }

    public void SetTimerText()
    {
        _timerDisplay.SetText();
    }
}
