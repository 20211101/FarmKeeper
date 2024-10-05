using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcePrintControll_CreatePannel : MonoBehaviour
{
    [SerializeField]
    Resource_CreatePannel[] resources;

    public void PrintResources(ToolRecipy recipy)
    {
        foreach (Resource_CreatePannel r in resources)
        {
            r.gameObject.SetActive(false);
        }
        foreach (Resource r in recipy.cost)
        {
            resources[(int)r.type].gameObject.SetActive(true);
            resources[(int)r.type].ChangeCnt(r.cnt);
        }
    }
}
