using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RecipyType
{
    Tool,
    Resource
}
[System.Serializable]
public class ToolRecipy
{
    public ToolRecipy(RecipyType recipy, string name, Tool.type t, Resource.EType resT, int _resourceCount, Resource[] c)
    { recipyT = recipy; toolName = name; toolType = t; resourceType = resT; resourceCount = _resourceCount; cost = c; }
    public RecipyType recipyT;
    public string toolName;
    public Tool.type toolType;
    public Resource.EType resourceType;
    public int resourceCount;
    public Resource[] cost;
}

public class ToolCreator : MonoBehaviour
{
    PlayerInventory inventory;

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
