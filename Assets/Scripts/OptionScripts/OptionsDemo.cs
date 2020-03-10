using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class OptionsDemo : MonoBehaviour
{
    private static Options options = new Options();

    // Start is called before the first frame update
    void Start()
    {
        Options.Setup();
    }
}
