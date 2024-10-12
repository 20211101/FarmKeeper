using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CreateToolInfoManager : MonoBehaviour
{
    [SerializeField]
    Image toolImg;
    [SerializeField]
    TextMeshProUGUI toolName;
    [SerializeField]
    TextMeshProUGUI description;
    [SerializeField]
    ResourcePrintControll_CreatePannel resourceInfo;
    [SerializeField]
    Button createButton;

    public void ShowToolInfo(ToolUI_Create info, bool canBuy)
    {
        toolImg.sprite = info.ToolImg.sprite;
        toolName.text = info.Recipy.toolName;
        description.text = info.Description;
        resourceInfo.PrintResources(info.Recipy);
        createButton.interactable = canBuy;
    }

    public void ButtonEnableCheck(bool canBuy)
    {
        createButton.interactable = canBuy;
    }
}
