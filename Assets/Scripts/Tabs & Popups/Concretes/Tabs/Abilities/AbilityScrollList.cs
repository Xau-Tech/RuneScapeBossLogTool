using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityScrollList : MonoBehaviour
{
    [SerializeField] private GameObject m_AbilityButtonTemplate;
    private List<GameObject> m_AbilityButtonList = new();

    //  Test function for putting some data on screen for now will flesh out later
    public void Display(IEnumerable<AbilityDamageResults> abilDamageResultsColl)
    {
        if(m_AbilityButtonList.Count > 0)
        {
            foreach (GameObject button in m_AbilityButtonList)
            {
                Destroy(button);
            }

            m_AbilityButtonList.Clear();
        }

        foreach(AbilityDamageResults adr in abilDamageResultsColl)
        {
            GameObject button = Instantiate(m_AbilityButtonTemplate) as GameObject;
            m_AbilityButtonList.Add(button);
            button.SetActive(true);
            button.GetComponentInChildren<Text>().text = $"\t{adr.Name}\n\tMin: {adr.Min}\n\tMax: {adr.Max}";
            button.transform.SetParent(m_AbilityButtonTemplate.transform.parent, false);
        }

        StartCoroutine(SetScrollPos());
    }

    private IEnumerator SetScrollPos()
    {
        yield return null;

        GetComponentInChildren<Scrollbar>().value = 1.0f;
    }
}
