using UnityEngine;
using UnityEngine.UI;

public class NewSetupButton : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        NewSetupWindow.Instance.OpenWindow(ProgramState.CurrentState);
    }
}
