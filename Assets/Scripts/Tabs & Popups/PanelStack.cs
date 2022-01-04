using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Keeps track of the stack of active panels in the application
/// </summary>
public class PanelStack
{
    //  Properties & fields
    private static Stack<AbstractPanel> _panelStack = new Stack<AbstractPanel>();
    private static Dictionary<string, AbstractTab> _mainTabsDict = new Dictionary<string, AbstractTab>();

    //  Constructor
    public PanelStack()
    {
        
    }

    //  Methods

    public void Setup(List<AbstractTab> mainTabs)
    {
        for(int i = 0; i < AppState.GetTabStates().Count; ++i)
        {
            AbstractTab absTabScript = mainTabs[i];
            absTabScript.gameObject.SetActive(false);
            _mainTabsDict.Add(absTabScript.AssociatedTabState.ToString(), absTabScript);
        }
    }

    public void SwitchTabs(Enums.TabStates newTabState)
    {
        string tabStateString = newTabState.ToString();

        _mainTabsDict.TryGetValue(tabStateString, out AbstractTab newTab);

        if(_panelStack.Count <= 1)
        {
            if (_panelStack.Count == 1)
            {
                _panelStack.Peek().gameObject.SetActive(false);
                _panelStack.Pop();
            }

            _panelStack.Push(newTab);
            newTab.gameObject.SetActive(true);
        }
        else
        {
            Debug.Log("Should not be switching tab states with windows/popups open!");
        }

        //PrintDebugInfo();
    }

    public bool OpenPopup(AbstractPopup popup)
    {
        if(_panelStack.Peek() is AbstractPopup && _panelStack.Peek() == popup)
        {
            Debug.Log("ERROR: Cannot open 2 of the same popup on top of each other!");
            return false;
        }
        else
        {
            popup.gameObject.SetActive(true);
            _panelStack.Push(popup);
            //PrintDebugInfo();
            return true;
        }
    }

    public void ClosePopup(AbstractPopup sender)
    {
        //  Make sure the sender matches the top object on the stack
        if(sender == (_panelStack.Peek()))
        {
            AbstractPopup popup = (AbstractPopup)_panelStack.Pop();
            popup.gameObject.SetActive(false);

            if (_panelStack.Peek() is AbstractPopup)
                AppState.PopupState = ((AbstractPopup)_panelStack.Peek()).AssociatedPopupState;
        }
        else
        {
            throw new System.Exception($"Sending popup object: {sender.name}\nTop popup object on stack: {_panelStack.Peek().name}");
        }

        //PrintDebugInfo();
    }

    private void PrintDebugInfo()
    {
        string message = "Printing stack:";

        foreach(AbstractPanel ap in _panelStack)
        {
            message += $"\n{ap.name}";
        }

        Debug.Log(message);
    }
}
