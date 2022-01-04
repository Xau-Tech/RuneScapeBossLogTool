using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatIntensityTooltip : MonoBehaviour, ITooltipHandler
{
    public string GetTooltipMessage()
    {
        return CombatIntensity.GetInfo();
    }
}
