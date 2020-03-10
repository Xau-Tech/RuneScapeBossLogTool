using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimpleOutput : MonoBehaviour
{
    public static SimpleOutput Instance;

    private Text thisText;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);

        thisText = GetComponent<Text>();
    }

    public void Print(in string _message)
    {
        thisText.text += (_message + "\n");
    }
}
