using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class IFSearch : MonoBehaviour
{
    //  Properties & fields
    private InputField thisInputField;
    [SerializeField] private Dropdown dropdownToSearch;
    private List<Dropdown.OptionData> optionData;
    private int dropdownHighlightedOption;
    private Selectable selectedOption;
    [SerializeField] private ColorBlock normalColorBlock;
    private ColorBlock selectedColorBlock;

    //  Monobehaviors

    private void Awake()
    {
        thisInputField = GetComponent<InputField>();
        optionData = dropdownToSearch.options;
        selectedColorBlock = normalColorBlock;
        selectedColorBlock.normalColor = selectedColorBlock.selectedColor;
    }

    private void Update()
    {
        //  This input field is selected
        if (EventSystem.current.currentSelectedGameObject == this.gameObject)
        {
            //  User pressed down
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                //  Clamp value to options.count maximum
                if (dropdownHighlightedOption < dropdownToSearch.options.Count - 1)
                {
                    //  Increment value and select the item
                    dropdownHighlightedOption++;
                    SelectItem();
                }
            }
            //  User pressed up
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                //  Clamp value to 0 minimum
                if (dropdownHighlightedOption > 0)
                {
                    //  Decrement value and select the item
                    dropdownHighlightedOption--;
                    SelectItem();
                }
            }
            //  User pressed enter/return
            else if (Input.GetKeyDown(KeyCode.Return))
            {
                dropdownToSearch.Hide();

                //  Check if there are no matching options
                if (dropdownToSearch.options.Count == 0)
                {
                    SetToDefaults();
                }

                //  Cache the item name so that we can find it again after resetting the options values
                string itemName = dropdownToSearch.options[dropdownHighlightedOption].text;

                CloseOnSelection(itemName);
            }
            //  User input some value(s)
            else if (Input.inputString != "" && Input.inputString != " ")
            {
                Search();
            }
        }
        //  Input field is active, but not selected
        else
        {
            //  Left mouse click
            if (Input.GetMouseButtonDown(0))
            {
                //  This handles if there are no options and the user clicks on the dropdown template
                if (EventSystem.current.currentSelectedGameObject == null)
                    SetToDefaults();

                //  Blocker was clicked on - this triggers if clicking anywhere off the Dropdown/InputField as well as the Dropdown arrow
                if (EventSystem.current.currentSelectedGameObject.name == "Blocker")
                    SetToDefaults();
            }
            //  Return/enter press
            else if (Input.GetKey(KeyCode.Return))
            {
                Debug.Log("not sel");
                //  Get item name, close search and select item
                string itemName = dropdownToSearch.options[dropdownToSearch.value].text;
                CloseOnSelection(itemName);
            }
        }
    }

    private void LateUpdate()
    {
        //  Always move the caret of the inputfield to the end of text
        //  Also ensures the entire block of text is not selected
        thisInputField.MoveTextEnd(false);
    }

    //  Methods

    //  This runs anytime text is changed in the InputField
    public void Search()
    {
        dropdownHighlightedOption = 0;
        CheckText();
    }

    //  Find the appropriate Item gameobject and change its color to show that it is "selected"
    private void SelectItem()
    {
        if (selectedOption != null)
            selectedOption.colors = normalColorBlock;

        //  Find all selectables
        foreach (Selectable s in Selectable.allSelectablesArray)
        {
            //  Find the correct Item gameobject
            if (s.gameObject.name.IndexOf($"Item {dropdownHighlightedOption}:") >= 0)
            {
                selectedOption = s;
                selectedOption.colors = selectedColorBlock;
                continue;
            }
        }

        SetScrollPosition();
    }

    //  Set the position of the dropdown's scrollbar
    private void SetScrollPosition()
    {
        //  Base scroll value based on current / total options
        float scrollValue = 1f - ((float)dropdownHighlightedOption / dropdownToSearch.options.Count);

        //  Set scroll to 0 (bottom) if we are on the last option
        if (dropdownHighlightedOption + 1 == dropdownToSearch.options.Count)
            scrollValue = 0f;
        else if (dropdownHighlightedOption == 0)
            scrollValue = 1f;
        //  Modify slightly based on number of options
        else
        {
            //  Determine current option's distance from center
            float halfNumOptions = dropdownToSearch.options.Count / 2f;
            float distanceFromCenter = Mathf.Abs(dropdownHighlightedOption - halfNumOptions);

            //  Adjust value up if below half (top of list)
            if (dropdownHighlightedOption < halfNumOptions)
            {
                scrollValue = scrollValue + (scrollValue * (distanceFromCenter / (dropdownToSearch.options.Count * 10f)));
                scrollValue = Mathf.Min(1f, scrollValue);
            }
            //  Adjust value down if above half (bottom of list)
            else
            {
                scrollValue = scrollValue - (scrollValue * (distanceFromCenter / dropdownToSearch.options.Count));
                scrollValue = Mathf.Max(0f, scrollValue);
            }
        }

        //  Try to get scrollbar and set its value
        if (TryGetScrollbar(out Selectable scrollbar))
            scrollbar.GetComponent<Scrollbar>().value = scrollValue;
    }

    //  Function to return the active scrollbar from dropdown
    private bool TryGetScrollbar(out Selectable scrollbar)
    {
        //  Find all selectables
        foreach (Selectable s in Selectable.allSelectablesArray)
        {
            //  Check that selectable is child of dropdown and is named Scrollbar
            if (s.transform.IsChildOf(dropdownToSearch.transform) && s.gameObject.name == "Scrollbar")
            {
                //  Make sure object has a scrollbar component and set out value if so, returning true
                if (s.GetComponent<Scrollbar>())
                {
                    scrollbar = s;
                    return true;
                }
            }
        }

        //  No scrollbar found - return null/false
        //  This should not be handled as an error due to instances where the list is short enough to not require a scrollbar
        scrollbar = null;
        return false;
    }

    

    //  Close the search functionality and set the dropdown to the passed value
    //  Requires exact match on itemName
    private void CloseOnSelection(string itemName)
    {
        //  Reset options to original data as well
        SetToDefaults();

        //  Select the proper value
        dropdownToSearch.value = dropdownToSearch.options.FindIndex(item => item.text.CompareTo(itemName) == 0);
    }

    //  Reset our ui and optiondata to original values
    private void SetToDefaults()
    {
        dropdownToSearch.Hide();
        thisInputField.text = "";
        dropdownToSearch.options = optionData;
        thisInputField.gameObject.SetActive(false);
    }

    //  Update the UI to show proper options based on search
    //  Note: Due to some internal coding reason w/ Unity, the coroutine MUST be used for this to work even if just set to WaitForSeconds(0f)
    //   shrug \_(O_O)_/
    private void UpdateDropdown()
    {
        dropdownToSearch.Hide();
        dropdownToSearch.options = optionData.FindAll(option => option.text.ToLower().IndexOf(thisInputField.text.ToLower()) >= 0);

        StartCoroutine(ShowOptions());
    }

    //  See above comment
    private IEnumerator ShowOptions()
    {
        yield return new WaitForSeconds(0f);

        dropdownToSearch.Show();
        SelectItem();
        //  Reselect inputfield for next input/frame
        thisInputField.Select();
    }

    //  Look at inputfield text and remove unwanted chars
    private void CheckText()
    {
        string searchFieldText = thisInputField.text;
        string newText = "";

        foreach (char ch in searchFieldText)
        {
            //  Allow any letter or digit as well as spaces and apostrophes
            if (char.IsLetterOrDigit(ch) || ch == ' ' || ch == '\'')
                newText += ch;
        }

        thisInputField.text = newText;
        UpdateDropdown();
    }

    /*  If pointer enters the dropdown item area (which selects part of the dropdown) change color of selectedOption back to normal
        so dropdown handles input as it normally would
    */
    public void DropdownItem_OnPointerEnter(BaseEventData baseEventData)
    {
        if (this.gameObject.activeSelf)
        {
            if (selectedOption)
                selectedOption.colors = normalColorBlock;
        }
    }

    //  User clicks on a dropdown's item selectable in the template
    public void DropdownItem_OnPointerClick(Selectable go)
    {
        //  Only run if inputfield is active (otherwise dropdown's default code will handle it fine)
        if (this.gameObject.activeSelf)
        {
            //  Set it to uninteractable so that it's OnClick event is blocked as we want to handle this interaction on our own
            go.interactable = false;

            //  Get just the itemName itself out of the object
            string[] objectName = go.gameObject.name.Split(':');
            string itemName = objectName[1].Remove(0, 1);

            //  Close and select our item
            CloseOnSelection(itemName);
        }
    }
}