using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CancelButton : MonoBehaviour
{
    private void Awake()
    {
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(ReselectSavedOptions);
    }

    //  User may change options and then press cancel
    //  We want to make sure saved option values are re-selected
    private void ReselectSavedOptions()
    {
        EventManager.Instance.OptionUISetup();
    }
}
