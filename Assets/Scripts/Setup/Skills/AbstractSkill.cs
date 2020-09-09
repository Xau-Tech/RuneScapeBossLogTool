using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractSkill
{
    public SkillNames Name { get; protected set; }
    public sbyte Level { get; set; }
    public sbyte WebQueryLineNumber { get; protected set; }
}

public enum SkillNames { Prayer, Smithing };
