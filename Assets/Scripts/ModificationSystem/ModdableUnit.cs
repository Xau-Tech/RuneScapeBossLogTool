using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModdableUnit<T> : IModdableUnit<T>
{
    private readonly T m_ObjToModify;
    private readonly List<IModifier<T>> m_ModifierList;

    public ModdableUnit(T objToModify, List<IModifier<T>> modifierList)
    {
        m_ObjToModify = objToModify;
        m_ModifierList = modifierList;
    }

    public virtual T ModifyObject()
    {
        T copyToReturn = m_ObjToModify;

        foreach(IModifier<T> modifier in m_ModifierList)
        {
            copyToReturn = modifier.Apply(copyToReturn);
        }

        return copyToReturn;
    }
}