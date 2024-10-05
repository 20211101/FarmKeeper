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

    public void ShowToolInfo(ToolUI_Create info)
    {
        toolImg = info.ToolImg;
        toolName.text = info.Recipy.toolName;
        description.text = info.Description;
        resourceInfo.PrintResources(info.Recipy);
    }
}
