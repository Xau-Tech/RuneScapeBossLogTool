using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Smithing skill
/// </summary>
public class SmithingSkill : AbsSkill
{
    //  Properties & fields

    public new static sbyte WebQueryLineNumber { get; } = 14;

    //  Constructor

    public SmithingSkill()
    {
        Name = Enums.SkillName.Smithing;
        Level = 1;
        base.WebQueryLineNumber = WebQueryLineNumber;
    }
}
