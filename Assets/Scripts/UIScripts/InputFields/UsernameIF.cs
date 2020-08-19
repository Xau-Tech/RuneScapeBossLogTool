using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//  Inputfield where user can enter their username to auto-fill stats
public class UsernameIF : MonoBehaviour
{
    private InputField thisIF;

    private void Awake()
    {
        if (!(thisIF = GetComponent<InputField>()))
            throw new System.Exception($"UsernameIF is not attached to an inputfield gameobject!");
        else
            thisIF.GetComponent<InputFieldOnEndEnter>().endEditAction = NewUsernameEntered;
    }

    private void NewUsernameEntered()
    {
        EventManager.Instance.NewUsernameEntered(thisIF.text);
        PlayerPrefs.SetString(PlayerPrefKeys.GetKeyName(PlayerPrefKeys.KeyNamesEnum.Username), thisIF.text);
        PlayerPrefs.Save();
    }
}
