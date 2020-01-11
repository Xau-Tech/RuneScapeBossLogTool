using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TabJumpInteractables : MonoBehaviour
{
    [SerializeField]
    private Selectable[] m_Selectables;


    private void Update()
    {
        //  Make sure the current selected game object isn't null
        if(EventSystem.current.currentSelectedGameObject == null)
        {
            return;
        }
        //  Tab has been pressed
        else if (Input.GetKeyDown(KeyCode.Tab))
        {
            int x;

            //  One of the input fields is currently selected
            if((x = InputSelected()) != -1)
            {
                //  Select the next input field
                if (x == (m_Selectables.Length - 1))
                    m_Selectables[0].Select();
                else
                    m_Selectables[x + 1].Select();
            }
        }
    }

    //  Check if one of the input fields is selected
    private int InputSelected()
    {
        for(int i = 0; i < m_Selectables.Length; ++i)
        {
            if(EventSystem.current.currentSelectedGameObject.Equals(m_Selectables[i].gameObject))
            {
                return i;
            }
        }

        return -1;
    }
}
