using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DropdownSearch : MonoBehaviour
{
    [SerializeField] private Dropdown m_DropdownToSearch;
    private InputField m_SearchField;
    private List<Dropdown.OptionData> m_DropdownOptions;

    private void Awake()
    {
        m_SearchField = GetComponent<InputField>();
        m_SearchField.onValueChanged.AddListener(SearchDropdown);
        m_DropdownOptions = m_DropdownToSearch.options;
    }

    private void SearchDropdown(string _search)
    {
        m_DropdownToSearch.Hide();
        
        m_DropdownToSearch.options = m_DropdownOptions.FindAll(options => options.text.ToLower().IndexOf
            (_search.ToLower()) >= 0);

        StartCoroutine(WaitForSomeStupidReason());
    }

    //  For hide and show to refresh and work properly, you need a delay because....reasons....
    private IEnumerator WaitForSomeStupidReason()
    {
        yield return new WaitForSeconds(0f);

        m_DropdownToSearch.Show();
        m_SearchField.Select();
    }

    private void LateUpdate()
    {
        m_SearchField.MoveTextEnd(true);
    }
}
