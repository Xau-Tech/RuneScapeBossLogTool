using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WidgetFactory
{
    private static GameObject bossTotalsWidget;
    private static GameObject logTotalsWidget;
    private static GameObject timerWidget;

    static WidgetFactory()
    {
        Load();
    }

    private static void Load()
    {
        bossTotalsWidget = Resources.Load("BossLogListDisplayWidget") as GameObject;
        logTotalsWidget = Resources.Load("BossLogDisplayWidget") as GameObject;
        timerWidget = Resources.Load("TimerDisplay") as GameObject;
    }

    public static GameObject InstantiateWidget(WidgetTypes widgetType, GameObject gameObject)
    {
        if (widgetType == WidgetTypes.BossTotals)
            return GameObject.Instantiate(bossTotalsWidget, gameObject.transform.position, Quaternion.identity, gameObject.transform.parent);
        else if (widgetType == WidgetTypes.LogTotals)
            return GameObject.Instantiate(logTotalsWidget, gameObject.transform.position, Quaternion.identity, gameObject.transform.parent);
        else if (widgetType == WidgetTypes.Timer)
            return GameObject.Instantiate(timerWidget, gameObject.transform.position, Quaternion.identity, gameObject.transform.parent);
        else
            return null;
    }
}

public enum WidgetTypes { BossTotals, LogTotals, Timer }
