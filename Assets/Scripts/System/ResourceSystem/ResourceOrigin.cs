using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceOrigin : MonoBehaviour
{
    [SerializeField]
    public int responeTime;
    public virtual void Damaged(PlayerInventory playerInventory)
    {
    }
    public virtual void Respone()
    {

    }
    public virtual void Diactivate()
    {

    }
}
