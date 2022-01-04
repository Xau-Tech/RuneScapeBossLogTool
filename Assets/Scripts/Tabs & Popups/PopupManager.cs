using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class PopupManager : MonoBehaviour
{
    //  Properties & fields
    public static PopupManager Instance { get { return _instance; } }

    [SerializeField] private AbstractPopup _notificationPopup;
    [SerializeField] private AbstractPopup _confirmPopup;
    [SerializeField] private AbstractPopup _logTripPopup;
    [SerializeField] private AbstractPopup _inputPopup;
    private static PopupManager _instance = null;

    //  Monobehavior methods
    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else if (_instance != this)
            Destroy(gameObject);
    }

    //  Methods

    public void ShowNotification(string message)
    {
        _notificationPopup.OpenPopup(message);
    }

    public async Task<bool> ShowConfirm(string message)
    {
        return await ((ConfirmPopup)_confirmPopup).OpenPopup(message);
    }

    public void ShowLogTrip(DropsTab dropTab)
    {
        _logTripPopup.OpenPopup(dropTab);
    }

    /// <summary>
    /// Open the input popup with the specificed option
    /// </summary>
    /// <param name="option">The InputPopup class contains public static fields to use as parameters for the option</param>
    /// <returns>The value entered by the user </returns>
    public async Task<string> ShowInputPopup(byte option)
    {
        return await ((InputPopup)_inputPopup).OpenPopup(option);
    }
}