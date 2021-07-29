using UnityEngine;
using UnityEngine.UI;

public class LogTripButton : MonoBehaviour
{
    private Button thisButton;

    private void Awake()
    {
        thisButton = GetComponent<Button>();

        if (!thisButton)
            throw new System.Exception($"LogTripButton.cs is not attached to a button gameobject!");
        else
            thisButton.onClick.AddListener(LogTripClick);
    }

    private void LogTripClick()
    {
        //  Make sure a log exists for selected boss to add onto
        if (DataController.Instance.bossLogsDictionary.GetBossLogList(CacheManager.currentBoss.bossID).Count == 0)
        {
            InputWarningWindow.Instance.OpenWindow($"You must create at least one log for {CacheManager.currentBoss.bossName} before attempting" +
                $" to add data to it!");
            return;
        }

        Timer.Stop();
        AddToLogWindow.Instance.OpenWindow(ProgramState.CurrentState);
    }
}
