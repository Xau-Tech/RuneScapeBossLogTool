using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class testscript : MonoBehaviour
{
    private Text thisText;

    private void Awake()
    {
        thisText = GetComponent<Text>();

    }
}