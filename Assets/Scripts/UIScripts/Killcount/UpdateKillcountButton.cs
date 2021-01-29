using UnityEngine;
using UnityEngine.UI;

//  Button to increment killcount
public class UpdateKillcountButton : MonoBehaviour
{
    [SerializeField] private short addValue;

    private Button thisButton;

    private void Awake()
    {
        thisButton = GetComponent<Button>();

        if (!thisButton)
            throw new System.Exception($"AddKillButton.cs is not attached to a button gameobject!");

        thisButton.onClick.AddListener(UpdateKillCount);
    }

    private void UpdateKillCount()
    {
        Killcount.UpdateKillcount(addValue);
    }
}
