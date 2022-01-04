using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Struct for holding log totals
/// </summary>
public struct LogDataStruct
{
    public uint Kills { get; set; }
    public uint Time { get; set; }
    public ulong Loot { get; set; }
}
