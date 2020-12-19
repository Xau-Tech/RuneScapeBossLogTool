using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RareItemScrollListController : MonoBehaviour, IDisplayable<RareItemList>
{
    [SerializeField] private GameObject rareItemButtonTemplate;
    [SerializeField] private Text emptyListText;
    private List<GameObject> rareItemButtons;
    private static List<Sprite> spriteList;

    private void Awake()
    {
        rareItemButtons = new List<GameObject>();
        spriteList = new List<Sprite>();
    }

    //  Unload unneeded sprite assets from previous boss
    private void UnloadSprites()
    {
        foreach(Sprite sprite in spriteList)
        {
            Resources.UnloadAsset(sprite);
        }
    }

    //  Load item sprites for new boss
    public void LoadNewSprites(in BossInfo boss)
    {
        UnloadSprites();

        //  Load sprites associated with this boss's rare drops
        Sprite[] spriteArray = Resources.LoadAll<Sprite>("RareItems/Sprites/" + ProgramControl.Options.GetOptionValue(RSVersionOption.Name()) + "/" + boss.bossName + "/");

        //  Check if this boss has access to the RareDropTable and add the sprites from that if so
        BossInfo bossInfo;
        if((bossInfo = DataController.Instance.bossInfoDictionary.GetBossByID(boss.bossID)) != null)
        {
            if (bossInfo.hasAccessToRareDropTable)
            {
                Sprite[] rdtSpriteArray = Resources.LoadAll<Sprite>("RareItems/Sprites/" + ProgramControl.Options.GetOptionValue(RSVersionOption.Name()) + "/Rare Drop Table/");
                int spriteArrayOrigSize = spriteArray.Length;

                System.Array.Resize<Sprite>(ref spriteArray, (spriteArray.Length + rdtSpriteArray.Length));
                System.Array.Copy(rdtSpriteArray, 0, spriteArray, spriteArrayOrigSize, rdtSpriteArray.Length);
            }
        }

        //  Copy to global list for searching later
        spriteList = new List<Sprite>(spriteArray);

        for(int i = 0; i < spriteList.Count; ++i)
        {
            Debug.Log("Loaded Sprite: " + spriteList[i].name);
        }
    }

    //  Get the sprite from our loaded list that matches the passed item
    public Sprite GetRareItemSprite(in RareItem rareItem)
    {
        foreach(Sprite sprite in spriteList)
        {
            //Debug.Log($"Comparing {rareItem.GetName()} to {sprite.name}");
            if (sprite.name.CompareTo(rareItem.GetName()) == 0)
                return sprite;
        }

        return null;
    }

    public void Display(in RareItemList rareItemList)
    {
        //  Remove any old buttons
        if (rareItemButtons.Count > 0)
        {
            foreach (GameObject button in rareItemButtons)
                Destroy(button.gameObject);

            rareItemButtons.Clear();
        }

        //  If list is empty display proper element
        if (rareItemList.Count == 0)
        {
            emptyListText.gameObject.SetActive(true);
        }
        else
        {
            emptyListText.gameObject.SetActive(false);

            foreach (RareItem rareItem in rareItemList)
            {
                GameObject button = Instantiate(rareItemButtonTemplate) as GameObject;
                rareItemButtons.Add(button);
                button.SetActive(true);
                button.GetComponent<RareItemButton>().Display(in rareItem);
                button.transform.SetParent(rareItemButtonTemplate.transform.parent, false);
            }
        }
    }
}
