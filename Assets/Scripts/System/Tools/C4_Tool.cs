using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C4_Tool : Tool
{
    [SerializeField] GameObject C4Obj;
    public override void Action()
    {
        Instantiate(C4Obj, transform.position + new Vector3(0, 2, 0) + transform.forward * 2, Quaternion.identity);
        toolCnt--;
        if (toolCnt < 1)
        {
            PlayerHand.instance.ChangeTool(type.Club);
        }
    }

}
