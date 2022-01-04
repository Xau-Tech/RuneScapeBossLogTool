using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// View for the skills area of the setup tab
/// </summary>
public class SkillsView : MonoBehaviour
{
    //  Properties & fields

    [SerializeField] private InputField _prayerInputField;
    [SerializeField] private InputField _smithingInputField;

    //  Methods

    public void Display(List<AbsSkill> skills)
    {
        foreach(AbsSkill skill in skills)
        {
            switch (skill.Name)
            {
                case Enums.SkillName.Prayer:
                    _prayerInputField.text = skill.Level.ToString();
                    break;
                case Enums.SkillName.Smithing:
                    _smithingInputField.text = skill.Level.ToString();
                    break;
                default:
                    break;
            }
        }
    }
}
