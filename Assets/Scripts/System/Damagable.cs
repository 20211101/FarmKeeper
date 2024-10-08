using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damagable : MonoBehaviour
{
    protected int MAX_HP = 100;
    public int MaxHp { get => MAX_HP; private set { } }
    protected int hp = 100;
    public int Hp { get => hp; private set { } }

    public virtual void Damaged(int damage)
    {

    }
}
