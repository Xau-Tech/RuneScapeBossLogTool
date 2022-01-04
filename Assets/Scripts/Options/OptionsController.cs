using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

/// <summary>
/// Controller for application's option system
/// </summary>
public class OptionsController
{
    //  Properties & fields
    public OptionsController Instance
    {
        get
        {
            if (_instance == null)
                _instance = this;

            return _instance;
        }
    }

    private Options _model;
    private OptionsView _view;
    private static OptionsController _instance;

    //  Constructor
    public OptionsController(Options model, OptionsView view)
    {
        this._model = model;
        this._view = view;
    }

    ~OptionsController()
    {
        EventManager.Instance.onOptionsUpdated -= UpdateOptionsFromUI;
    }

    //  Custom methods
    public void OpenOptions()
    {
        _view.OpenPopup();
    }

    //  Make sure to call this in awake - toggle gameobjects are always null until first post-awake frame
    public async Task Setup()
    {
        EventManager.Instance.onOptionsUpdated += UpdateOptionsFromUI;
        await _view.Setup();
        _model.Setup();
    }

    //  Set data from UI
    private void UpdateOptionsFromUI()
    {
        _view.UpdateOptions(_model.GetOptionList());
        _model.SaveOptions();
    }

    //  Get the current value of an option
    public string GetOptionValue(Enums.OptionNames optionName)
    {
        return _model.GetOptionValue(optionName);
    }
}