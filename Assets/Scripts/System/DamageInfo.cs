using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageInfo 
{
    public DamageInfo(int d, GameObject g)
    {
        damage = d;
        damager = g;
    }

    public int damage;
    public GameObject damager;
}
