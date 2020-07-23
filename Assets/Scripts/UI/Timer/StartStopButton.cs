using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//  Button to start/stop the timer depending on its current state
public class StartStopButton : MonoBehaviour
{
    private Button thisButton;
    private Text buttonText;

    private void Awake()
    {
        thisButton = GetComponent<Button>();

        if (!thisButton)
            throw new System.Exception($"StartStopButton.cs not attached to a button gameobject!");
        else
        {
            thisButton.onClick.AddListener(Execute);
            buttonText = GetComponentInChildren<Text>();
        }
    }

    private void Execute()
    {
        if (Timer.IsRunning)
        {
            Timer.Stop();
            buttonText.text = "Start Timer";
        }
        else
        {
            Timer.Start();
            buttonText.text = "Stop Timer";
        }   
    }
}
