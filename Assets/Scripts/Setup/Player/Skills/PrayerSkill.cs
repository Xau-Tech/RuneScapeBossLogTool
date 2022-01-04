using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Prayer skill
/// </summary>
public class PrayerSkill : AbsSkill
{
    //  Properties & fields

    public new static sbyte WebQueryLineNumber { get; } = 6;

    //  Constructor

    public PrayerSkill()
    {
        Name = Enums.SkillName.Prayer;
        Level = 1;
        base.WebQueryLineNumber = WebQueryLineNumber;
    }
}
