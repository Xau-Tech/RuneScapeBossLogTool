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
        EventManager.Instance.onBossDropdownValueChanged += UpdateView;
    }

    private void OnEnable()
    {
        EventManager.Instance.onLogDeleted += LogDeleted;
        UpdateView();
    }

    private void OnDisable()
    {
        EventManager.Instance.onLogDeleted -= LogDeleted;
    }

    //  Updates the BossLogList data if a Log is deleted
    private void LogDeleted()
    {
        UpdateView();
    }

    //  Add a listener to the onValueChanged event
    public void AddValueChangedAction(Action<string> action)
    {
        onValueChanged += action;
    }

    private void UpdateView()
    {
        //  Make sure a view exists
        if (!view)
        {
            Debug.Log("view has not been instantiated");
            return;
        }

        //  Update the view with the BossLogList associated with this dropdown's current value
        view.Display(DataController.Instance.bossLogsDictionary.GetBossLogList(thisDropdown.options[thisDropdown.value].text));

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
