using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Relates a dropdown to a value (kills, loot, time) in the LogTrip popup in order to update values as the user changes them
/// </summary>
public class LogTripIFEndUpdate : MonoBehaviour
{
    //  Properties & fields
    [SerializeField] private LogTripPopup _logTripPopup;
    [SerializeField] private int _valueUpdated;
    private InputField _thisInputField;

    //  Monobehaviors

    private void Awake()
    {
        _thisInputField = this.GetComponent<InputField>();
    }

    private void OnEnable()
    {
        _thisInputField.onEndEdit.AddListener(EndEdit);
    }

    private void OnDisable()
    {
        _thisInputField.onEndEdit.RemoveAllListeners();
    }

    //  Methods

    private void EndEdit(string text)
    {
        int newValue;

        if (string.IsNullOrEmpty(text))
        {
            newValue = 0;
        }
        else
        {
            if (!int.TryParse(text, out newValue))
                newValue = 0;
        }

        _logTripPopup.UpdateAddedValue(_valueUpdated, newValue);
    }
}
