using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  MVC
public class OptionController
{
    private static OptionController instance;

    //  Data
    private Options model;
    //  View
    private OptionUI view;

    public OptionController(Options model, OptionUI view)
    {
        this.model = model;
        this.view = view;
    }

    ~OptionController()
    {
        EventManager.Instance.onOptionsUpdated -= UpdateOptionsFromUI;
    }

    public OptionController Instance()
    {
        if (instance == null)
            instance = this;

        return instance;
    }

    //  Open the options window
    public void OpenOptionWindow()
    {
        OptionUI.OpenOptionsWindow();
    }

    //  Make sure to call this in awake
    //  Toggle GameObject is always null until first post-awake frame
    public void Setup()
    {
        EventManager.Instance.onOptionsUpdated += UpdateOptionsFromUI;

        view.Setup();
        model.Setup();
    }

    //  Set our current UI choices to option values
    private void UpdateOptionsFromUI()
    {
        view.UpdateOptions(model.GetOptionList());
        model.SaveOptions();
    }

    //  Wrapper for our data class method to get an option's value by option name
    public string GetOptionValue(in string optionName)
    {
        return model.GetOptionValue(in optionName);
    }
}
