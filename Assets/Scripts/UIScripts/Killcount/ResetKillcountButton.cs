using UnityEngine;
using UnityEngine.UI;

//  Button to reset killcount
public class ResetKillcountButton : MonoBehaviour
{
    private Button thisButton;

    private void Awake()
    {
        thisButton = GetComponent<Button>();

        if (!thisButton)
            throw new System.Exception($"ResetKillcountButton.cs is not attached to a button gameobject!");

        thisButton.onClick.AddListener(ResetKillcount);
    }

    private void OnEnable()
    {
        EventManager.Instance.onLogUpdated += ResetKillcount;
        EventManager.Instance.onBossDropdownValueChanged += ResetKillcount;
    }

    private void OnDisable()
    {
        EventManager.Instance.onLogUpdated -= ResetKillcount;
        EventManager.Instance.onBossDropdownValueChanged -= ResetKillcount;
    }

    private void ResetKillcount()
    {
        Killcount.Reset();
    }
}
