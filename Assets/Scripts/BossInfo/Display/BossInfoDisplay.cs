using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//  Display class for the sub-boss combat info
public class BossInfoDisplay : MonoBehaviour
{
    [SerializeField] private Text subBossNameText;
    [SerializeField] private Text lifePointsText;
    [SerializeField] private Text poisonousText;
    [SerializeField] private Text weaknessAffinityText;
    [SerializeField] private Text meleeAffinityText;
    [SerializeField] private Text rangedAffinityText;
    [SerializeField] private Text magicAffinityText;
    [SerializeField] private Text poisonImmuneText;
    [SerializeField] private Text deflectImmuneText;
    [SerializeField] private Text stunImmuneText;
    [SerializeField] private Text statDrainImmuneText;

    [SerializeField] private Image weaknessImage;
    [SerializeField] private Image monsterTypeSuscImage;
    [SerializeField] private Image combatClassSuscImage;

    [SerializeField] private Button leftArrow;
    [SerializeField] private Button rightArrow;

    [SerializeField] private Sprite[] weaknessSprites;
    [SerializeField] private Sprite[] monsterTypeSuscSprites;
    [SerializeField] private Sprite[] combatClassSuscSprites;

    private readonly string IMMUNETEXT = "Immune";
    private readonly string NOTIMMUNETEXT = "Not immune";

    private void OnEnable()
    {
        leftArrow.onClick.AddListener(DecrementSubBossIndex);
        rightArrow.onClick.AddListener(IncrementSubBossIndex);

        EventManager.Instance.onBossDropdownValueChanged += BossChanged;

        DisplayBossCombatInfo();
    }

    private void OnDisable()
    {
        leftArrow.onClick.RemoveAllListeners();
        rightArrow.onClick.RemoveAllListeners();
        EventManager.Instance.onBossDropdownValueChanged -= BossChanged;
    }

    //  Called when boss dropdown is changed
    private void BossChanged()
    {
        CacheManager.BossInfoTab.currentSubBossIndex = 0;
        DisplayBossCombatInfo();
    }

    private void DecrementSubBossIndex()
    {
        --CacheManager.BossInfoTab.currentSubBossIndex;
        DisplayBossCombatInfo();
    }

    private void IncrementSubBossIndex()
    {
        ++CacheManager.BossInfoTab.currentSubBossIndex;
        DisplayBossCombatInfo();
    }

    //  Get and display all info for current sub-boss
    private void DisplayBossCombatInfo()
    {
        if (CacheManager.BossInfoTab.CurrentSubBossList.Count != 0)
        {
            //UPDATE ARROW UI - disable buttons if not possible to increment and/or decrement
            if (CacheManager.BossInfoTab.currentSubBossIndex == 0)
                leftArrow.interactable = false;
            else
                leftArrow.interactable = true;
            if (CacheManager.BossInfoTab.currentSubBossIndex == CacheManager.BossInfoTab.CurrentSubBossList.Count - 1)
                rightArrow.interactable = false;
            else
                rightArrow.interactable = true;

            //  Get the current sub-boss data
            BossCombatData bcd = CacheManager.BossInfoTab.CurrentSubBoss;

            //  Basic info - name, lifepoints, whether or not it can poison the player
            subBossNameText.text = bcd.name;
            lifePointsText.text = bcd.lifepoints.ToString("N0");
            poisonousText.text = bcd.poisonous ? "Yes" : "No";

            //  AFFINITIES
            //  Images
            weaknessImage.sprite = null;
            weaknessImage.sprite = weaknessSprites[(int)bcd.affinityData.attackStyleWeakness];
            weaknessAffinityText.text = bcd.affinityData.weaknessAffinity == -1 ? "-" : bcd.affinityData.weaknessAffinity + "";

            //  Text
            meleeAffinityText.text = bcd.affinityData.meleeAffinity + "";
            rangedAffinityText.text = bcd.affinityData.rangedAffinity + "";
            magicAffinityText.text = bcd.affinityData.magicAffinity + "";

            //  Immunities
            poisonImmuneText.text = bcd.poisonImmune ? IMMUNETEXT : NOTIMMUNETEXT;
            deflectImmuneText.text = bcd.reflectImmune ? IMMUNETEXT : NOTIMMUNETEXT;
            stunImmuneText.text = bcd.stunImmune ? IMMUNETEXT : NOTIMMUNETEXT;
            statDrainImmuneText.text = bcd.statDrainImmune ? IMMUNETEXT : NOTIMMUNETEXT;

            //  Susceptibilities; always reset sprite to null first
            monsterTypeSuscImage.sprite = null;
            combatClassSuscImage.sprite = null;
            monsterTypeSuscImage.sprite = monsterTypeSuscSprites[(int)bcd.monsterType];
            combatClassSuscImage.sprite = combatClassSuscSprites[(int)bcd.combatClass];
        }

        //  Call event to calculate and display hit chance
        EventManager.Instance.UpdateHitChance();
    }
}
