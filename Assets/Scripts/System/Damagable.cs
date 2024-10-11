using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damagable : MonoBehaviour
{
    protected int MAX_HP = 100;
    public int MaxHp { get => MAX_HP; private set { } }
    protected int _hp = 100;
    protected int hp
    {
        get => _hp;
        set
        {
            _hp = value;
            _hp = Mathf.Clamp(_hp, 0, MAX_HP);
        }
    }
    public int Hp { get => hp; private set { } }

    public virtual void Damaged(DamageInfo damage)
    {

    }

    public virtual void Heal(int healAmount)
    {
        hp += healAmount;
    }
}
