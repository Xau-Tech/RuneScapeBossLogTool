﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public Player(string username)
    {
        this.username = username;

        skills.Add(prayerSkill);
        skills.Add(smithingSkill);
    }

    public string username { get; private set; }
    //  Skill levels
    public sbyte PrayerLevel { get { return prayerSkill.Level; } set { prayerSkill.Level = value; } }
    public sbyte SmithingLevel { get { return smithingSkill.Level; } set { smithingSkill.Level = value; } }
    public ref List<AbstractSkill> Skills { get { return ref skills; } }

    private List<AbstractSkill> skills = new List<AbstractSkill>();
    private PrayerSkill prayerSkill = new PrayerSkill();
    private SmithingSkill smithingSkill = new SmithingSkill();

    /*  Skill functions  */

    //  Set passed skill to passed level
    public void SetLevel(in SkillNames skillName, in sbyte level)
    {
        switch (skillName)
        {
            case SkillNames.Prayer:
                PrayerLevel = level;
                break;
            case SkillNames.Smithing:
                SmithingLevel = level;
                break;
            default:
                throw new System.Exception($"{skillName} skill not found!");
        }
    }
}