using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill
{
    public Skill(SkillLoaderArray.SkillNames name, sbyte level)
    {
        this.name = name;
        this.level = level;
    }

    public SkillLoaderArray.SkillNames name { get; private set; }
    public sbyte level { get; set; }
}

//  Data to load and set up skills
public static class SkillLoaderArray
{
    //  Skill names - make sure to change the below enum AND list when adding/removing skills in this code
    public enum SkillNames { Attack, Ranged, Prayer, Magic, Smithing, Summoning };

    private static readonly List<SkillLoaderStruct> skills = new List<SkillLoaderStruct>()
    {
        new SkillLoaderStruct(SkillNames.Attack, 1),
        new SkillLoaderStruct(SkillNames.Ranged, 5),
        new SkillLoaderStruct(SkillNames.Prayer, 6),
        new SkillLoaderStruct(SkillNames.Magic, 7),
        new SkillLoaderStruct(SkillNames.Smithing, 14),
        new SkillLoaderStruct(SkillNames.Summoning, 24)
    };

    public static List<SkillLoaderStruct> Skills { get { return skills; } }

    //  Struct for each skill to be used for loading/skill instance creation
    public struct SkillLoaderStruct
    {
        public SkillLoaderStruct(SkillNames name, sbyte lineNumber)
        {
            this.name = name;
            this.lineNumber = lineNumber;
        }

        public SkillNames name { get; private set; }
        //  The line number corresponding to the skill within the hiscores query response
        public sbyte lineNumber { get; private set; }
    }
}
