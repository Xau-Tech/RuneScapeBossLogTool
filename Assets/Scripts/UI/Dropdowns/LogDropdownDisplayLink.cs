using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogDropdownDisplayLink : DropdownDisplayLink
{
    private string bossName { get; set; }
    private BossLogDisplay view { get; set; }

    private void Awake()
    {
        base.Setup();
        thisDropdown.onValueChanged.AddListener(UpdateView);
    }

    //  Set the boss linked to this LogDisplay
    public void SetBoss(string bossName)
    {
        this.bossName = bossName;

        if(thisDropdown.options.Count != 0)
            UpdateView(thisDropdown.value);
    }

    private void UpdateView(int index)
    {
        //  Make sure a view exists
        if (!view)
        {
            Debug.Log("view has not been instantiated");
            return;
        }

        BossLogList bossLogList = DataController.Instance.bossLogsDictionary.GetBossLogList(bossName);
        Debug.Log($"{bossLogList.bossName}");
        //  If there are no Logs for selected boss, pass a BossLog with logname "Empty" and no data
        if (bossLogList.Count == 0 || bossLogList == null)
            view.Display(new BossLog("", "Empty"));
        else
        {
            //  Ensure log exists and display it
            string logName = thisDropdown.options[thisDropdown.value].text;
            if (bossLogList.Exists(logName))
                view.Display(bossLogList.Find(logName));
        }
    }

    //  Cache and create a BossLogWidget at passed GameObject's position
    public override void LinkAndCreateWidget(in GameObject objectLocation)
    {
        view = WidgetFactory.InstantiateWidget(WidgetTypes.LogTotals, objectLocation).GetComponent<BossLogDisplay>();
    }
}
