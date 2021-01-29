using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//  Controller object for the droplist UI element which is a dynamic scroll list of buttons
public class DropListController : MonoBehaviour
{
    public List<GameObject> dropListButtons { get; private set; }

    [SerializeField] private GameObject buttonTemplate;
    private float scrollPosition;
    private bool listUpdated = false;
    private ScrollRect scrollRect;

    private readonly Color NORMALBACKGROUNDCOLOR = Color.white;
    private readonly Color RAREBACKGROUNDCOLOR = Color.yellow;

    private void Awake()
    {
        dropListButtons = new List<GameObject>();
        scrollRect = GetComponent<ScrollRect>();
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
    private void GenerateList(int addedItemID)
    {
        listUpdated = true;

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

            ItemSlot itemSlot = DataController.Instance.dropList.AtIndex(in i);

            //  Set drop the button will be linked to (this sets the button text as well)
            button.GetComponent<DropListButton>().SetDrop(in itemSlot, itemSlot.item.itemID == addedItemID);
            
            //  Set parent so scroll + layout group function properly
            button.transform.SetParent(buttonTemplate.transform.parent, false);

            Image buttonImage = button.GetComponent<Image>();

            //  Set button background color based on normal vs unique/rare drops
            if (RareItemDB.IsRare(CacheManager.currentBoss.bossName, itemSlot.item.itemID))
                buttonImage.color = RAREBACKGROUNDCOLOR;
            else
                buttonImage.color = NORMALBACKGROUNDCOLOR;
        }

        //  Set scrollbar to proper position
        if (addedItemID == -1)
            scrollPosition = 1;
        else
        {
            ItemSlot itemSlot = DataController.Instance.dropList.Find(addedItemID);

            if (itemSlot == null)
                scrollPosition = 1;
            else
            {
                int dropIndex = DataController.Instance.dropList.IndexOf(in itemSlot);

                if (dropIndex == 0)
                    scrollPosition = 1.0f;
                else
                    scrollPosition = (1 - ((float)(dropIndex + 1) / DataController.Instance.dropList.Count));
            }
        }

        StartCoroutine(SetScrollPos());
    }

    //  Set the position of the scrollbar
    private IEnumerator SetScrollPos()
    {
        /*  DO NOT REMOVE
         Weird unity quirk
         Yield return appears necessary because Unity modifies the value at some point for some reason (???)
        */

        yield return null;

        if (listUpdated)
        {
            scrollRect.verticalNormalizedPosition = scrollPosition;
            listUpdated = false;
        }
    }
}
