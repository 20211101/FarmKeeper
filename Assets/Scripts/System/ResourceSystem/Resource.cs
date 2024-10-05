using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Resource
{

    public enum EType
    {
        Wood = 0,
        Stone,
        Steal,
        Meat,
        Leather,
        TypeCnt
    }
    public Resource(EType _type, int c = 0)
    {
        type = _type;
        cnt = c;
    }
    public Resource(int c = 0)
    {
        type = EType.Wood;
        cnt = c;
    }
    public EType type;

    public int cnt = 0;
}
