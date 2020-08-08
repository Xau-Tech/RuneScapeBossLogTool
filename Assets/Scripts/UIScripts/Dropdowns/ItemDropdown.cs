using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//  Dropdown used to display all available items for selected boss/encounter
public class ItemDropdown : MonoBehaviour, IUninterruptable
{
    private Dropdown thisDropdown;
    [SerializeField] private InputField searchField;

    private void Awake()
    {
        thisDropdown = GetComponent<Dropdown>();
        if (!thisDropdown)
            throw new System.Exception($"An ItemDropdown.cs script is attached to a dropdown object!");
    }

    private void OnEnable()
    {
        EventManager.Instance.onItemsLoaded += FillDropdown;
        EventManager.Instance.onLogUpdated += SetToTopValue;
    }

    private void OnDisable()
    {
        EventManager.Instance.onItemsLoaded -= FillDropdown;
        EventManager.Instance.onLogUpdated -= SetToTopValue;
    }

    private void Update()
    {
        //  Make sure something is selected so we don't get NullRefExc
        if (EventSystem.current.currentSelectedGameObject == null)
            return;

        //  Selected gameobject is the dropdown itself or a child of it
        if (EventSystem.current.currentSelectedGameObject == this.gameObject
            || EventSystem.current.currentSelectedGameObject.transform.IsChildOf(this.gameObject.transform))
        {
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

    //  Clear and fill dropdown with values from itemList
    private void FillDropdown()
    {
        thisDropdown.ClearOptions();
        thisDropdown.AddOptions(DataController.Instance.itemList.GetItemNames());

        //  Check if UI is loaded
        if(CacheManager.DropsTab.IsUILoaded(CacheManager.DropsTab.Elements.ItemDropdown, true))
        {
            DataState.CurrentState = DataState.states.None;
        }
    }

    //  Used to scroll
    //  Not entirely sure why, but something in my other code blocks built in scrolling
    //  functionality when hovering over the dropdown items (not the scrollbar though)
    public void DropdownItem_OnScroll()
    {
        Scrollbar scroll = GetComponentInChildren<Scrollbar>();
        if (!scroll)
            throw new System.Exception($"Scrollbar could not be found on ItemDropdown gameobject!\nItemDropdown.cs");

        //  Set scroll value based on scrollDelta input and number of options as well as a constant value
        scroll.value += Input.mouseScrollDelta.y / thisDropdown.options.Count;
        //  Make sure to clamp from [0, 1]
        scroll.value = Mathf.Clamp(scroll.value, 0, 1f);
    }

    public bool CanDeselect()
    {
        if (searchField.gameObject.activeSelf == true)
            return false;
        else
            return true;
    }
}
