using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// Button used to remove items from setup collections
/// </summary>
public class RemoveItemButton : MonoBehaviour, IPointerExitHandler
{
    //  Properties & fields

    public static RemoveItemButton Instance { get { return _instance; } }

    private static RemoveItemButton _instance = null;
    private Button _thisButton;
    private Enums.SetupCollections _collType;
    private Enums.ItemSlotCategory _slotCategory;
    private int _slotId;
    private Sprite _defaultSlotSprite;

    //  Monobehaviors

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else if (_instance != this)
            Destroy(gameObject);

        _thisButton = GetComponent<Button>();
        _thisButton.onClick.AddListener(OnClick);
        this.gameObject.SetActive(false);
    }

    //  Methods

    public void Show(Enums.SetupCollections collType, Enums.ItemSlotCategory slotCategory, int slotId, Sprite defaultSprite)
    {
        gameObject.SetActive(true);
        gameObject.transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

        this._collType = collType;
        this._slotCategory = slotCategory;
        this._slotId = slotId;
        this._defaultSlotSprite = defaultSprite;
    }

    private void OnClick()
    {
        SetupItem item = General.NullItem();
        item.ItemSprite = _defaultSlotSprite;

        EventManager.Instance.SetupItemAdded(item, 1, _collType, _slotCategory, _slotId);
        gameObject.SetActive(false);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        gameObject.SetActive(false);
    }
}
