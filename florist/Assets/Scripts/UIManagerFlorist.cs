using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManagerFlorist : MonoBehaviour
{
    public static UIManagerFlorist ins;
    public GameObject[] panelList;

    private void Awake()
    {
        if (ins == null)
            ins = this;
    }
    private void Start()
    {
        CloseAllPanels();
    }

    public void OpenPanel(GameObject go)
    {
        for (int i = 0; i < panelList.Length; i++)
        {
            if (panelList[i] == go)
                panelList[i].SetActive(true);
            else
                panelList[i].SetActive(false);
        }
    }

    public void CloseAllPanels()
    {
        for (int i = 0; i < panelList.Length; i++)
        {
            if(panelList[i].activeSelf)
                panelList[i].SetActive(false);
        }
    }

    public void IncreaseWeaponLevel()
    {
        WeaponStation.ins.NextLevel();
    }

    public void IncreaseIdleGeneretorLevel()
    {
        BuildingController.ins.NextLevel();
    }

    public void FailToPayLevelUp()
    {
        OpenPanel(panelList[3]);
    }
}
