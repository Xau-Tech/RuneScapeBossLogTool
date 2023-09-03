using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossInfoView : MonoBehaviour
{
    [SerializeField] private Text _subBossNameText;
    [SerializeField] private Text _lifePointsText;
    [SerializeField] private Text _poisonousText;
    [SerializeField] private Text _weaknessAffinityText;
    [SerializeField] private Text _meleeAffinityText;
    [SerializeField] private Text _rangedAffinityText;
    [SerializeField] private Text _magicAffinityText;
    [SerializeField] private Text _necromancyAffinityText;
    [SerializeField] private Text _poisonImmuneText;
    [SerializeField] private Text _deflectImmuneText;
    [SerializeField] private Text _stunImmuneText;
    [SerializeField] private Text _statDrainImmuneText;
    [SerializeField] private Image _weaknessImage;
    [SerializeField] private Image _monsterTypeSuscImage;
    [SerializeField] private Image _combatClassSuscImage;
    [SerializeField] private Sprite[] _weaknessSprites;
    [SerializeField] private Sprite[] _monsterTypeSuscSprites;
    [SerializeField] private Sprite[] _combatClassSuscSprites;

    private readonly string IMMUNETEXT = "Immune";
    private readonly string NOTIMMUNETEXT = "Not immune";

    //  Get and display all info for current sub-boss
    public void DisplayBossCombatInfo(BossCombatData combatInfo)
    {
        //  Basic info - name, lifepoints, whether or not it can poison the player
        _subBossNameText.text = combatInfo.Name;
        _lifePointsText.text = combatInfo.Lifepoints != -1 ? (combatInfo.Lifepoints.ToString("N0")) : "Group size dependant";
        _poisonousText.text = combatInfo.Poisonous ? "Yes" : "No";

        //  AFFINITIES
        //  Images
        _weaknessImage.sprite = null;
        _weaknessImage.sprite = _weaknessSprites[(int)combatInfo.AffinityData.AttackStyleWeakness];
        _weaknessAffinityText.text = combatInfo.AffinityData.WeaknessAffinity == -1 ? "-" : combatInfo.AffinityData.WeaknessAffinity + "";

        //  Text
        _meleeAffinityText.text = combatInfo.AffinityData.MeleeAffinity + "";
        _rangedAffinityText.text = combatInfo.AffinityData.RangedAffinity + "";
        _magicAffinityText.text = combatInfo.AffinityData.MagicAffinity + "";
        _necromancyAffinityText.text = combatInfo.AffinityData.NecromancyAffinity + "";

        //  Immunities
        _poisonImmuneText.text = combatInfo.PoisonImmune ? IMMUNETEXT : NOTIMMUNETEXT;
        _deflectImmuneText.text = combatInfo.ReflectImmune ? IMMUNETEXT : NOTIMMUNETEXT;
        _stunImmuneText.text = combatInfo.StunImmune ? IMMUNETEXT : NOTIMMUNETEXT;
        _statDrainImmuneText.text = combatInfo.StatDrainImmune ? IMMUNETEXT : NOTIMMUNETEXT;

        //  Susceptibilities; always reset sprite to null first
        _monsterTypeSuscImage.sprite = null;
        _combatClassSuscImage.sprite = null;
        _monsterTypeSuscImage.sprite = _monsterTypeSuscSprites[(int)combatInfo.MonsterType];
        _combatClassSuscImage.sprite = _combatClassSuscSprites[(int)combatInfo.CombatClass];
    }
}
