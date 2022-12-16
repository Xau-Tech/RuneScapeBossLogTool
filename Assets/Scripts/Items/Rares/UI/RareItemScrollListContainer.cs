using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Display a collection of RareItems to a scroll list UI
/// </summary>
public class RareItemScrollListContainer : MonoBehaviour
{
    //  Properties & fields

    [SerializeField] private GameObject _rareItemButtonTemplate;
    [SerializeField] private Text _emptyListText;
    private List<GameObject> _rareItemButtons;
    private List<Sprite> _spriteList;

    //  Monobehaviors

    private void Awake()
    {
        _rareItemButtons = new List<GameObject>();
        _spriteList = new List<Sprite>();
    }

    //  Methods

    //  Unload unneeded sprites from previous boss
    private void UnloadSprites()
    {
        foreach(Sprite spr in _spriteList)
        {
            Resources.UnloadAsset(spr);
        }
    }

    //  Load sprites for new boss
    public void LoadSprites(BossInfo boss)
    {
        UnloadSprites();

        //  Load sprites associated with this boss' rare drops
        Sprite[] spriteArr = Resources.LoadAll<Sprite>("RareItems/Sprites/" + ApplicationController.OptionController.GetOptionValue(Enums.OptionNames.RSVersion) + "/" + boss.BossName + "/");

        //  Check if this boss has access to the rare drop table and add those sprites if so
        BossInfo bossRDTCheck;
        if((bossRDTCheck = ApplicationController.Instance.BossInfo.GetBoss(boss.BossId)) != null)
        {
            if (bossRDTCheck.HasAccessToRareDropTable)
            {
                Sprite[] rdtSpriteArr = Resources.LoadAll<Sprite>("RareItems/Sprites/" + ApplicationController.OptionController.GetOptionValue(Enums.OptionNames.RSVersion) + "/" + "Rare Drop Table/");
                int spriteArrayOrigSize = spriteArr.Length;
                //Debug.Log("original size: " + spriteArrayOrigSize);
                System.Array.Resize(ref spriteArr, (spriteArrayOrigSize + rdtSpriteArr.Length));
                //Debug.Log("new size: " + spriteArr.Length);
                System.Array.Copy(rdtSpriteArr, 0, spriteArr, spriteArrayOrigSize, rdtSpriteArr.Length);
            }
        }

        //  Copy to global list for searching later
        _spriteList = new List<Sprite>(spriteArr);
    }

    //  Get the proper sprite for the RareItem passed
    private Sprite GetMatchingSprite(RareItem rareItem, string bossName)
    {
        foreach(Sprite sprite in _spriteList)
        {
            if (sprite.name.CompareTo(rareItem.GetName(bossName)) == 0)
                return sprite;
        }

        return null;
    }

    public void Display(RareItemList rareItemList, string bossName)
    {
        //  Remove old buttons
        if(_rareItemButtons.Count > 0)
        {
            foreach (GameObject button in _rareItemButtons)
            {
                Destroy(button.gameObject);
            }

            _rareItemButtons.Clear();
        }

        //  Display text if list is empty
        if (rareItemList.Count == 0)
        {
            _emptyListText.gameObject.SetActive(true);
        }
        else
        {
            _emptyListText.gameObject.SetActive(false);

            foreach(RareItem rareItem in rareItemList)
            {
                GameObject button = Instantiate(_rareItemButtonTemplate) as GameObject;
                _rareItemButtons.Add(button);
                button.SetActive(true);
                button.GetComponent<RareItemButton>().Display(rareItem, GetMatchingSprite(rareItem, bossName), bossName);
                button.transform.SetParent(_rareItemButtonTemplate.transform.parent, false);
            }
        }
    }
}
