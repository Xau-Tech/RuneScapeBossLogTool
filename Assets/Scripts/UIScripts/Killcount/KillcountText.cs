using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//  UI view of the killcount data
public class KillcountText : MonoBehaviour
{
    private Text thisText;

    private void Awake()
    {
        thisText = GetComponent<Text>();

        if (!thisText)
            throw new System.Exception($"KillcountText.cs is not attached to a text gameobject!");
    }

    private void OnEnable()
    {
        EventManager.Instance.onKillcountUpdated += UpdateText;
    }

    private void OnDisable()
    {
        EventManager.Instance.onKillcountUpdated -= UpdateText;
    }

    private void UpdateText()
    {
        thisText.text = $"Killcount: {Killcount.killcount}";
    }
}
