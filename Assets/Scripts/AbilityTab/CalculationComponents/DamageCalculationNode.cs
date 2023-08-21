using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DamageCalculationNode
{
    public abstract DamageCalculationResults Calculate(DamageCalculationNode prevNode, SubAbility subAbililty);
}

public struct DamageCalculationResults
{
    public int Min;
    public int Max;
}