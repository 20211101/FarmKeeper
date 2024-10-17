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
            new Resource(Resource.EType.Leather),
            new Resource(Resource.EType.Coin),
            new Resource(Resource.EType.TNTPowder)
        };
    [SerializeField]
    Tool[] tools = new Tool[(int)Tool.type.TypeCnt];

    public delegate void GetResourceDel();
    public event GetResourceDel getResourceDel;

    public void GetResource(Resource resource)
    {
        resources[(int)resource.type].cnt += resource.cnt;
        if (getResourceDel != null)
            getResourceDel();
        Debug.Log($"타입 : {resource.type}, {resource.cnt}개 추가됨");
    }

    // 도구를 이 타입으로 바꿀 수 있는지 확인하는 함수
    internal bool CanGet(Tool.type t)
    {
        if (tools[(int)t] != null && tools[(int)t].toolCnt > 0) return true;
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


    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.transform.CompareTag("ResourcePrab"))
        {
            SoundPlayer.instance.PlayGainSound();
            GetResource(hit.transform.GetComponent<ResourcePickUp>().resource);
            Destroy(hit.gameObject);
        }
    }
}
