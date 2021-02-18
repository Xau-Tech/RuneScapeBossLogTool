using UnityEngine;
using UnityEngine.UI;

//  Window to add quantity of a setupitem
public class AddQuantityWindow : MonoBehaviour
{
    public AddQuantityWindow()
    {
        Instance = this;
    }

    public static AddQuantityWindow Instance;

    [SerializeField] private GameObject thisWindow;
    [SerializeField] private AddQuantityButton addButtonScript;
    private InputField newLogField;
    private ProgramState.states previousState;

    private void Awake()
    {
        newLogField = thisWindow.GetComponentInChildren<InputField>();
    }

    //  Opens the window and caches the previous state to return to on close
    public void OpenWindow(in AddedItemData itemData, in ProgramState.states currentState, SetupCollections collectionType)
    {
        addButtonScript.itemToAddData = itemData;
        addButtonScript.collectionType = collectionType;

        previousState = currentState;

        WindowState.currentState = WindowState.states.AddSetupItemQuantity;
        thisWindow.SetActive(true);

        newLogField.text = "";
        newLogField.Select();
    }

    //  Closes the window and restores the previous state
    public void CloseWindow()
    {
        thisWindow.SetActive(false);
        ProgramState.CurrentState = previousState;
    }
}
