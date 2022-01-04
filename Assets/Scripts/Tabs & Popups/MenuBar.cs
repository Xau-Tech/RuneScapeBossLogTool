using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Handles all behavior & UI for the menu bar
/// </summary>
public class MenuBar : MonoBehaviour
{
    //  Properties & fields
    [SerializeField] private Button _saveButton;
    [SerializeField] private Button _optionsButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private Button _howToUseButton;

    //  Monobehaviors

    private void Awake()
    {
        _saveButton.onClick.AddListener(SaveButton_OnClick);
        _optionsButton.onClick.AddListener(OptionsButton_OnClick);
        _exitButton.onClick.AddListener(ExitButton_OnClick);
        _howToUseButton.onClick.AddListener(HowToUseButton_OnClick);
    }

    private void SaveButton_OnClick()
    {
        ApplicationController.Instance.Save();
    }

    private void OptionsButton_OnClick()
    {
        ApplicationController.OptionController.OpenOptions();
    }

    private void ExitButton_OnClick()
    {
        Application.Quit();
    }

    private void HowToUseButton_OnClick()
    {
        Application.OpenURL(Application.streamingAssetsPath + "/HelpDoc.txt");
    }
}
