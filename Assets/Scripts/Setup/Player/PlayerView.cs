using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerView : MonoBehaviour, IDisplayable<Player>
{
    [SerializeField] private SkillsView skillsView;
    [SerializeField] private InputField usernameInputField;
    [SerializeField] private InventoryView inventoryView;

    //  Display all player data to view
    public void Display(in Player value)
    {
        Debug.Log("PlayerView display");
        usernameInputField.text = value.username;
        skillsView.Display(in value.Skills);
    }
}
