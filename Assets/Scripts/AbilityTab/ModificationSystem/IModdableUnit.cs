using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IModdableUnit<T>
{
    T ModifyObject();
}