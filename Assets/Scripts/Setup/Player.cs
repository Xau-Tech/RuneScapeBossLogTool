using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public Player(string username)
    {
        this.username = username;
        PopulateSkills();
    }

    public string username { get; private set; }
    public int healthRestored { get; set; }
    public int prayerRestored { get; set; }

    private List<Skill> skills;
    private List<ItemSlot> inventory;

    //  Skill functions

    private void PopulateSkills()
    {
        skills = new List<Skill>();

        foreach (var skillData in SkillLoaderArray.Skills)
        {
            skills.Add(new Skill(skillData.name, 1));
        }
    }

    public Skill GetSkill(SkillLoaderArray.SkillNames skillName)
    {
        return skills.Find(skill => skill.name.CompareTo(skillName) == 0);
    }

    public void SetSkill(SkillLoaderArray.SkillNames skillName, sbyte level)
    {
        GetSkill(skillName).level = level;
    }

    public sbyte GetSkillLevel(SkillLoaderArray.SkillNames skillName)
    {
        return GetSkill(skillName).level;
    }
    
    //  Inventory functions
}
