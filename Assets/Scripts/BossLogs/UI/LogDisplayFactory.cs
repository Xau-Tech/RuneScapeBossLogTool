using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Loads and returns new UI objects for displaying BossLogLists & BossLogs
/// </summary>
public static class LogDisplayFactory
{
    //  Properties & fields

    private static GameObject _bossTotalsWidget;
    private static GameObject _logTotalsWidget;

    //  Methods

    static LogDisplayFactory()
    {
        Load();
    }

    private static void Load()
    {
        _bossTotalsWidget = Resources.Load("BossLogListDisplayWidget") as GameObject;
        _logTotalsWidget = Resources.Load("BossLogDisplayWidget") as GameObject;
    }

    /// <summary>
    /// Create a log UI object
    /// </summary>
    /// <param name="displayType">Either a display for an entire boss, or a specific log from that boss</param>
    /// <param name="position">The position the returned object will be created at</param>
    /// <returns></returns>
    public static GameObject Instantiate(Enums.LogDisplays displayType, GameObject gameObject)
    {
        if (displayType == Enums.LogDisplays.BossTotals)
            return GameObject.Instantiate(_bossTotalsWidget, gameObject.transform.position, Quaternion.identity, gameObject.transform.parent);
        else if (displayType == Enums.LogDisplays.LogTotals)
            return GameObject.Instantiate(_logTotalsWidget, gameObject.transform.position, Quaternion.identity, gameObject.transform.parent);
        else
            return null;
    }
}
