using UnityEngine;
using UnityEngine.UI;

//  Text that displays the total value of the current dropList
public class DropListValueText : MonoBehaviour
{
    private Text thisText;

    private void Awake()
    {
        thisText = GetComponent<Text>();

        if (!thisText)
            throw new System.Exception($"DropListValueText.cs is not attached to a text gameobject!");
    }

    private void OnEnable()
    {
        EventManager.Instance.onDropListModified += UpdateText;
    }

    private void OnDisable()
    {
        EventManager.Instance.onDropListModified -= UpdateText;
    }

    private void UpdateText(int addedItemID)
    {
        thisText.text = $"Total value: {DataController.Instance.dropList.TotalValue().ToString("N0")} gp";
    }
}
