using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitChanceView : MonoBehaviour
{
    //  Properties & fields

    [SerializeField] private Text _hitChanceText;
    [SerializeField] private HitChanceInputField _combatLevelIF;
    [SerializeField] private HitChanceInputField _weaponAccuracyIF;
    [SerializeField] private HitChanceDropdown _attackStyleDD;
    [SerializeField] private HitChanceDropdown _prayerDD;
    [SerializeField] private HitChanceDropdown _potionDD;
    [SerializeField] private HitChanceDropdown _auraDD;
    [SerializeField] private HitChanceDropdown _nihilDD;
    [SerializeField] private HitChanceDropdown _scrimshawDD;
    [SerializeField] private HitChanceDropdown _reaperDD;
    [SerializeField] private HitChanceDropdown _quakeDD;
    [SerializeField] private HitChanceDropdown _statWarhammerDD;
    [SerializeField] private HitChanceDropdown _gstaffDD;
    [SerializeField] private HitChanceDropdown _bandosBookDD;

    //  Methods

    public void Setup(List<HitChanceUISetupData> setupData)
    {
        //  Add all data to a list and iterate setting up with passed in data
        //  ORDER MUST MATCH OR VALUES WILL BE IMPROPERLY SET
        List<AbsHitChanceUIElement> hcEles = new List<AbsHitChanceUIElement>();
        hcEles.Add(_combatLevelIF);
        hcEles.Add(_weaponAccuracyIF);
        hcEles.Add(_attackStyleDD);
        hcEles.Add(_prayerDD);
        hcEles.Add(_potionDD);
        hcEles.Add(_auraDD);
        hcEles.Add(_nihilDD);
        hcEles.Add(_scrimshawDD);
        hcEles.Add(_reaperDD);
        hcEles.Add(_quakeDD);
        hcEles.Add(_statWarhammerDD);
        hcEles.Add(_gstaffDD);
        hcEles.Add(_bandosBookDD);

        for(int i = 0; i < hcEles.Count; ++i)
        {
            hcEles[i].Setup(setupData[i]);
        }
    }

    public void SetHitChanceText(string text)
    {
        _hitChanceText.text = text;
    }

    public void ResetCombatLevelText(string text)
    {
        _combatLevelIF.GetComponent<InputField>().text = text;
    }

    public void ResetWeaponAccuracyText(string text)
    {
        _weaponAccuracyIF.GetComponent<InputField>().text = text;
    }
}
