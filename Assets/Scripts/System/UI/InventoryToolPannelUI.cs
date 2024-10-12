using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class InventoryToolPannelUI : MonoBehaviour
{
    private static InventoryToolPannelUI _instance;
    public static InventoryToolPannelUI instance { get => _instance; private set { } }


    [SerializeField]
    ToolUI[] toolUIs = new ToolUI[10];
    [SerializeField]
    PlayerHand hand;

    void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(gameObject);
    }

    public void ChangeToolCount(int type, int count)
    {
        toolUIs[type].CntTxt.text = count.ToString();
    }

    public void ChangeSelectedTool(int selectedToolType)
    {
        foreach (ToolUI i in toolUIs)
            i.ChangeUnSelected();
        toolUIs[selectedToolType].ChangeSelected();
    }

    public void CantChangeReaction(Tool.type t)
    {
        toolUIs[(int)t].CantChangeReaction();
    }
}
