using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//  Button to increment killcount
public class AddKillButton : MonoBehaviour
{
    private Button thisButton;

    private void Awake()
    {
        thisButton = GetComponent<Button>();

        if (!thisButton)
            throw new System.Exception($"AddKillButton.cs is not attached to a button gameobject!");

        thisButton.onClick.AddListener(IncrementKillcount);
    }

    private void IncrementKillcount()
    {
        Killcount.Increment();
    }
}
