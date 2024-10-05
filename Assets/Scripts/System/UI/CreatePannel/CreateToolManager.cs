using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateToolManager : MonoBehaviour
{
    [SerializeField]
    CreateToolInfoManager infoManage;
    public void ShowInfo(ToolUI_Create info)
    {
        infoManage.ShowToolInfo(info);
    }
}
