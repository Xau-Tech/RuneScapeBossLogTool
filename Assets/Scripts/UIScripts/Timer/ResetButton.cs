using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//  Button to reset the timer
public class ResetButton : MonoBehaviour
{
    private Button thisButton;
    [SerializeField] Button startStopButton;

    private void Awake()
    {
        thisButton = GetComponent<Button>();

        if (!thisButton)
            throw new System.Exception($"ResetButton.cs is not attached to a button gameobject!");
        else
            thisButton.onClick.AddListener(Reset);
    }

    private void OnEnable()
    {
        EventManager.Instance.onLogUpdated += Reset;
        EventManager.Instance.onBossDropdownValueChanged += Reset;
    }

    private void OnDisable()
    {
        EventManager.Instance.onLogUpdated -= Reset;
        EventManager.Instance.onBossDropdownValueChanged -= Reset;
    }

    private void Reset()
    {
        Timer.Reset();
        startStopButton.GetComponentInChildren<Text>().text = "Start Timer";
    }
}
