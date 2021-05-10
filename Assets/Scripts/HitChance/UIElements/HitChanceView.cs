using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//  View class for HitChance data
public class HitChanceView : MonoBehaviour
{
    [SerializeField] private Text hitChanceText;

    [SerializeField] private HitChanceIF combatLevelIF;
    [SerializeField] private HitChanceIF weaponAccuracyIF;
    [SerializeField] private HitChanceDropdown attackStyleDD;
    [SerializeField] private HitChanceDropdown prayerDD;
    [SerializeField] private HitChanceDropdown potionDD;
    [SerializeField] private HitChanceDropdown auraDD;
    [SerializeField] private HitChanceDropdown nihilDD;
    [SerializeField] private HitChanceDropdown scrimshawDD;
    [SerializeField] private HitChanceDropdown reaperDD;
    [SerializeField] private HitChanceDropdown quakeDD;
    [SerializeField] private HitChanceDropdown statWarhammerDD;
    [SerializeField] private HitChanceDropdown gstaffDD;
    [SerializeField] private HitChanceDropdown bandosBookDD;

    public void Setup(in List<HitChanceUISetupData> setupData)
    {
        //  Add all data to a list and iterate; setting up with passed in data
        //  ORDER MUST MATCH OR VALUES WILL BE IMPROPERLY SET
        List<AbsHitChanceUIEle> hitChanceElements = new List<AbsHitChanceUIEle>();
        hitChanceElements.Add(combatLevelIF);
        hitChanceElements.Add(weaponAccuracyIF);
        hitChanceElements.Add(attackStyleDD);
        hitChanceElements.Add(prayerDD);
        hitChanceElements.Add(potionDD);
        hitChanceElements.Add(auraDD);
        hitChanceElements.Add(nihilDD);
        hitChanceElements.Add(scrimshawDD);
        hitChanceElements.Add(reaperDD);
        hitChanceElements.Add(quakeDD);
        hitChanceElements.Add(statWarhammerDD);
        hitChanceElements.Add(gstaffDD);
        hitChanceElements.Add(bandosBookDD);

        for(int i = 0; i < hitChanceElements.Count; ++i)
        {
            hitChanceElements[i].Setup(setupData[i]);
        }
    }

    public void SetHitChanceText(string value)
    {
        hitChanceText.text = value;
    }

    public void ResetCombatLevelText(string value)
    {
        combatLevelIF.GetComponent<InputField>().text = value;
    }

    public void ResetWeaponAccuracyText(string value)
    {
        weaponAccuracyIF.GetComponent<InputField>().text = value;
    }
}
