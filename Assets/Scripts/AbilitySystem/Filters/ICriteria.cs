using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Generic Criteria interface to be extended for filtering data sets of object type T
/// </summary>
/// <typeparam name="T">The type of the object you want to filter</typeparam>
public interface ICriteria<T>
{
    bool MeetsCriteria(T obj);
}