using UnityEngine;
using UnityEngine.UI;

//  Creates a link between a boss or log dropdown and a display for it
//  For use in Logs tab as well as Comparisons Window
public abstract class DropdownDisplayLink : MonoBehaviour
{
    protected Dropdown thisDropdown { get; private set; }

    protected void Setup()
    {
        thisDropdown = GetComponent<Dropdown>();
        if (!thisDropdown)
            throw new System.Exception($"BossDropdownDisplayLink.cs is not attached to a dropdown gameobject!");
    }

    public abstract void LinkAndCreateWidget(in GameObject objectLocation);
}
