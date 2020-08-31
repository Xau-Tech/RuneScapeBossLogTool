using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

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
    public async Task Setup()
    {
        EventManager.Instance.onOptionsUpdated += UpdateOptionsFromUI;

        await view.Setup();
        model.Setup();
    }

    //  Set our current UI choices to option values
    private void UpdateOptionsFromUI()
    {
        view.UpdateOptions(model.GetOptionList());
        model.SaveOptions();
    }

    public GenericOption GetOption(in OptionData.OptionNames optionName)
    {
        return model.GetOption(optionName);
    }

    //  Wrapper for our data class method to get an option's value by option name
    public string GetOptionValue(in string optionName)
    {
        return model.GetOptionValue(in optionName);
    }
}
