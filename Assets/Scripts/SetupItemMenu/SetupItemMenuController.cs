using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Controller for the SetupItem menu that allows users to select and add items to their setups
/// </summary>
public class SetupItemMenuController : MonoBehaviour, IPointerExitHandler
{
    //  Properties & fields

    public static SetupItemMenuController Instance { get { return _instance; } }
    public SetupItemSubMenu ActiveSubMenu { get { return _activeSubMenu.GetComponent<SetupItemSubMenu>(); } }
    public int ClickedSlotID { get; private set; }
    public uint AmountToWithdraw { get; private set; }
    public Enums.ItemSlotCategory ItemSlotCategory { get; private set; }
    public Enums.SetupCollections CollectionType { get; private set; }

    private static SetupItemMenuController _instance = null;
    private GameObject _activeSubMenu;
    private Stack<GameObject> _setupLists = new Stack<GameObject>();
    [SerializeField] private GameObject _itemListPrefab;

    private readonly float _SUBMENUWIDTH = 140.0f;
    private readonly float _BUTTONHEIGHT = 35.0f;
    private readonly float _MENUPADDING = 20.0f;
    private readonly int _MAXBUTTONSINVIEW = 6;

    //  Monobehaviors

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else if (_instance != this)
            Destroy(gameObject);
    }

    //  Methods

    private void SetActiveSubMenu(GameObject obj)
    {
        _activeSubMenu = obj;
    }

    //  Do sizing and setup of first submenu
    private GameObject Setup(int listSize)
    {
        this.gameObject.SetActive(true);

        //  Move object to mouse position offset by width accounting for canvas scaling
        this.gameObject.transform.position = new Vector2(Input.mousePosition.x - (_SUBMENUWIDTH / 2.0f * ApplicationController.Instance.CanvasScale) + 2.0f, Input.mousePosition.y);

        //  Instantiate and add first submenu as well as set it as the activeSubMenu
        GameObject list = Instantiate(_itemListPrefab) as GameObject;
        _setupLists.Push(list);
        _activeSubMenu = list;

        //  Set size and parenting of submenu
        list.transform.SetParent(gameObject.transform, false);
        RectTransform rect = list.GetComponent<RectTransform>();
        float height = Mathf.Min((_BUTTONHEIGHT * _MAXBUTTONSINVIEW), (_BUTTONHEIGHT * listSize));
        rect.sizeDelta = new Vector2(rect.sizeDelta.x, height);

        //  Check if any part of the menu is offscreen
        MenuLocationData mld = IsMenuOnScreen(gameObject.transform.position.y, height * ApplicationController.Instance.CanvasScale);

        //  Move the menu up if it is offscreen
        if (!mld.IsOnScreen)
            gameObject.transform.position = new Vector2(gameObject.transform.position.x, (gameObject.transform.position.y + mld.DistOffScreen + _MENUPADDING));

        return list;
    }

    //  Initialize new menu with list of items
    public void NewMenu(Enums.SetupCollections collectionType, Enums.ItemSlotCategory itemSlotCategory, int slotId, List<SetupItemStruct> setupItems, uint amountToWithdraw)
    {
        this.CollectionType = collectionType;
        this.ItemSlotCategory = itemSlotCategory;
        this.ClickedSlotID = slotId;
        this.AmountToWithdraw = amountToWithdraw;

        //  Setup and return first submenu
        GameObject list = Setup(setupItems.Count);

        //  Pass data to the submenu for setup
        list.GetComponent<SetupItemSubMenu>().Setup(setupItems, 0);
    }

    //  Initialize new menu with list of setup item categories
    public void NewMenu(Enums.SetupCollections collectionType, Enums.ItemSlotCategory itemSlotCategory, int slotId, List<Enums.SetupItemCategory> itemCategories, uint amountToWithdraw)
    {
        this.CollectionType = collectionType;
        this.ItemSlotCategory = itemSlotCategory;
        this.ClickedSlotID = slotId;
        this.AmountToWithdraw = amountToWithdraw;

        //  Setup and return first submenu
        GameObject list = Setup(itemCategories.Count);

        //  Pass data to the submenu for its setup
        list.GetComponent<SetupItemSubMenu>().Setup(itemCategories, 0);
    }

    //  Add a new submenu of SetupItemCategories on top of the stack
    public void AddSubmenu(List<Enums.SetupItemCategory> itemCategories, int fromStackIndex, float maxY)
    {
        SubMenuSetup(itemCategories.Count, fromStackIndex, maxY).GetComponent<SetupItemSubMenu>().Setup(itemCategories, ++fromStackIndex);
    }

    //  Add a new submenu of SetupItems on top of the stack
    public void AddSubmenu(List<SetupItemStruct> setupItems, int fromStackIndex, float maxY)
    {
        SubMenuSetup(setupItems.Count, fromStackIndex, maxY).GetComponent<SetupItemSubMenu>().Setup(setupItems, ++fromStackIndex);
    }

    //  Create, size, and translate submenus before their setup
    private GameObject SubMenuSetup(int numItems, int fromStackIndex, float maxY)
    {
        //  Destroy any submenus on top of where the new one will sit
        while (_setupLists.Count > (fromStackIndex + 2))
            Destroy(_setupLists.Pop());

        GameObject subMenu = null;
        float canvasScale = ApplicationController.Instance.CanvasScale;

        //  Create a new list on top of the stack
        if (fromStackIndex == (_setupLists.Count - 1))
        {
            //  Set x position; all submenus continue to the left currently
            Vector3 menuPos = gameObject.transform.position;
            menuPos.x -= (_SUBMENUWIDTH * _setupLists.Count * canvasScale);

            subMenu = Instantiate(_itemListPrefab, menuPos, Quaternion.identity, gameObject.transform) as GameObject;
            _setupLists.Push(subMenu);
        }
        //  Re-assigning top stack value
        else
        {
            subMenu = _setupLists.Peek();
        }

        //  Set the height delta of the submenu
        RectTransform rect = subMenu.GetComponent<RectTransform>();
        float height = Mathf.Min((_BUTTONHEIGHT * _MAXBUTTONSINVIEW), (_BUTTONHEIGHT * numItems));
        rect.sizeDelta = new Vector2(rect.sizeDelta.x, height);

        //  Set the y position of the new submenu to align with the selected button in the previous submenu
        Vector3 pos = subMenu.transform.position;
        pos.y = (maxY + (_BUTTONHEIGHT * canvasScale / 2.0f)) - (height * canvasScale / 2.0f);

        //  Check if the menu is fully on screen
        MenuLocationData mld = IsMenuOnScreen(pos.y, height * canvasScale);

        //  Set the location so that the bottom of the menu is directly left instead if the menu is off screen
        if (!mld.IsOnScreen)
            pos.y += ((height - _BUTTONHEIGHT) * canvasScale);

        subMenu.transform.position = pos;

        return subMenu;
    }

    private MenuLocationData IsMenuOnScreen(float menuYPos, float menuHeight)
    {
        float halfHeight = menuHeight / 2.0f;
        MenuLocationData data = new MenuLocationData();

        //  Check if extends above ymax
        float distOffScreen = menuYPos + halfHeight - Screen.height;
        if (distOffScreen >= 0)
        {
            data.IsOnScreen = false;
            data.DistOffScreen = distOffScreen;
            return data;
        }

        //  Check if extends below ymin
        distOffScreen = menuYPos - halfHeight;
        if (distOffScreen <= 0)
        {
            data.IsOnScreen = false;
            data.DistOffScreen = Mathf.Abs(distOffScreen);
            return data;
        }

        //  Menu is completely on screen
        data.IsOnScreen = true;
        return data;
    }

    //  Delete all submenus and deactivate this menu OnPointerExit
    public void OnPointerExit(PointerEventData eventData)
    {
        while (_setupLists.Count != 0)
        {
            Destroy(_setupLists.Pop());
        }

        gameObject.SetActive(false);
    }

    //  Struct to pass back when checking the menu/submenu locations wrt screen space
    private struct MenuLocationData
    {
        public bool IsOnScreen;
        public float DistOffScreen;
    }
}
