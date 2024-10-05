using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ToolRecipy
{
    public ToolRecipy(string name, Tool.type t, Resource[] c)
    { toolName = name; toolType = t; cost = c; }
    public string toolName;
    public Tool.type toolType;
    public Resource[] cost;
}

public class ToolCreator : MonoBehaviour
{
    PlayerInventory inventory;

    ToolRecipy[] recipies = new ToolRecipy[]
    {
        new ToolRecipy("¸ùµÕÀÌ", Tool.type.Club, new Resource[]{new Resource(Resource.EType.Wood,10) })
    };

    public void Make(Tool.type t)
    {
        if (CanMake(recipies[(int)t].cost) == false) return;

        foreach (Resource i in recipies[(int)t].cost)
        {
            inventory.resources[(int)i.type].cnt -= i.cnt;
        }

        inventory.MakeTool(t);
    }

    private bool CanMake(Resource[] cost)
    {
        foreach(Resource i in cost)
        {
            if(inventory.resources[(int)i.type].cnt < i.cnt)
            {
                return false;
            }
        }
        return true;
    }
}
