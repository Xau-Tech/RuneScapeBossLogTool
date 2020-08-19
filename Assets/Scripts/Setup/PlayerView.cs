using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerView : MonoBehaviour, IDisplayable<Player>
{
    [SerializeField] private InputField usernameInputField;
    [SerializeField] private InputField attackInputField;
    [SerializeField] private InputField rangedInputField;
    [SerializeField] private InputField magicInputField;
    [SerializeField] private InputField prayerInputField;
    [SerializeField] private InputField summoningInputField;
    [SerializeField] private InputField smithingInputField;

    public void Display(in Player value)
    {
        Debug.Log("PlayerView display");
        usernameInputField.text = value.username;
        attackInputField.text = value.GetSkill(SkillLoaderArray.SkillNames.Attack).level.ToString();
        rangedInputField.text = value.GetSkill(SkillLoaderArray.SkillNames.Ranged).level.ToString();
        magicInputField.text = value.GetSkill(SkillLoaderArray.SkillNames.Magic).level.ToString();
        prayerInputField.text = value.GetSkill(SkillLoaderArray.SkillNames.Prayer).level.ToString();
        summoningInputField.text = value.GetSkill(SkillLoaderArray.SkillNames.Summoning).level.ToString();
        smithingInputField.text = value.GetSkill(SkillLoaderArray.SkillNames.Smithing).level.ToString();
    }
}
