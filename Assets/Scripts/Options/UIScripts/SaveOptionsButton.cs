using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveOptionsButton : MonoBehaviour
{
    private void Awake()
    {
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(SaveOptions);
    }

    //  Event to get option values from UI and save to our data class
    private void SaveOptions()
    {
        EventManager.Instance.OptionsUpdated();
    }
}
