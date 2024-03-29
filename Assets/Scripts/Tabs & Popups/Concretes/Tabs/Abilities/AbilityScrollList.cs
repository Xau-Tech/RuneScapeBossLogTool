using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityScrollList : MonoBehaviour
{
    [SerializeField] private GameObject m_AbilityButtonTemplate;
    private List<GameObject> m_AbilityButtonList = new();

    //  Test function for putting some data on screen for now will flesh out later
    public void Display(IEnumerable<AbilityResult> abilDamageResultsColl, string sortOption)
    {
        if(m_AbilityButtonList.Count > 0)
        {
            foreach (GameObject button in m_AbilityButtonList)
            {
                Destroy(button);
            }

            m_AbilityButtonList.Clear();
        }

        foreach(AbilityResult ar in abilDamageResultsColl)
        {
            GameObject button = Instantiate(m_AbilityButtonTemplate) as GameObject;
            m_AbilityButtonList.Add(button);
            button.SetActive(true);
            button.GetComponentInChildren<AbilityResultButton>().Set(ar);
            button.transform.SetParent(m_AbilityButtonTemplate.transform.parent, false);

            Text[] childTexts = button.GetComponentsInChildren<Text>();
            foreach(Text t in childTexts)
            {
                if (t.CompareTag(sortOption))
                {
                    t.color = Color.yellow;
                    break;
                }
            }
        }

        StartCoroutine(SetScrollPos());
    }

    private IEnumerator SetScrollPos()
    {
        yield return null;

        GetComponentInChildren<Scrollbar>().value = 1.0f;
    }
}