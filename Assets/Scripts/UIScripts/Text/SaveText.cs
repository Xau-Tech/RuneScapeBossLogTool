using UnityEngine;
using UnityEngine.UI;
using System;

//  Text that shows when the last save was committed
public class SaveText : MonoBehaviour
{
    private Text thisText;

    private void Awake()
    {
        thisText = GetComponent<Text>();

        if (!thisText)
            throw new System.Exception($"SaveText.cs is not attached to a text gameobject!");
    }

    private void OnEnable()
    {
        EventManager.Instance.onLogsSaved += SetText;
    }

    private void OnDisable()
    {
        EventManager.Instance.onLogsSaved -= SetText;
    }

    private void SetText()
    {
        thisText.text = $"Saved at: {DateTime.Now.ToString("T")}";
    }
}
