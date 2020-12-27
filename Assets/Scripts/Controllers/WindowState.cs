using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  State for new windows set active or opened
public static class WindowState
{
    public enum states { None, AddToLog, AddLog, DeleteLog, AddSetup, AddSetupItemQuantity }
    public static states currentState;
}
