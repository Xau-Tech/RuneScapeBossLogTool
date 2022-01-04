using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// List of items received as drops from a pvm encounter displayed in the drops tab
/// </summary>
public class DropList : MonoBehaviour
{
    //  Properties & fields
    public ulong Value { get { return _data.TotalValue(); } }
    public ItemSlotList Drops { get { return _data; } }

    private ItemSlotList _data;
    private List<GameObject> _dataButtons;
    private int _indexToRemove;
    private float _scrollPos;
    private bool _updated = false;
    private ScrollRect _scrollRect;
    [SerializeField] private GameObject _dataButtonTemplate;
    [SerializeField] private GameObject _removeDropButton;
    [SerializeField] private Text _dropListValueText;

    private readonly Color NORMALBACKGROUNDCOLOR = Color.white;
    private readonly Color RAREBACKGROUNDCOLOR = Color.yellow;

    //  Monobehaviors

    private void Awake()
    {
        _data = new ItemSlotList();
        _dataButtons = new List<GameObject>();
        _scrollRect = GetComponent<ScrollRect>();
        _removeDropButton.GetComponent<Button>().onClick.AddListener(RemoveDrop);
    }

    //  Methods

    private void GenerateList(int addedItemId)
    {
        _updated = true;

        //  Destroy each button and clear the list
        if(_dataButtons.Count > 0)
        {
            foreach(GameObject button in _dataButtons)
            {
                Destroy(button.gameObject);
            }

            _dataButtons.Clear();
        }

        for(int i = 0; i < _data.Count; ++i)
        {
            //  Create and add a button to the list of buttons
            GameObject button = Instantiate(_dataButtonTemplate) as GameObject;
            _dataButtons.Add(button);
            button.SetActive(true);

            ItemSlot itemSlot = _data.AtIndex(i);

            //  Set the button's linked drop
            button.GetComponent<DropListButton>().Setup(this, itemSlot, i, itemSlot.Item.ItemId == addedItemId);

            //  Set parent so scroll + layout group function properly
            button.transform.SetParent(_dataButtonTemplate.transform.parent, false);
            Image buttonImage = button.GetComponent<Image>();

            //  Set button background color
            if (RareItemDB.IsRare(ApplicationController.Instance.CurrentBoss.BossName, itemSlot.Item.ItemId))
                buttonImage.color = RAREBACKGROUNDCOLOR;
            else
                buttonImage.color = NORMALBACKGROUNDCOLOR;
        }

        //  Set scrollbar pos
        if(addedItemId == -1)
        {
            _scrollPos = 1;
        }
        else
        {
            ItemSlot addedItemSlot = _data.Find(addedItemId);

            if(addedItemSlot == null)
            {
                _scrollPos = 1;
            }
            else
            {
                int dropIndex = _data.IndexOf(addedItemSlot);

                if (dropIndex == 0)
                    _scrollPos = 1.0f;
                else
                    _scrollPos = (1 - ((float)(dropIndex + 1) / _data.Count));
            }
        }

        _dropListValueText.text = $"Total value: {_data.TotalValue().ToString("N0")} gp";
        StartCoroutine(SetScrollPos());
    }

    public void ShowRemoveDropButton(int index)
    {
        _removeDropButton.gameObject.SetActive(true);
        _removeDropButton.transform.position = Input.mousePosition;
        this._indexToRemove = index;
    }

    public void Add(ItemSlot itemSlot)
    {
        _data.Add(itemSlot);
        GenerateList(itemSlot.Item.ItemId);
    }

    public void AddToDrop(string dropName, uint addedQuantity, int addedItemId)
    {
        _data.AddToDrop(dropName, addedQuantity);
        GenerateList(addedItemId);
    }

    public bool Exists(string itemName)
    {
        return _data.Exists(itemName);
    }

    public void Clear()
    {
        _data.Clear();
        GenerateList(-1);
    }

    private void RemoveDrop()
    {
        _data.Remove(_indexToRemove);
        GenerateList(-1);
        _removeDropButton.SetActive(false);
    }

    public void Print()
    {
        _data.Print();
    }

    //  Set the position of the scrollbar
    private IEnumerator SetScrollPos()
    {
        /*  DO NOT REMOVE
         Weird unity quirk
         Yield return appears necessary because Unity modifies the value at some point for some reason (???)
        */

        yield return null;

        if (_updated)
        {
            _scrollRect.verticalNormalizedPosition = _scrollPos;
            _updated = false;
        }
    }
}
