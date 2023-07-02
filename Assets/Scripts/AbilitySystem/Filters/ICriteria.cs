using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICriteria<T>
{
    List<T> FilterByCriteria(IEnumerable<T> listToFilter);
}