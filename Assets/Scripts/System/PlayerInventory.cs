using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 플레이어가 가지는 자원 관리
public class PlayerInventory : MonoBehaviour
{
    public Resource[] resources = new Resource[(int)Resource.EType.TypeCnt]
        {
            new Resource(Resource.EType.Wood),
            new Resource(Resource.EType.Stone),
            new Resource(Resource.EType.Steal),
            new Resource(Resource.EType.Meat),
            new Resource(Resource.EType.Leather)
        };
    [SerializeField]
    Tool[] tools = new Tool[(int)Tool.type.TypeCnt];

    public void GetResource(Resource resource)
    {
        resources[(int)resource.type].cnt += resource.cnt;
        Debug.Log($"타입 : {resource.type}, {resource.cnt}개 추가됨");
    }

    internal bool CanGet(Tool.type t)
    {
        if (tools[(int)t] != null) return true;
        else return false;
    }

    public void MakeTool(Tool.type t)
    {
        ++tools[(int)t].toolCnt;
    }

    internal Tool GetTool(Tool.type t)
    {
        tools[(int)t].Setting();
        return tools[(int)t];
    }
}
