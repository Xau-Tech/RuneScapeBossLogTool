using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IFEndUpdateText : MonoBehaviour
{
    [SerializeField] private AddToLogWindow _window;
    [SerializeField] private int _valueUpdated;
    private InputField thisIf;

    private void Awake()
    {
        thisIf = gameObject.GetComponent<InputField>();
    }

    private void OnEnable()
    {
        thisIf.onEndEdit.AddListener(EndEdit);
    }

    private void OnDisable()
    {
        thisIf.onEndEdit.RemoveAllListeners();
    }

    public void EndEdit(string value)
    {
        int newValue;

        if (string.IsNullOrEmpty(value))
        {
            newValue = 0;
        }
        else
        {
            if (!int.TryParse(value, out newValue))
                newValue = 0;
        }

        _window.UpdateAddedValue(_valueUpdated, newValue);
    }
}
