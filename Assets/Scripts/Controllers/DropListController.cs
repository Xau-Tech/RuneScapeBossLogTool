using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropListController : MonoBehaviour
{
    public List<GameObject> DropListButtons { get { return m_DropListButtons; } }
    public GameObject ActiveButton { get { return m_ActiveButton; } set { m_ActiveButton = value; } }


    [SerializeField]
    private GameObject buttonTemplate;


    private List<GameObject> m_DropListButtons;
    private GameObject m_ActiveButton;


    private void Awake()
    {
        m_DropListButtons = new List<GameObject>();
    }


    public void GenerateList()
    {
        //  Destroy each button and clear the list
        if(m_DropListButtons.Count > 0)
        {
            foreach(GameObject button in m_DropListButtons)
            {
                Destroy(button.gameObject);
            }

            m_DropListButtons.Clear();
        }

        for(int i = 0; i < DataController.dataController.DropListClass.DropList.Count; ++i)
        {
            //  Create and add a button to the list of buttons
            GameObject button = Instantiate(buttonTemplate) as GameObject;
            m_DropListButtons.Add(button);
            button.SetActive(true);

            //  Get the drop that will be associated with this button
            Drop drop = DataController.dataController.DropListClass.DropList[i];

            //  Set the button text
            button.GetComponent<DropListButton>().SetText(drop.ToString());
            
            //  Set parent so scroll + layout group function properly
            button.transform.SetParent(buttonTemplate.transform.parent, false);
        }


        //  Update total value text
        if (DataController.dataController.DropListClass.DropList.Count == 0)
            UIController.uicontroller.m_DropsValueText.text = "Total value: 0 gp";
        else
            UIController.uicontroller.m_DropsValueText.text = "Total value: " +
                DataController.dataController.DropListClass.GetTotalValue().ToString("#,#") + " gp";
    }
}
