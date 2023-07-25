using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModdableUnit : IModdableUnit
{
    private readonly object m_ObjToModify;
    private readonly List<IModifier> m_ModifierList;

    public ModdableUnit(object objToModify, List<IModifier> modifierList)
    {
        m_ObjToModify = objToModify;
        m_ModifierList = modifierList;
    }

    public virtual object ModifyObject()
    {
        object copyToReturn = m_ObjToModify;

        foreach(IModifier modifier in m_ModifierList)
        {
            copyToReturn = modifier.Apply(copyToReturn);
        }

        return copyToReturn;
    }
}