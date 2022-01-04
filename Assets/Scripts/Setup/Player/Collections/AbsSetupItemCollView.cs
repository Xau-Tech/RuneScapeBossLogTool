using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Abs base class for SetupItemCollection view classes to inherit from to mark their collection type
/// </summary>
public class AbsSetupItemCollView : MonoBehaviour
{
    //  Properties & fields

    public Enums.SetupCollections CollectionType { get; protected set; }
}
