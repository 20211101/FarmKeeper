using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateToolManager : MonoBehaviour
{
    [SerializeField]
    CreateToolInfoManager infoManage;
    [SerializeField]
    PlayerInventory inventory;
    ToolUI_Create showingTool;

    private void Start()
    {

        inventory.getResourceDel += () =>
        {
            if (showingTool == null) return;
            infoManage.ButtonEnableCheck(CanBuy(showingTool.Recipy.cost));
        };
    }

    public void ShowInfo(ToolUI_Create info)
    {
        infoManage.gameObject.SetActive(true);
        infoManage.ShowToolInfo(info, CanBuy(info.Recipy.cost));
        showingTool = info;
    }

    public void CreateTool()
    {
        foreach (Resource i in showingTool.Recipy.cost)
        {
            inventory.resources[(int)i.type].cnt -= i.cnt;
        }

        SoundPlayer.instance.PlayGoldSound();

        if(showingTool.Recipy.recipyT == RecipyType.Tool)
            inventory.MakeTool(showingTool.Recipy.toolType);
        else
            inventory.GetResource(new Resource(showingTool.Recipy.resourceType, showingTool.Recipy.resourceCount));
        infoManage.ButtonEnableCheck(CanBuy(showingTool.Recipy.cost));  
    }

    private bool CanBuy(Resource[] cost)
    {
        foreach (Resource i in cost)
        {
            if (inventory.resources[(int)i.type].cnt < i.cnt)
            {
                return false;
            }
        }
        return true;
    }
}
