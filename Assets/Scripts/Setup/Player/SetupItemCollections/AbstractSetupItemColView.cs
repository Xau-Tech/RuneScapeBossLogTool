using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  Abstract class for SetupItemCollection view clases to inherit from marking their collection type
public abstract class AbstractSetupItemColView : MonoBehaviour
{
    public SetupCollections CollectionType { get; protected set; }
}

public enum SetupCollections { Inventory, Equipment, Prefight, BoB };