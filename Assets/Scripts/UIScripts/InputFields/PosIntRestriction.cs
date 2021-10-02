using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PosIntRestriction : MonoBehaviour
{
    private InputField thisIF;

    private void Awake()
    {
        thisIF = this.GetComponent<InputField>();
        thisIF.onValidateInput += new InputField.OnValidateInput(ValidatePosInt);
        thisIF.onEndEdit.AddListener(EndEdit);
    }

    //  Ensure no negatives or leading zeros can be entered
    private char ValidatePosInt(string text, int charIndex, char addedChar)
    {
        if (char.IsDigit(addedChar))
        {
            if (charIndex == 0 && addedChar == '0')
                return '\0';
            else
                return addedChar;
        }
        else
        {
            return '\0';
        }
    }

    //  Reset to 1 if empty
    private void EndEdit(string value)
    {
        if (string.IsNullOrEmpty(value))
            thisIF.text = "1";
    }
}
