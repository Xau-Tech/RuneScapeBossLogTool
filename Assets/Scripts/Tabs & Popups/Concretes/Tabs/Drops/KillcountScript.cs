using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Handle behavior & UI for killcount in drops tab
/// </summary>
public class KillcountScript : MonoBehaviour
{
    //  Properties & fields
    public ushort Killcount { get; private set; }

    [SerializeField] private Text _kcText;
    [SerializeField] private Button _addButton;
    [SerializeField] private Button _subtractButton;
    [SerializeField] private Button _resetButton;

    //  Monobehaviors

    private void Awake()
    {
        _addButton.onClick.AddListener(IncrementKillcount);
        _subtractButton.onClick.AddListener(DecrementKillcount);
        _resetButton.onClick.AddListener(ResetKillcount);
    }

    //  Methods

    private void IncrementKillcount()
    {
        ++Killcount;
        UpdateText();
    }

    private void DecrementKillcount()
    {
        if (Killcount > 0)
        {
            --Killcount;
            UpdateText();
        }
    }

    public void ResetKillcount()
    {
        Killcount = 0;
        UpdateText();
    }

    private void UpdateText()
    {
        _kcText.text = $"Killcount: {Killcount}";
    }
}
