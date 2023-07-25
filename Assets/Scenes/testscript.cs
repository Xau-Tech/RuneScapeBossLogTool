using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class testscript : MonoBehaviour
{
    private void Awake()
    {
        CritCap cc = new();
        cc.Modify();
        Debug.Log("Relogging value from maxcritmodded property: " + cc.ModdedMaxCrit);
    }
}