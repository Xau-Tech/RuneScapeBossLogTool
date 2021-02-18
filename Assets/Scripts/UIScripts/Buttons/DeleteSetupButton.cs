using UnityEngine;
using UnityEngine.UI;

public class DeleteSetupButton : MonoBehaviour
{
    private Button thisButton;

    private void Awake()
    {
        thisButton = GetComponent<Button>();

        if (!thisButton)
            throw new System.Exception($"DeleteSetupButton.cs is not attached to a button gameobject!");
        else
            thisButton.onClick.AddListener(DeleteSetup);
    }

    //  Prompt user for confirmation to delete selected setup
    private void DeleteSetup()
    {
        string setupToDelete = CacheManager.SetupTab.Setup.CurrentSetupName;

        //  Make sure there is a log to delete
        if (!DataController.Instance.setupDictionary.ContainsKey(setupToDelete))
        {
            InputWarningWindow.Instance.OpenWindow($"There is no setup called {setupToDelete} to delete!");
            return;
        }

        ConfirmWindow.Instance.NewConfirmWindow($"Are you sure you want to delete the {setupToDelete} setup?", DeleteSetupResponse, setupToDelete);
    }

    //  Confirm callback function
    private void DeleteSetupResponse(UserResponse response, string returnedData)
    {
        //  Delete if user confirms choice
        if (response.userResponse)
        {
            DataController.Instance.setupDictionary.Remove(returnedData);
            EventManager.Instance.SetupDeleted();
        }
    }
}
