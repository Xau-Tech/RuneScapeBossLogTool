using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  Player class for the setup
public class Player
{
    public Player(string username)
    {
        this.Username = username;
        Inventory = new Inventory();
        Equipment = new Equipment();

        skills.Add(prayerSkill);
        skills.Add(smithingSkill);
    }

    public string Username { get; private set; }
    //  Skill levels
    public sbyte PrayerLevel { get { return prayerSkill.Level; } set { prayerSkill.Level = value; } }
    public sbyte SmithingLevel { get { return smithingSkill.Level; } set { smithingSkill.Level = value; } }
    //  End skill levels
    public ref List<AbstractSkill> Skills { get { return ref skills; } }
    public Inventory Inventory { get; }
    public Equipment Equipment { get; }
    //  Combat intensity data

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
                Equipment.DetermineCost();
                EventManager.Instance.SmithingUpdated();
                break;
            default:
                throw new System.Exception($"{skillName} skill not found!");
        }
    }

    public sbyte GetLevel(in SkillNames skillName)
    {
        switch (skillName)
        {
            case SkillNames.Prayer:
                return PrayerLevel;
            case SkillNames.Smithing:
                return SmithingLevel;
            default:
                throw new System.Exception($"{skillName} skill not found!");
        }
    }
}
