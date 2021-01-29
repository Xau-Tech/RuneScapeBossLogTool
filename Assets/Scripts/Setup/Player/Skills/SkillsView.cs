using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//  Display all the skills of a player to the UI
public class SkillsView : MonoBehaviour, IDisplayable<List<AbstractSkill>>
{
    [SerializeField] private InputField prayerInputField;
    [SerializeField] private InputField smithingInputField;

    //  Display all skill data to view
    public void Display(in List<AbstractSkill> skills)
    {
        for(int i = 0; i < skills.Count; ++i)
        {
            switch (skills[i].Name)
            {
                case SkillNames.Prayer:
                    prayerInputField.text = skills[i].Level.ToString();
                    break;
                case SkillNames.Smithing:
                    smithingInputField.text = skills[i].Level.ToString();
                    break;
                default:
                    break;
            }
        }
    }
}
