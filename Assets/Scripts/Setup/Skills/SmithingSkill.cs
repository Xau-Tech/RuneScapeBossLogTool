﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmithingSkill : AbstractSkill
{
    public SmithingSkill()
    {
        Name = SkillNames.Smithing;
        Level = 1;
        base.WebQueryLineNumber = WebQueryLineNumber;
    }

    public new static sbyte WebQueryLineNumber { get; } = 14;
}
