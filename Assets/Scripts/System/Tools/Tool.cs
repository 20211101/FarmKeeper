using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool : MonoBehaviour
{
    public enum type
    {
        Club = 0,
        Spear,
        Granade,
        SlingTurret,
        TypeCnt
    }

    public type toolType;
    private int _toolCnt = 0;
    public int toolCnt { get=>_toolCnt; 
        set { _toolCnt = value; 
            InventoryToolPannelUI.instance.ChangeToolCount((int)toolType, _toolCnt); } 
    }

    public virtual void Action()
    {
    }
    public virtual void ClassReset()
    {
    }

    public virtual void Setting()
    {
    }
}
