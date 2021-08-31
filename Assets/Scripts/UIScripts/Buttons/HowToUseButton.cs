using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//  Code for the action at: Menu Bar -> Help -> How to Use
public class HowToUseButton : MonoBehaviour
{
    private Button _howToUseBtn = null;
    private string _filePath = "";

    private void Awake()
    {
        if (!(_howToUseBtn = GetComponent<Button>()))
            throw new NullReferenceException();

        _howToUseBtn.onClick.AddListener(HowToUseBtn_OnClick);
        _filePath = Application.streamingAssetsPath + "/HelpDoc.txt";
    }

    private void HowToUseBtn_OnClick()
    {
        Application.OpenURL(_filePath);
    }
}
