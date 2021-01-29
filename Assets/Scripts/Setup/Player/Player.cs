using System.Collections.Generic;

//  Player class for the setup
public class Player
{
    public Player(string username)
    {
        this.Username = username;
        Inventory = new Inventory();
        Equipment = new Equipment();

        Skills.Add(prayerSkill);
        Skills.Add(smithingSkill);
    }

    public Player(string username, PlayerGlob playerSaveGlob)
    {
        this.Username = username;
        Inventory = new Inventory(playerSaveGlob.inventory);
        Equipment = new Equipment(playerSaveGlob.equipment);

        Skills.Add(prayerSkill);
        Skills.Add(smithingSkill);
    }

    public string Username { get; set; }

    //  Skill levels
    public sbyte PrayerLevel { get { return prayerSkill.Level; } set { prayerSkill.Level = value; } }
    public sbyte SmithingLevel {
        get { return smithingSkill.Level; }
        set
        {
            smithingSkill.Level = value;
            Equipment.DetermineCost();
        }
    }
    //  End skill levels

    public List<AbstractSkill> Skills { get; } = new List<AbstractSkill>();
    public Inventory Inventory { get; }
    public Equipment Equipment { get; }
    public int AugmentsCost { get { return Equipment.AugmentsCost; } }

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

    public override string ToString()
    {
        return $"Player [ Name: {Username}, Prayer: {prayerSkill.Level}, Smithing: {smithingSkill.Level} ]\nSkills [ {Skills} ]";
    }
}
