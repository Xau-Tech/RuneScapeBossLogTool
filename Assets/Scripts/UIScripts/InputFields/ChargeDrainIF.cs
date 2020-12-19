using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//  InputField for the charge drain rate in the Setup tab
public class ChargeDrainIF : MonoBehaviour
{
    private InputField thisInputField;

    private void Awake()
    {
        if (!(thisInputField = GetComponent<InputField>()))
            Debug.Log($"ChargeDrainIF is not attached to an InputField gameobject!");
        else
            thisInputField.onEndEdit.AddListener(OnEndEdit);
    }

    private void OnEndEdit(string value)
    {
        //  Make sure can be parsed to float - field is set to decimal values but just an additional safety check
        float rate;
        if (!float.TryParse(value, out rate))
        {
            Debug.Log($"{value} could not be converted to float for charge drain rate!");
            return;
        }

        //  Check for negative value
        if(rate < 0)
        {
            //  Notify user of field requirement
            InputWarningWindow.Instance.OpenWindow($"Charge drain rate must be a positive value!");
            //  Reset text to current value
            thisInputField.text = CacheManager.SetupTab.Setup.ChargeDrainRate.ToString();
            return;
        }

        //  Update value if it has been changed
        if(rate != CacheManager.SetupTab.Setup.ChargeDrainRate)
            CacheManager.SetupTab.Setup.SetChargeDrainRate(in rate);
    }
}