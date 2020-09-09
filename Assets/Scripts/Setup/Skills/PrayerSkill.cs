using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrayerSkill : AbstractSkill
{
    public PrayerSkill()
    {
        Name = SkillNames.Prayer;
        Level = 1;
        base.WebQueryLineNumber = WebQueryLineNumber;
    }

    public new static sbyte WebQueryLineNumber { get; } = 6;
}
