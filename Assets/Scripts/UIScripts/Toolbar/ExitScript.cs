using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GoogleSheetsToUnity;

//  Exit application script
public class ExitScript : MonoBehaviour
{
    private void Awake()
    {
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(Exit);
    }


    private void Exit()
    {
        //  If there is active drop list data, confirm the user wishes to exit the program
        if(DataController.Instance.DropList.data.Count != 0)
        {
            ProgramState.CurrentState = ProgramState.states.Exit;
            EventManager.Instance.ConfirmOpen("You have an open list of drops.\nAre you sure you want to exit?");
        }
        else
        {
            ProgramControl.Instance.CloseProgram();
        }
    }
}
