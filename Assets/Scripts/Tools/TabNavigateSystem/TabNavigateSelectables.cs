using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//  Allow user to use tab to navigate through editor-set selectables in UI
public class TabNavigateSelectables : MonoBehaviour, IPowerable
{
    [SerializeField] private Selectable[] selectables;
    private int index;

    private void Awake()
    {
        //  Setup for our system and its objects
        for(int i = 0; i < selectables.Length; ++i)
        {
            //  Create and add an SID to each selectable
            SelectableID sid = selectables[i].gameObject.AddComponent<SelectableID>();
            sid.Setup(in i, SelectableTriggered);

            //  Create and add an OnSelect EventTrigger that tells the selectable to notify this script with its ID
            EventTrigger trigger = selectables[i].gameObject.AddComponent<EventTrigger>();
            EventTrigger.Entry selectEntry = new EventTrigger.Entry();
            selectEntry.eventID = EventTriggerType.Select;
            selectEntry.callback.AddListener((data) => { sid.OnSelectCallback((BaseEventData)data); });
            trigger.triggers.Add(selectEntry);
        }
    }

    //  Called by a selectable when it is selected to properly update the index value
    private void SelectableTriggered(int index)
    {
        this.index = index;
    }

    //  Reset index if user clicked and none of the selectables are active at the end of the frame
    private void LateUpdate()
    {
        if (Input.GetMouseButtonDown(0) && !IsSelectableActive())
            index = 0;
    }

    private void Update()
    {
        //  User presses tab
        if (Input.GetKeyDown(KeyCode.Tab) && IsSelectableActive())
            SelectNext();
    }

    //  Update index and select proper selectable object
    private void SelectNext()
    {
        //  Check if the selectable implements IUninterruptable and check if it currently can be deselected
        IUninterruptable interruptInterface = selectables[index].gameObject.GetComponent<IUninterruptable>();
        if(interruptInterface != null)
        {
            if (!interruptInterface.CanDeselect())
                return;
        }

        //  Hide the dropdown template if necessary
        Dropdown dd;
        if (dd = selectables[index].GetComponent<Dropdown>())
            dd.Hide();

        //  Set and select our selectable
        if(index == selectables.Length - 1)
            selectables[index = 0].Select();
        else
            selectables[++index].Select();
    }

    //  Check if one of the selectables is already active
    private bool IsSelectableActive()
    {
        //  Check for null so later compare doesn't get NullReferenceException
        if (EventSystem.current.currentSelectedGameObject == null)
            return false;

        for(int i = 0; i < selectables.Length; ++i)
        {
            //  Currently selected object is either in our list or a child of an object in our list
            if(selectables[i].gameObject == EventSystem.current.currentSelectedGameObject ||
                EventSystem.current.currentSelectedGameObject.transform.IsChildOf(selectables[i].transform))
            {
                return true;
            }
        }

        return false;
    }

    //  IPowerable interface
    public void PowerOn()
    {
        this.enabled = true;
    }

    public void PowerOff()
    {
        this.enabled = false;
    }
    //  End of IPowerable interface
}
