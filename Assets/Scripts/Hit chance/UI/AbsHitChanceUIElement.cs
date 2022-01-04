using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Abs class for any UI element that is part of the hit chance calculator
/// </summary>
public abstract class AbsHitChanceUIElement : MonoBehaviour
{
    abstract public void Setup(HitChanceUISetupData setupData);
}
