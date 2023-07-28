using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IModifier<T>
{
    T Apply(in T objToModify);
}