using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//  Controller class for the UI menu that opens when adding any setup items to inventory, armour, etc
public class SetupItemMenuController : MonoBehaviour, IPointerExitHandler
{
    public SetupItemSubMenu ActiveSubMenu { get { return activeSubMenu.GetComponent<SetupItemSubMenu>(); } }

    private GameObject activeSubMenu;
    private ItemSlotCategories clickedSlotCategory;
    private int clickedSlotID;
    private Stack<GameObject> setupLists = new Stack<GameObject>();
    [SerializeField] private GameObject itemListPrefab;

    private readonly float SUBMENUWIDTH = 98.0f;
    private readonly float BUTTONHEIGHT = 30.0f;
    private readonly int MAXBUTTONSINVIEW = 6;

    private void OnEnable()
    {
        EventManager.Instance.onPointerEnterSetupItemSubMenu += SetActiveSubMenu;
    }

    private void OnDisable()
    {
        EventManager.Instance.onPointerEnterSetupItemSubMenu -= SetActiveSubMenu;
    }

    private void SetActiveSubMenu(GameObject obj)
    {
        activeSubMenu = obj;
    }

    //  Initialize the menu and create the first submenu
    public void NewMenu(in ItemSlotCategories itemSlotCategory, in int slotID, in List<SetupItemCategories> itemCategories)
    {
        gameObject.SetActive(true);

        this.clickedSlotCategory = itemSlotCategory;
        this.clickedSlotID = slotID;

        //  Move object to mouse position
        this.gameObject.transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

        //  Instantiate and add first submenu as well as set it as the activeSubMenu
        GameObject list = Instantiate(itemListPrefab) as GameObject;
        setupLists.Push(list);
        activeSubMenu = list;

        //  Set size and parenting of submenu
        list.transform.SetParent(gameObject.transform, false);
        RectTransform rect = list.GetComponent<RectTransform>();
        float height = Mathf.Min((BUTTONHEIGHT * MAXBUTTONSINVIEW), (BUTTONHEIGHT * itemCategories.Count));
        rect.sizeDelta = new Vector2(rect.sizeDelta.x, height);

        //  Pass data to the submenu for its setup
        list.GetComponent<SetupItemSubMenu>().Setup(in itemCategories, 0);
    }

    //  Add a new submenu of SetupItemCategories on top of the stack
    public void AddSubmenu(in List<SetupItemCategories> itemCategories, int fromStackIndex, in float maxY)
    {
        SubMenuSetup(itemCategories.Count, in fromStackIndex, in maxY).GetComponent<SetupItemSubMenu>().Setup(in itemCategories, ++fromStackIndex);
    }

    //  Add a new submenu of SetupItems on top of the stack
    public void AddSubmenu(in List<SetupItem> setupItems, int fromStackIndex, in float maxY)
    {
        SubMenuSetup(setupItems.Count, in fromStackIndex, in maxY).GetComponent<SetupItemSubMenu>().Setup(in setupItems, ++fromStackIndex);
    }

    //  Create, size, and translate submenus before their setup
    private GameObject SubMenuSetup(in int numItems, in int fromStackIndex, in float maxY)
    {
        //  Destroy any submenus on top of where the new one will sit
        while (setupLists.Count > (fromStackIndex + 2))
            Destroy(setupLists.Pop());

        GameObject subMenu = null;

        //  Create a new list on top of the stack
        if(fromStackIndex == (setupLists.Count - 1))
        {
            //  Set x position; all submenus continue to the left currently
            Vector3 menuPos = gameObject.transform.position;
            menuPos.x -= (SUBMENUWIDTH * setupLists.Count);

            subMenu = Instantiate(itemListPrefab, menuPos, Quaternion.identity, gameObject.transform) as GameObject;
            setupLists.Push(subMenu);
        }
        //  Re-assigning top stack value
        else
            subMenu = setupLists.Peek();


        //  Set the height delta of the submenu
        RectTransform rect = subMenu.GetComponent<RectTransform>();
        float height = Mathf.Min((BUTTONHEIGHT * MAXBUTTONSINVIEW), (BUTTONHEIGHT * numItems));
        rect.sizeDelta = new Vector2(rect.sizeDelta.x, height);

        //  Set the y position of the new submenu to align with the selected button in the previous submenu
        Vector3 pos = subMenu.transform.position;
        pos.y = (maxY + (BUTTONHEIGHT / 2.0f)) - (height / 2.0f);
        subMenu.transform.position = pos;

        return subMenu;
    }

    //  Delete all submenus and deactivate this menu OnPointerExit
    public void OnPointerExit(PointerEventData eventData)
    {
        while (setupLists.Count != 0)
            Destroy(setupLists.Pop());

        gameObject.SetActive(false);
    }
}
