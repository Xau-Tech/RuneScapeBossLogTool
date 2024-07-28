using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

//  Dropdown used to display all available items for selected boss/encounter
public class ItemDropdown : MonoBehaviour
{
    //  Properties & fields

    private Dropdown thisDropdown;
    [SerializeField] private InputField searchField;
    [SerializeField] private InputField _itemAmountField;
    [SerializeField] private GameObject _mainCanvas;

    //  Monobehaviors

    private void Awake()
    {
        thisDropdown = GetComponent<Dropdown>();
    }

    private void Update()
    {
        Debug.Log(EventSystem.current.currentSelectedGameObject);

        //  Make sure something is selected so we don't get NullRefExc
        if (EventSystem.current.currentSelectedGameObject == null)
            return;

        //  Selected gameobject is the dropdown itself or a child of it
        if (EventSystem.current.currentSelectedGameObject == this.gameObject
            || EventSystem.current.currentSelectedGameObject.transform.IsChildOf(this.gameObject.transform))
        {
            //  Fix for issue with multiple blockers instantiating leading to user being locked on the dropdown and having to exit to reset
            //  If dropdown template is hidden (ie it isn't expanded) go through and manually delete any blocker objects from under the main canvas
            Transform instantiatedTemplate = this.gameObject.transform.Find("Dropdown List");
            if(instantiatedTemplate == null)
            {
                //Debug.Log("no instantiated template - deleting all blockers");
                List<GameObject> blockers = new List<GameObject>();

                for(int i = 0; i < _mainCanvas.transform.childCount; ++i)
                {
                    GameObject child = _mainCanvas.transform.GetChild(i).gameObject;

                    if (child.name == "Blocker")
                        blockers.Add(child);
                }

                foreach (var blocker in blockers)
                    Destroy(blocker);
            }

            if(CanDeselect() && Input.GetKeyDown(KeyCode.Tab))
            {
                thisDropdown.Hide();
                _itemAmountField.Select();
                return;
            }

            string input = Input.inputString;

            if (input == "") { }
            //  Is the user entering letters
            else if (char.IsLetter(input[0]) || Input.GetKeyDown(KeyCode.Backspace) || input == " " || input == "'")
            {
                //  Activate searchfield and add letter chars to its text
                searchField.gameObject.SetActive(true);

                if (char.IsLetter(input[0]))
                    searchField.text += input;
                else if (Input.GetKeyDown(KeyCode.Backspace))
                {
                    if (searchField.text.Length > 0)
                        searchField.text = searchField.text.Remove(searchField.text.Length - 1);
                }
                else
                {
                    if (searchField.text.Length > 0)
                        searchField.text += input;
                }

                //  Perform the search functionality with our initial value
                searchField.GetComponent<IFSearch>().Search();
            }
        }
    }

    //  Set to top value - used after updating a log
    private void SetToTopValue()
    {
        thisDropdown.value = 0;
    }

    //  Used to scroll
    //  Not entirely sure why, but something in my other code blocks built in scrolling
    //  functionality when hovering over the dropdown items (not the scrollbar though)
    public void DropdownItem_OnScroll()
    {
        Scrollbar scroll = GetComponentInChildren<Scrollbar>();

        if (!scroll)
            return;

        //  Set scroll value based on scrollDelta input and number of options as well as a constant value
        scroll.value += Input.mouseScrollDelta.y / thisDropdown.options.Count;
        //  Make sure to clamp from [0, 1]
        scroll.value = Mathf.Clamp(scroll.value, 0, 1f);
    }

    private bool CanDeselect()
    {
        if (searchField.gameObject.activeSelf == true)
            return false;
        else
            return true;
    }
}