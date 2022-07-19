using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class NextTabObject : MonoBehaviour
{
    [SerializeField] private Selectable _nextSelectable;

    private void Awake()
    {
        if (_nextSelectable == null)
            throw new System.Exception($"NextTabObject.cs ERROR: Next selectable object not set in inspector window!");
    }

    private void LateUpdate()
    {
        //  Ensure this object is selected
        if(EventSystem.current.currentSelectedGameObject == this.gameObject)
        {
            if (Input.GetKeyDown(KeyCode.Tab))
                _nextSelectable.Select();
        }
    }
}
