using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//  CombatIntensity dropdown in Setup tab
public class CombatIntensityDropdown : MonoBehaviour
{
    private Dropdown thisDropdown;

    private void Awake()
    {
        if (!(thisDropdown = GetComponent<Dropdown>()))
            Debug.Log($"CombatIntensityDropdown.cs is not attached to a dropdown gameobject!");
        else
        {
            thisDropdown.options.Clear();
            thisDropdown.AddOptions(CombatIntensity.CombatIntensityNames());
            thisDropdown.onValueChanged.AddListener(OnValueChanged);
        }
    }

    private void OnValueChanged(int value)
    {
        CacheManager.SetupTab.Setup.SetCombatIntensity((CombatIntensity.CombatIntensityLevels)value);
    }
}
