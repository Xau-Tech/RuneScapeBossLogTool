using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractItemEffect : ScriptableObject
{
    public abstract void Apply(in Setup setup);
}
