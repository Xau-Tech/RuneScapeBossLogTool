using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

//  Set up a link between a BossDropdown and a BossDisplayWidget view to update
public class BossDropdownDisplayLink : DropdownDisplayLink
{
    private BossLogListDisplay view;
    private event Action<string> onValueChanged;
        
    private void Awake()
    {
        base.Setup();
    }

    private void OnEnable()
    {
        EventManager.Instance.onLogDeleted += UpdateView;
        EventManager.Instance.onBossDropdownValueChanged += UpdateView;
        EventManager.Instance.onTabChanged += UpdateView;
    }

    private void OnDisable()
    {
        EventManager.Instance.onLogDeleted -= UpdateView;
        EventManager.Instance.onBossDropdownValueChanged -= UpdateView;
        EventManager.Instance.onTabChanged -= UpdateView;
    }

    //  Add a listener to the onValueChanged event
    public void AddValueChangedAction(Action<string> action)
    {
        onValueChanged += action;
    }

    private void UpdateView()
    {
        Debug.Log("Bossview updated");
        //  Make sure a view exists
        if (!view)
        {
            Debug.Log("view has not been instantiated");
            return;
        }

        string bossName = CacheManager.currentBoss.bossName;
        //string bossName = thisDropdown.options[thisDropdown.value].text;
        //  Update the view with the BossLogList associated with this dropdown's current value
        view.Display(DataController.Instance.bossLogsDictionary.GetBossLogList(DataController.Instance.bossInfoDictionary.GetBossIDByName(in bossName)));

        //  Also invoke this event to update the BossLogDisplay
        onValueChanged?.Invoke(thisDropdown.options[thisDropdown.value].text);
    }

    //  Create and cache the widget (view) with a passed GameObject for the instantiate position
    public override void LinkAndCreateWidget(in GameObject objectLocation)
    {
        view = WidgetFactory.InstantiateWidget(WidgetTypes.BossTotals, objectLocation)
            .GetComponent<BossLogListDisplay>();
    }
}
