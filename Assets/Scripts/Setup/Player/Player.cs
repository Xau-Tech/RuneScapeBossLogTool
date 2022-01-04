using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    //  Properties & fields

    public string Username { get; set; }
    public sbyte PrayerLevel { get { return _prayer.Level; } set { _prayer.Level = value; } }
    public sbyte SmithingLevel { get { return _smithing.Level; }
        set
        {
            _smithing.Level = value;
            Equipment.DetermineCost();
        }
    }
    public List<AbsSkill> Skills { get; } = new List<AbsSkill>();
    public Inventory Inventory { get; }
    public BeastOfBurden BeastOfBurden { get; }
    public Prefight Prefight { get; }
    public Equipment Equipment { get; }
    public int AugmentsCost { get { return Equipment.AugmentsCost; } }

    private PrayerSkill _prayer = new PrayerSkill();
    private SmithingSkill _smithing = new SmithingSkill();

    //  Constructor

    public Player(string username)
    {
        this.Username = username;
        Inventory = new Inventory();
        BeastOfBurden = new BeastOfBurden();
        Prefight = new Prefight();
        Equipment = new Equipment();

        Skills.Add(_prayer);
        Skills.Add(_smithing);
    }
    public Player(string username, PlayerGlob pg)
    {
        this.Username = username;
        Inventory = new Inventory(pg.inventory);
        Equipment = new Equipment(pg.equipment);
        Prefight = new Prefight(pg.prefight);
        BeastOfBurden = new BeastOfBurden(pg.beastOfBurden);

        Skills.Add(_prayer);
        Skills.Add(_smithing);
    }

    //  Methods

    public void SetLevel(Enums.SkillName skillName, sbyte level)
    {
        switch (skillName)
        {
            case Enums.SkillName.Prayer:
                PrayerLevel = level;
                break;
            case Enums.SkillName.Smithing:
                SmithingLevel = level;
                Equipment.DetermineCost();
                break;
            default:
                break;
        }
    }

    public sbyte GetLevel(Enums.SkillName skillName)
    {
        switch (skillName)
        {
            case Enums.SkillName.Prayer:
                return PrayerLevel;
            case Enums.SkillName.Smithing:
                return SmithingLevel;
            default:
                throw new System.Exception($"{skillName} skill not found!");
        }
    }

    public override string ToString()
    {
        return $"Player [ Name: {Username}, Prayer: {_prayer.Level}, Smithing {_smithing.Level} ]\nSkills [ {Skills} ]";
    }
}
