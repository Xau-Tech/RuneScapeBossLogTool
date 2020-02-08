using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveButtonScript : MonoBehaviour
{
    [SerializeField] private GameObject m_MenuPanel;

    private void Awake()
    {
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(Save);
    }

    private void Save()
    {
        DataController.Instance.SaveBossLogData();
    }
}
