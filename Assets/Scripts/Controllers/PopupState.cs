using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PopupState
{
    public enum states { None, Warning, Confirm }
    public static states CurrentState;
}
