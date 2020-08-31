using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//  Button to remove the selected log
public class DeleteLogButton : MonoBehaviour
{
    private Button thisButton;

    private void Awake()
    {
        thisButton = GetComponent<Button>();

        if (!thisButton)
            throw new System.Exception($"DeleteLogButton.cs is not attached to a button gameobject!");
        else
            thisButton.onClick.AddListener(DeleteLog);
    }

    //  Prompt user for confirmation to delete selected log
    private void DeleteLog()
    {
        //  Make sure there is a log to delete
        if (!DataController.Instance.bossLogsDictionary.ContainsLogName(CacheManager.currentBoss.bossID, CacheManager.currentLog))
        {
            InputWarningWindow.Instance.OpenWindow($"There are no logs to delete!");
            return;
        }

        ConfirmWindow.Instance.NewConfirmWindow($"Are you sure you want to delete the {CacheManager.currentLog} log for the {CacheManager.currentBoss} boss?"
            , DeleteLogResponse);
    }

    //  Confirm callback function
    private void DeleteLogResponse(UserResponse response, string returnedData)
    {
        //  Delete if user confirms choice
        if (response.userResponse)
        {
            DataController.Instance.bossLogsDictionary.RemoveLog(CacheManager.currentBoss.bossID, CacheManager.currentLog);
            Debug.Log($"Log deleted");
        }
        else
            Debug.Log($"User chose not to delete log");
    }
}
