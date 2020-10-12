using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//  InputField for Instance Cost of the Setup
public class InstanceCostInputField : MonoBehaviour
{
    private InputField thisInputField;

    private void Awake()
    {
        if (!(thisInputField = GetComponent<InputField>()))
            throw new System.Exception($"InstanceCostInputField.cs is not attached to an InputField gameobject!");
        else
            thisInputField.onEndEdit.AddListener(OnEndEdit);
    }

    //  OnEndEdit action - parse and set InstanceCost if proper input
    private void OnEndEdit(string value)
    {
        int cost = int.Parse(value);

        //  Ensure cost is at least 0
        if(cost < 0)
        {
            InputWarningWindow.Instance.OpenWindow($"Instance cost must be greater than or equal to 0!");
            thisInputField.text = CacheManager.SetupTab.Setup.InstanceCost.ToString();
            return;
        }

        //  Update value
        CacheManager.SetupTab.Setup.SetInstanceCost(in cost);
    }
}
