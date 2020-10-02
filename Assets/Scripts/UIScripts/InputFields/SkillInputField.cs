using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillInputField : MonoBehaviour
{
    private InputField thisInputField;
    [SerializeField] private SkillNames linkedSkill;

    private void Awake()
    {
        thisInputField = GetComponent<InputField>();
        if (!thisInputField)
            throw new System.Exception($"SkillInputField.cs is not attached to an input field gameobject!");
        else
            thisInputField.onEndEdit.AddListener(UpdateSkill);
    }

    private void UpdateSkill(string s)
    {
        sbyte level;
        if (!sbyte.TryParse(s, out level))
            return;

        CacheManager.SetupTab.Setup.Player.SetLevel(in linkedSkill, in level);
    }
}
