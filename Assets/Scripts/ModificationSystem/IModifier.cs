using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IModifier
{
    object Apply(object objToModify);
}