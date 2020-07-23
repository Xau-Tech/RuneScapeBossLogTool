using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  Controller object for the droplist UI element which is a dynamic scroll list of buttons
public class DropListController : MonoBehaviour
{
    public List<GameObject> dropListButtons { get; private set; }

    [SerializeField]
    private GameObject buttonTemplate;

    private void Awake()
    {
        dropListButtons = new List<GameObject>();
    }

    public void OnEnable()
    {
        EventManager.Instance.onDropListModified += GenerateList;
    }

    public void OnDisable()
    {
        EventManager.Instance.onDropListModified -= GenerateList;
    }

    //  Generate and display our data in UI
    private void GenerateList()
    {
        //  Destroy each button and clear the list
        if(dropListButtons.Count > 0)
        {
            foreach(GameObject button in dropListButtons)
            {
                Destroy(button.gameObject);
            }

            dropListButtons.Clear();
        }

        for(int i = 0; i < DataController.Instance.dropList.Count; ++i)
        {
            //  Create and add a button to the list of buttons
            GameObject button = Instantiate(buttonTemplate) as GameObject;
            dropListButtons.Add(button);
            button.SetActive(true);

            //  Set drop the button will be linked to (this sets the button text as well)
            button.GetComponent<DropListButton>().SetDrop(DataController.Instance.dropList.AtIndex(in i));
            
            //  Set parent so scroll + layout group function properly
            button.transform.SetParent(buttonTemplate.transform.parent, false);
        }
    }
}
