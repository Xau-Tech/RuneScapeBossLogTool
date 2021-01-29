using UnityEngine;

public class CombatIntensityText : MonoBehaviour, ITooltipHandler
{
    public string GetTooltipMessage()
    {
        return CombatIntensity.GetInfo();
    }
}
