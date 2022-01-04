using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Extension to determine wrapping on unsigned variables
/// </summary>
public static class UnsignedWrapping
{
    //  Addition

    //  uint
    public static bool WillWrap(this uint origValue, in uint addedValue)
    {
        if (origValue > uint.MaxValue - addedValue)
            return true;
        else
            return false;
    }

    //  ulong
    public static bool WillWrap(this ulong origValue, in ulong addedValue)
    {
        if (origValue > ulong.MaxValue - addedValue)
            return true;
        else
            return false;
    }

    //  ushort
    public static bool WillWrap(this ushort origValue, in ushort addedValue)
    {
        if (origValue > ushort.MaxValue - addedValue)
            return true;
        else
            return false;
    }
}
