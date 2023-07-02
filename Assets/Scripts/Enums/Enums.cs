using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enums : MonoBehaviour
{
    //  Monster type used for susceptibilities
    public enum MonsterType { Other, Dagannoth, Kalphite, Dragon, Undead };
    //  Combat class types
    public enum CombatClasses { None, Melee, Magic, Ranged };
    //  Attack styles (each of these is a subcategory of a combat class)
    public enum AttackStyles { None, Crush, Slash, Stab, Arrows, Bolts, Thrown, Air, Water, Earth, Fire };
    //  Types of ways to display options
    public enum OptionTypes { Dropdown, Toggle };
    //  Option Names
    public enum OptionNames { Resolution, BossSync, RSVersion };
    //  Application states
    public enum ProgramStates { Loading, Exiting, Running };
    //  Tab states
    public enum TabStates { None, Drops, Logs, Setup, BossInfo, Abilities };
    //  Data states
    public enum DataStates { None, Loading, Saving };
    //  Popup states
    public enum PopupStates { None, Options, Notification, Confirm, LogTrip, AddLog, RenameLog, AddSetup, AssignSetup, RenameSetup };
    //  Combat intensity levels
    public enum CombatIntensityLevels { Low = 0, Average, High, Maximum };
    //  Types of log displays
    public enum LogDisplays { BossTotals, LogTotals };
    //  Types of Setup Items
    public enum SetupItemCategory { All, General, Food, Potion, Armour, Head, Pocket, Cape, Neck, Ammunition, Body, Legs, Gloves, Boots, Ring, Shield, Weapon, Mainhand, TwoHand, Offhand, None, Summoning, Familiars, Scrolls };
    //  Item slot types
    public enum ItemSlotCategory { Inventory, Head, Pocket, Cape, Necklace, Ammunition, Mainhand, Body, Offhand, Legs, Gloves, Boots, Ring, Familiar, Scroll };
    //  Collections that Setup Items can belong to
    public enum SetupCollections { Inventory, Equipment, Prefight, BoB };
    //  Skills included
    public enum SkillName { Prayer, Smithing };
    //  Effect modifier types
    public enum EffectTypes { Level, Accuracy, HitChance, Affinity };
    //  Modifier list types
    public enum ModTypes { Potion, Prayer, Aura, Nihil, Scrimshaw, Reaper, Quake, StatWarh, Gstaff, BandosBook };
}
