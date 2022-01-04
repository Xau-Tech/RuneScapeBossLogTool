using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Abstract base class for any skill in the game
/// </summary>
public abstract class AbsSkill
{
    //  Properties & fields

    public Enums.SkillName Name { get; protected set; }
    public sbyte Level { get; set; }
    public sbyte WebQueryLineNumber { get; protected set; }
}
