using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupInfo
{
    public static SetupInfo Instance { get; } = new SetupInfo();

    public long TotalCost { get; private set; }

    public void AddToTotalCost(in long value)
    {
        TotalCost += value;
    }
}
