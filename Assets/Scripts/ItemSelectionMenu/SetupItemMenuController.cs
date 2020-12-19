using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//  Controller class for the UI menu that opens when adding any setup items to inventory, armour, etc
public class SetupItemMenuController : MonoBehaviour, IPointerExitHandler
{
    public SetupItemSubMenu ActiveSubMenu { get { return activeSubMenu.GetComponent<SetupItemSubMenu>(); } }
    public int ClickedSlotID { get; private set; }
    public ItemSlotCategories ItemSlotCategory { get; private set; }

    private GameObject activeSubMenu;
    private Stack<GameObject> setupLists = new Stack<GameObject>();
    [SerializeField] private GameObject itemListPrefab;

    private readonly float SUBMENUWIDTH = 140.0f;
    private readonly float BUTTONHEIGHT = 35.0f;
    private readonly float MENUPADDING = 20.0f;
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

    //  Do sizing and setup of first submenu
    private GameObject Setup(int listSize)
    {
        gameObject.SetActive(true);

        //  Move object to mouse position offset by width accounting for current canvas scaling factor
        gameObject.transform.position = new Vector2(Input.mousePosition.x - (SUBMENUWIDTH / 2.0f * UIController.Instance.CanvasScale) + 2.0f, Input.mousePosition.y);

        //  Instantiate and add first submenu as well as set it as the activeSubMenu
        GameObject list = Instantiate(itemListPrefab) as GameObject;
        setupLists.Push(list);
        activeSubMenu = list;

        //  Set size and parenting of submenu
        list.transform.SetParent(gameObject.transform, false);
        RectTransform rect = list.GetComponent<RectTransform>();
        float height = Mathf.Min((BUTTONHEIGHT * MAXBUTTONSINVIEW), (BUTTONHEIGHT * listSize));
        rect.sizeDelta = new Vector2(rect.sizeDelta.x, height);

        //  Check if any part of the menu if offscreen
        MenuLocationData data = IsMenuOnScreen(gameObject.transform.position.y, in height);

        //  Move the menu up if it is offscreen
        if (!data.isOnScreen)
            gameObject.transform.position = new Vector2(gameObject.transform.position.x, (gameObject.transform.position.y + data.distOffScreen + MENUPADDING));

        return list;
    }

    //  Initialize new menu with list of items
    public void NewMenu(ItemSlotCategories itemSlotCategory, int slotID, in List<SetupItemStruct> setupItems)
    {
        this.ItemSlotCategory = itemSlotCategory;
        this.ClickedSlotID = slotID;

        //  Setup and return first submenu
        GameObject list = Setup(setupItems.Count);

        //  Pass data to the submenu for its setup
        list.GetComponent<SetupItemSubMenu>().Setup(in setupItems, 0);
    }

    //  Initialize new menu with list of categories
    public void NewMenu(ItemSlotCategories itemSlotCategory, int slotID, in List<SetupItemCategories> itemCategories)
    {
        this.ItemSlotCategory = itemSlotCategory;
        this.ClickedSlotID = slotID;

        //  Setup and return first submenu
        GameObject list = Setup(itemCategories.Count);

        //  Pass data to the submenu for its setup
        list.GetComponent<SetupItemSubMenu>().Setup(in itemCategories, 0);
    }

    //  Add a new submenu of SetupItemCategories on top of the stack
    public void AddSubmenu(in List<SetupItemCategories> itemCategories, int fromStackIndex, in float maxY)
    {
        SubMenuSetup(itemCategories.Count, in fromStackIndex, in maxY).GetComponent<SetupItemSubMenu>().Setup(in itemCategories, ++fromStackIndex);
    }

    //  Add a new submenu of SetupItems on top of the stack
    public void AddSubmenu(in List<SetupItemStruct> setupItems, int fromStackIndex, in float maxY)
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
        float canvasScale = UIController.Instance.CanvasScale;

        //  Create a new list on top of the stack
        if(fromStackIndex == (setupLists.Count - 1))
        {
            //  Set x position; all submenus continue to the left currently
            Vector3 menuPos = gameObject.transform.position;
            menuPos.x -= (SUBMENUWIDTH * setupLists.Count * canvasScale);

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
        pos.y = (maxY + (BUTTONHEIGHT * canvasScale / 2.0f)) - (height * canvasScale / 2.0f);
        
        //  Check if the menu is fully on screen
        MenuLocationData data = IsMenuOnScreen(in pos.y, in height);

        //  Set the location so that the bottom of the menu is directly left instead if menu is off screen
        if (!data.isOnScreen)
            pos.y += (height - BUTTONHEIGHT);

        subMenu.transform.position = pos;

        return subMenu;
    }

    private MenuLocationData IsMenuOnScreen(in float menuYPos, in float menuHeight)
    {
        float halfHeight = menuHeight / 2.0f;
        MenuLocationData data = new MenuLocationData();

        //  Check if extends above ymax
        float distOffScreen = menuYPos + halfHeight - Screen.safeArea.yMax;
        if(distOffScreen > 0)
        {
            data.isOnScreen = false;
            data.distOffScreen = distOffScreen;
            return data;
        }

        //  Check if extends below ymin
        distOffScreen = (menuYPos - halfHeight - Screen.safeArea.yMin);
        if(distOffScreen < 0)
        {
            data.isOnScreen = false;
            data.distOffScreen = Mathf.Abs(distOffScreen);
            return data;
        }

        //  Menu is completely on screen
        data.isOnScreen = true;
        return data;
    }

    //  Delete all submenus and deactivate this menu OnPointerExit
    public void OnPointerExit(PointerEventData eventData)
    {
        while (setupLists.Count != 0)
            Destroy(setupLists.Pop());

        gameObject.SetActive(false);
    }

    //  Struct to pass back when checking the menu/submenu locations wrt screen space
    private struct MenuLocationData
    {
        public bool isOnScreen;
        public float distOffScreen;
    }
}
