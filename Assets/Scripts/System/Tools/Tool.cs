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
        TypeCnt
    }

    public type toolType;
    public int toolCnt { get; set; }

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
