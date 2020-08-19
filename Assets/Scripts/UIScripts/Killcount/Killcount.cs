using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  Killcount data class
//  User can track how many kills they have in their current trip
public static class Killcount
{
    public static ushort killcount { get; private set; }

    public static void UpdateKillcount(short value)
    {
        if (value < 0 && killcount == 0)
            return;

        killcount += (ushort)value;
        EventManager.Instance.KillcountUpdated();
    }

    public static void Reset()
    {
        killcount = 0;
        EventManager.Instance.KillcountUpdated();
    }
}
