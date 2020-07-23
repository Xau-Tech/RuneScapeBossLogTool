using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//  Text to display the current value of the timer
public class DisplayText : MonoBehaviour
{
    private Text thisText;

    private void Awake()
    {
        thisText = GetComponent<Text>();

        if (!thisText)
            throw new System.Exception($"DisplayText.cs is not attached to a text gameobject!");
    }

    private void OnEnable()
    {
        EventManager.Instance.onTimerUpdated += UpdateText;
    }

    private void OnDisable()
    {
        EventManager.Instance.onTimerUpdated -= UpdateText;
    }

    private void Update()
    {
        if(Timer.IsRunning)
            Timer.UpdateTime();
    }

    private void UpdateText()
    {
        thisText.text = $"Timer: {Timer.time.ToString("hh\\h\\:mm\\m\\:ss\\s")}";
    }
}
